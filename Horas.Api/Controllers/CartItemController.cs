namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public CartItemController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var foundList = await _uow.CartItemRepository.GetAllAsyncInclude();

                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<CartItemResDto>>(foundList);

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
        public async Task<ActionResult> GetCartItem(Guid id)
        {
            try
            {
                var found = await _uow.CartItemRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<CartItemResDto>(found);

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
        public async Task<IActionResult> Create([FromForm] CartItemCreateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartItem = _mapper.Map<CartItem>(requestDto);
            if (cartItem == null)
                return BadRequest("Invalid CartItem data.");

            var created = await _uow.CartItemRepository.CreateAsync(cartItem);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartItemResDto>(created);
                return CreatedAtAction(nameof(GetCartItem), new { id = mapped.Id }, mapped);
            }
            else
                return BadRequest();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] CartItemUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var found = await _uow.CartItemRepository.GetAsyncInclude(id);

            if (found == null)
                return NotFound();

            var cartItem = _mapper.Map<CartItem>(requestDto);

            if (cartItem == null)
                return BadRequest("Invalid CartItem data.");

            var updated = await _uow.CartItemRepository.UpdateAsync(cartItem);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartItemResDto>(updated);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var found = await _uow.CartItemRepository.GetAsyncInclude(id);

            if (found == null)
                return NotFound();

            var deleted = await _uow.CartItemRepository.DeleteAsync(id);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartItemResDto>(deleted);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }
    }
}
