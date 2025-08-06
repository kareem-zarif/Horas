
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public CartController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var foundList = await _uow.CartRepository.GetAllAsyncInclude();

                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<CartResDto>>(foundList);

                if (mapped == null)
                    return NotFound();

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message} :: {ex.InnerException}");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetCart(Guid id)
        {
            try
            {
                var found = await _uow.CartRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<CartResDto>(found);

                if (mapped == null)
                    return NotFound();

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message} :: {ex.InnerException}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartCreateDto requestDto)
        {
            var exsiting = await _uow.CartRepository.GetByCustomerIdAsync(requestDto.CustomerId);
            if (exsiting != null)
            {
                return Conflict("Cart already exists for this customer.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = _mapper.Map<Cart>(requestDto);

            var created = await _uow.CartRepository.CreateAsync(cart);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] CartUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var found = await _uow.CartRepository.GetAsyncInclude(requestDto.Id);

            if (found == null)
                return NotFound();

            var cart = _mapper.Map<Cart>(requestDto);

            var updated = await _uow.CartRepository.UpdateAsync(cart);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartResDto>(updated);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var found = await _uow.CartRepository.GetAsyncInclude(id);

            if (found == null)
                return NotFound();

            var deleted = await _uow.CartRepository.DeleteAsync(id);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartResDto>(deleted);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }

        [HttpGet("byCustomer/{customerId}")]
        public async Task<ActionResult> GetCartByCustomer(Guid customerId)
        {
            try
            {
                var found = await _uow.CartRepository.GetByCustomerIdAsync(customerId);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<CartResDto>(found);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message} :: {ex.InnerException}");
            }
        }
    }
}
