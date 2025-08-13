namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWishlistController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public ProductWishlistController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ProductWish = await _uow.ProductWishListRepository.GetAllAsyncInclude();

                if (ProductWish == null || !ProductWish.Any())
                    return NotFound();

                var dto = _mapper.Map<IEnumerable<ProductWishlistResDto>>(ProductWish);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message} {ex.InnerException}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            try
            {
                var ProductWish = await _uow.ProductWishListRepository.GetAsyncInclude(id);

                if (ProductWish == null)
                    return NotFound();

                var dto = _mapper.Map<ProductWishlistResDto>(ProductWish);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}   {ex.InnerException}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductWishlistCreateDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ProducWish = _mapper.Map<ProductWishList>(dto);

                if (ProducWish == null)
                    return BadRequest();

                var created = await _uow.ProductWishListRepository.CreateAsync(ProducWish);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<ProductWishList>(created);
                    return Ok(mapped);
                }
                else return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message} {ex.InnerException}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductWishlistUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ProductWish = await _uow.ProductWishListRepository.GetAsync(dto.Id);

            if (ProductWish == null)
                return NotFound();

            var updated = await _uow.ProductWishListRepository.UpdateAsyncInclude(ProductWish);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<ProductWishlistResDto>(updated);
                return Ok(result);
            }
            else
                return BadRequest("No changes were saved.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var wishlist = await _uow.ProductWishListRepository.GetAsync(id);

                if (wishlist == null)
                    return NotFound("Wishlist not found.");

                var deleted = await _uow.ProductWishListRepository.DeleteAsync(id);
                var saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<ProductWishlistResDto>(deleted);
                    return Ok(mapped);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}   {ex.InnerException}");
            }
        }

        [HttpDelete("{wishlistId}/{productId}")]
        public async Task<IActionResult> DeleteByCustomerIdProductId(Guid wishlistId, Guid productId)
        {
            try
            {
                var productWishList = await _uow.ProductWishListRepository.GetByWishlistIdByProductId(wishlistId, productId);

                if (productWishList == null)
                    return NotFound("productWishList not found.");

                var deleted = await _uow.ProductWishListRepository.DeleteByCustomerIdByProductId(wishlistId: wishlistId, productId: productId);

                var saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<ProductWishlistResDto>(deleted);
                    return Ok(mapped);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}   {ex.InnerException}");
            }
        }

        [HttpDelete("clear/{wishlistId}")]
        public async Task<IActionResult> ClearByWishlistId(Guid wishlistId)
        {
            try
            {
                var foundWishListHasProduct = await _uow.ProductWishListRepository.GetAllByWishlistId(wishlistId);

                if (foundWishListHasProduct == null | !foundWishListHasProduct.Any())
                    return NotFound("No products found for this wishlist");

                var deleted = await _uow.ProductWishListRepository.ClearByWishlistId(wishlistId);
                var saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<IList<ProductWishlistResDto>>(deleted);
                    return Ok(mapped);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}   {ex.InnerException}");
            }
        }
    }
}