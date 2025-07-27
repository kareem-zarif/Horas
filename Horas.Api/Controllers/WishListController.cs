
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public WishListController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var wishlists = await _uow.WishListRepository.GetAllAsyncInclude();

                if (wishlists == null || !wishlists.Any())
                    return NotFound(); 

                var dto = _mapper.Map<IEnumerable<WishListResDto>>(wishlists);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetWishListById(Guid id)
        {
            try
            {
                var wishlist = await _uow.WishListRepository.GetAsyncInclude(id);

                if (wishlist == null)
                    return NotFound();

                var mapped = _mapper.Map<WishListResDto>(wishlist);

                if (mapped == null)
                    return NotFound();

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlist(Guid id)
        {
            try
            {
                var wishlist = await _uow.WishListRepository.GetAsync(id);

                if (wishlist == null)
                    return NotFound("Wishlist not found.");

                await _uow.WishListRepository.DeleteAsync(id);
                await _uow.Complete();

                return Ok("Wishlist deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(WishListCreateDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto == null)
                return BadRequest();

            try
            {
                var wishlist = _mapper.Map<Wishlist>(dto);

                if (wishlist == null)
                    return BadRequest();

                await _uow.WishListRepository.CreateAsync(wishlist);
                await _uow.Complete();

                var mapped = _mapper.Map<WishListResDto>(wishlist);

                return Content("Wishlist Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWishlistDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var wishlist = await _uow.WishListRepository.GetAsyncInclude(dto.Id);

            if (wishlist == null)
                return NotFound();

            _mapper.Map(dto, wishlist);

            await _uow.WishListRepository.UpdateAsync(wishlist);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<WishListResDto>(wishlist);
                return Ok(result);
            }
            else
            {
                return BadRequest("No changes were saved.");
            }
        }

    
}
}