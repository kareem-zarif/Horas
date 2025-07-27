
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
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            try
            {
                var ProductWish = await _uow.ProductWishListRepository.GetAsyncInclude(id);

                if (ProductWish == null )
                    return NotFound();

                var dto = _mapper.Map<ProductWishlistResDto>(ProductWish);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductWishlistCreateDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto == null)
                return BadRequest();

            try
            {
                var ProducWish = _mapper.Map<ProductWishList>(dto);

                if (ProducWish == null)
                    return BadRequest();

                await _uow.ProductWishListRepository.CreateAsync(ProducWish);
                await _uow.Complete();

                var mapped = _mapper.Map<ProductWishlistCreateDto>(ProducWish);

                return Content("Wishlist Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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

            _mapper.Map(dto, ProductWish);

            await _uow.ProductWishListRepository.UpdateAsync(ProductWish);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<ProductWishlistResDto>(ProductWish);
                return Ok(result);
            }
            else
            {
                return BadRequest("No changes were saved.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var wishlist = await _uow.ProductWishListRepository.GetAsync(id);

                if (wishlist == null)
                    return NotFound("Wishlist not found.");

                await _uow.ProductWishListRepository.DeleteAsync(id);
                await _uow.Complete();

                return Ok("Wishlist deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}