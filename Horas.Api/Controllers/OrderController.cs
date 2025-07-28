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


            var order = _mapper.Map<Order>(requestDto);

            var created = await _uow.OrderRepository.CreateAsync(order);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<OrderResDto>(created);
                return Ok(mapped);
            }
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var order = await _uow.OrderRepository.GetAsync(id);

                if (order == null)
                    return NotFound();

                var deleted = await _uow.OrderRepository.DeleteAsync(id);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<OrderResDto>(deleted);
                    return Ok(mapped);
                }
                else return BadRequest();
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

            var mappedGo = _mapper.Map<Order>(dto);

            var updated = await _uow.OrderRepository.UpdateAsyncInclude(mappedGo);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<OrderResDto>(updated);
                return Ok(mapped);
            }
            else return BadRequest("No changes were saved.");

        }

    }
}
