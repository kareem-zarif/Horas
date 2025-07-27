
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public OrderController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var foundList = await _uow.OrderRepository.GetAllAsyncInclude();

                if (foundList == null)
                      return NotFound();

                var mapped = _mapper.Map<IEnumerable<OrderResDto>>(foundList);

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
        public async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                var found = await _uow.OrderRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<OrderResDto>(found);

                if (mapped == null)
                    return NotFound();

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create(OrderCreateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (requestDto == null)
                return BadRequest();

            try
            {
                var order = _mapper.Map<Order>(requestDto);

                if (order == null)
                    return BadRequest();

                await _uow.OrderRepository.CreateAsync(order);
                await _uow.Complete();

                var mapped = _mapper.Map<OrderResDto>(order);

                return Content("Order Added Successfully");
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
                var order = await _uow.OrderRepository.GetAsync(id);

                if (order == null)
                    return NotFound();

                await _uow.OrderRepository.DeleteAsync(id);

                await _uow.Complete();
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the order.");
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrderUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _uow.OrderRepository.GetAsyncInclude(dto.Id);

            if (order == null)
                return NotFound();

            _mapper.Map(dto, order);

            await _uow.OrderRepository.UpdateAsyncInclude(order);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<OrderResDto>(order);
                return Ok(result);
            }
            else
            {
                return BadRequest("No changes were saved.");
            }
        }

    }
}
