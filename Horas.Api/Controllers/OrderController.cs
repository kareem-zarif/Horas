using System.Security.Claims;

namespace Horas.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public OrderController(IUOW uow, IMapper mapper, IMediator mediator)
        {
            _uow = uow;
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out var userId))
                    return Unauthorized();

                var foundList = await _uow.OrderRepository.GetAllAsyncInclude(x => x.CustomerId == userId);

                if (foundList == null || !foundList.Any())
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<OrderResDto>>(foundList);

                if (mapped == null)
                    return NotFound();

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" {ex.Message} :: {ex.InnerException}");
            }
        }

        //[HttpGet("admin/all")]
        //[Authorize(Roles = "Admin")] // Assuming you have role-based authorization
        //public async Task<ActionResult> GetAllOrdersForAdmin()
        //{
        //    try
        //    {
        //        // Check if user has admin role
        //        if (!User.IsInRole("Admin"))
        //            return Forbid();

        //        var foundList = await _uow.OrderRepository.GetAllAsyncInclude();

        //        if (foundList == null || !foundList.Any())
        //            return NotFound();

        //        var mapped = _mapper.Map<IEnumerable<OrderResDto>>(foundList);

        //        if (mapped == null)
        //            return NotFound();

        //        return Ok(mapped);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $" {ex.Message} :: {ex.InnerException}");
        //    }
        //}


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out var userId))
                    return Unauthorized();

                var found = await _uow.OrderRepository.GetAsyncInclude(id);

                if (found == null || found.CustomerId != userId)
                    return NotFound();

                var mapped = _mapper.Map<OrderResDto>(found);

                if (mapped == null)
                    return NotFound();

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" {ex.Message} :: {ex.InnerException}");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create(OrderCreateDto requestDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(requestDto);
            order.CustomerId = userId; // Ensure customerId is set

            var created = await _uow.OrderRepository.CreateAsync(order);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                if (!order.CustomerId.HasValue)
                    return BadRequest("CustomerId is required to create a notification.");

                // Create initial OrderStatusHistory entry
                var statusHistory = new OrderStatusHistory
                {
                    OrderId = created.Id,
                    OrderStatus = OrderStatus.pending, // Initial status
                };
                await _uow.OrderStatusHistoryRepository.CreateAsync(statusHistory);
                await _uow.Complete();

                await _mediator.Publish(new NotificationEvent(
                    message: $" Order Created Successfully  {order.Id}",
                    personId: order.CustomerId.Value
                ));

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
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out var userId))
                    return Unauthorized();

                var order = await _uow.OrderRepository.GetAsync(id);

                if (order == null || order.CustomerId != userId)
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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var found = await _uow.OrderRepository.GetAsyncInclude(dto.Id);

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
