namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly HorasDBContext _context;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public RecommendationController(HorasDBContext context, IMemoryCache cache, IMapper mapper)
        {
            _context = context;
            _cache = cache;
            _mapper = mapper;
        }

        // ========== 1. Recommendations ==========
        [HttpGet("recommendations/{userId}")]
        public async Task<IActionResult> GetRecommendations(Guid userId)
        {
            string cacheKey = $"recommended_{userId}";

            if (_cache.TryGetValue(cacheKey, out IList<ProductResDto> cachedProducts))
                return Ok(cachedProducts);

            var userOrders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(e => e.SubCategory)
                .AsSplitQuery()
                .Where(o => o.CustomerId == userId)
                .ToListAsync();

            var favoriteCategoryIds = userOrders
                .SelectMany(o => o.OrderItems)
                .Where(oi => oi.Product != null && oi.Product.SubCategory != null)
                .GroupBy(oi => oi.Product.SubCategory.CategoryId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(3)
                .ToList();

            var purchasedProductIds = userOrders
                .SelectMany(o => o.OrderItems)
                .Select(oi => oi.ProductId)
                .Distinct()
                .ToList();

            IList<ProductResDto> recommendedProducts;

            if (favoriteCategoryIds.Any())
            {
                var alreadyPurchased = await _context.Products
                   .Include(p => p.SubCategory)
                   .Where(p => p.SubCategory != null &&
                          favoriteCategoryIds.Contains(p.SubCategory.CategoryId) &&
                          !purchasedProductIds.Contains(p.Id))
                   .Take(10)
                   .ToListAsync();

                recommendedProducts = _mapper.Map<IList<ProductResDto>>(alreadyPurchased);
            }
            else
            {
                var first10Products = await _context.Products
                     .OrderByDescending(p => p.Id)
                     .Take(10)
                     .ToListAsync();

                recommendedProducts = _mapper.Map<IList<ProductResDto>>(first10Products);
            }

            _cache.Set(cacheKey, recommendedProducts, TimeSpan.FromMinutes(30));
            return Ok(recommendedProducts);
        }

        // ========== 2. Best Sellers (Last Month) ==========
        [HttpGet("bestsellers")]
        public async Task<IActionResult> GetBestSellers()
        {
            var lastMonth = DateTime.UtcNow.AddMonths(-1);

            var bestSellers = await _context.OrderItems
                .Where(oi => oi.Order.CreatedOn >= lastMonth)
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    SalesCount = g.Count()
                })
                .OrderByDescending(x => x.SalesCount)
                .Take(10)
                .ToListAsync();

            var productIds = bestSellers.Select(x => x.ProductId).ToList();

            var products = await _context.Products
                .Include(p => p.SubCategory)
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            var result = bestSellers.Select(x => new
            {
                Product = _mapper.Map<ProductResDto>(products.First(p => p.Id == x.ProductId)),
                SalesCount = x.SalesCount,
                CategoryName = products.First(p => p.Id == x.ProductId).SubCategory?.Name
            });


            return Ok(result);
        }

        // ========== 3. New Releases ==========
        [HttpGet("newreleases")]
        public async Task<IActionResult> GetNewReleases()
        {
            var products = await _context.Products
                .OrderByDescending(p => p.CreatedOn)
                .Take(10)
                .ToListAsync();

            var mapped = _mapper.Map<IList<ProductResDto>>(products);

            return Ok(mapped);
        }
    }
}
