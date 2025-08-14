namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusHistoryController : ControllerBase
    {

        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrderStatusHistoryController(IUOW uow, IMapper mapper,IMediator mediator)
        {
            _uow = uow;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderStatusHistoryCreateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mappedGo = _mapper.Map<OrderStatusHistory>(requestDto);

            var created = await _uow.OrderStatusHistoryRepository.CreateAsync(mappedGo);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                await _mediator.Publish(new OrderStatusChangedEvent(created.OrderId, created.OrderStatus));
                var mapped = _mapper.Map<OrderStatusHistoryResDto>(mappedGo);
                return Ok(mapped);
            }
            else return BadRequest();

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

                var deleted = await _uow.OrderStatusHistoryRepository.DeleteAsync(id);

                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<OrderStatusHistoryResDto>(deleted);
                    return Ok(mapped);
                }
                else return BadRequest();
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

            var mappedGo = _mapper.Map<OrderStatusHistory>(dto);
            var updated = await _uow.OrderStatusHistoryRepository.UpdateAsync(mappedGo);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<OrderStatusHistoryResDto>(updated);
                return Ok(result);
            }
            else
                return BadRequest("No changes were saved.");
        }

    }
}
