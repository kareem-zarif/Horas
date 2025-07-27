
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusHistoryController : ControllerBase
    {

        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public OrderStatusHistoryController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderStatusHistoryCreateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (requestDto == null)
                return BadRequest();

            try
            {
                var orderSHis = _mapper.Map<OrderStatusHistory>(requestDto);
                orderSHis.ChangedAt = DateTime.UtcNow;

                if (orderSHis == null)
                    return BadRequest();

                await _uow.OrderStatusHistoryRepository.CreateAsync(orderSHis);
                await _uow.Complete();

                var mapped = _mapper.Map<OrderStatusHistoryResDto>(orderSHis);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var foundList = await _uow.OrderStatusHistoryRepository.GetAllAsync();

                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<OrderStatusHistoryResDto>>(foundList);

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
                var found = await _uow.OrderStatusHistoryRepository.GetAsync(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<OrderStatusHistoryResDto>(found);

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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var orderSHis = await _uow.OrderStatusHistoryRepository.GetAsync(id);

                if (orderSHis == null)
                    return NotFound();

                await _uow.OrderStatusHistoryRepository.DeleteAsync(id);

                await _uow.Complete();
                return NoContent();

            }
            catch (Exception msg)
            {
                return StatusCode(500, $"An error occurred while deleting the order.{msg}");
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderStatusHistoryUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderSHis = await _uow.OrderStatusHistoryRepository.GetAsync(dto.Id);

            if (orderSHis == null)
                return NotFound();

            _mapper.Map(dto, orderSHis);

            await _uow.OrderStatusHistoryRepository.UpdateAsync(orderSHis);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<OrderStatusHistoryResDto>(orderSHis);
                return Ok(result);
            }
            else
            {
                return BadRequest("No changes were saved.");
            }
        }

    }
}
