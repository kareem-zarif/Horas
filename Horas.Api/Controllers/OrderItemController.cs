
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public OrderItemController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var foundList = await _uow.OrderItemRepository.GetAllAsyncInclude();

                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<OrderItemResDto>>(foundList);

                if (mapped == null)
                    return NotFound();

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var orderItem = await _uow.OrderItemRepository.GetAsyncInclude(id);

                if (orderItem == null)
                    return NotFound();

                var dto = _mapper.Map<OrderItemResDto>(orderItem);

                if (dto == null)
                    return NotFound();

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderItemCreateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (requestDto == null)
                return BadRequest();

            try
            {
                var orderitem = _mapper.Map<OrderItem>(requestDto);

                if (orderitem == null)
                    return BadRequest();

                await _uow.OrderItemRepository.CreateAsync(orderitem);
                await _uow.Complete();

                var mapped = _mapper.Map<OrderItemResDto>(orderitem);

                return CreatedAtAction(nameof(GetAll), new { id = orderitem.Id }, mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {

                var orderItem = await _uow.OrderItemRepository.GetAsync(id);

                if (orderItem == null)
                    return NotFound();

                await _uow.OrderItemRepository.DeleteAsync(id);

                await _uow.Complete();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the payment method.");
            }

        }


        [HttpPut]
        public async Task<IActionResult> Update( OrderItemUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderitem = await _uow.OrderItemRepository.GetAsync(dto.Id);

            if (orderitem == null)
                return NotFound();

            _mapper.Map(dto, orderitem);

            await _uow.OrderItemRepository.UpdateAsync(orderitem);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<OrderItemResDto>(orderitem);
                return Ok(result);
            }
            else
            {
                return BadRequest("No changes were saved.");
            }
        }

    }
}

