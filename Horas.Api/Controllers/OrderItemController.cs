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

        [HttpGet("bySupplier/{supplierId}")]
        public async Task<ActionResult> GetBySupplierId(Guid supplierId)
        {
            try
            {
                var orderItems = await _uow.OrderItemRepository.GetAllBySupplierIdAsync(supplierId);

                if (orderItems == null || !orderItems.Any())
                    return NotFound();

                var mapped = _mapper.Map<IList<OrderItemResDto>>(orderItems);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message} :: {ex.InnerException}");
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

            var mappingGo = _mapper.Map<OrderItem>(requestDto);

            var created = await _uow.OrderItemRepository.CreateAsync(mappingGo);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<OrderItemResDto>(created);
                return Ok(mapped);
            }
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {

                var orderItem = await _uow.OrderItemRepository.GetAsync(id);

                if (orderItem == null)
                    return NotFound();

                var deleted = await _uow.OrderItemRepository.DeleteAsync(id);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<OrderItemResDto>(deleted);
                    return Ok(mapped);
                }
                else return BadRequest("can not save in database");

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the payment method.");
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update(OrderItemUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderitem = await _uow.OrderItemRepository.GetAsync(dto.Id);

            if (orderitem == null)
                return NotFound();

            var mappedGo = _mapper.Map<OrderItem>(dto);

            var updated = await _uow.OrderItemRepository.UpdateAsync(mappedGo);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<OrderItemResDto>(orderitem);
                return Ok(result);
            }
            else
                return BadRequest("No changes were saved.");
        }

    }
}

