
using Product = Horas.Domain.Product;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {

        private readonly HorasDBContext _context;
        private readonly IMemoryCache _cache;

        public RecommendationController(HorasDBContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecommendations(Guid userId)
        {
            string cacheKey = $"recommended_{userId}";

            // 1. لو الكاش فيه بيانات جاهزة رجعها فورًا
            if (_cache.TryGetValue(cacheKey, out List<Product> cachedProducts))
                return Ok(cachedProducts);

            // 2. هات الطلبات والمشتريات
            var userOrders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(e => e.SubCategory)
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

            List<Product> recommendedProducts;

            if (favoriteCategoryIds.Any())
            {
                recommendedProducts = await _context.Products
                 .Include(p => p.SubCategory)
                 .Where(p => p.SubCategory != null &&
                        favoriteCategoryIds.Contains(p.SubCategory.CategoryId) &&
                        !purchasedProductIds.Contains(p.Id))
                .Take(10)
                .ToListAsync();
            }
            else
            {
                // fallback: لو مفيش مشتريات
                recommendedProducts = await _context.Products
                    .OrderByDescending(p => p.Id)
                    .Take(10)
                    .ToListAsync();
            }

            // 3. خزّن النتيجة في الكاش
            _cache.Set(cacheKey, recommendedProducts, TimeSpan.FromMinutes(30));

            return Ok(recommendedProducts);
        }
    }

}
