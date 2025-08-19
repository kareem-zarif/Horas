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
                //var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //if (!Guid.TryParse(userIdClaim, out var userId))
                //    return Unauthorized();

                if (User.IsInRole("Customer") || User.IsInRole("Supplier") || User.IsInRole("Admin"))
                {
                    var foundList = await _uow.OrderRepository.GetAllAsyncInclude();

                    if (foundList == null || !foundList.Any())
                        return NotFound();

                    var mapped = _mapper.Map<IEnumerable<OrderResDto>>(foundList);

                    if (mapped == null)
                        return NotFound();

                    return Ok(mapped);
                }
                return Unauthorized();
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

        //[HttpGet("supplier/{supplierId}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> GetOrdersBySupplier(Guid supplierId)
        //{
        //    try
        //    {
        //        if (!User.IsInRole("Admin"))
        //            return Forbid();

        //        var allOrders = await _uow.OrderRepository.GetAllAsync(
        //            query => query
        //                .Include(o => o.OrderItems)
        //                .ThenInclude(oi => oi.Product)
        //                .ThenInclude(p => p.ProductSuppliers)
        //        );

        //        var supplierOrders = allOrders.Where(order =>
        //            order.OrderItems.Any(item =>
        //                item.Product != null &&
        //                item.Product.ProductSuppliers.Any(ps => ps.SupplierId == supplierId)
        //            )
        //        ).ToList();

        //        if (!supplierOrders.Any())
        //            return NotFound($"No orders found for supplier {supplierId}");

        //        var mapped = _mapper.Map<IEnumerable<OrderResDto>>(supplierOrders);

        //        return Ok(mapped);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $" {ex.Message} :: {ex.InnerException}");
        //    }
        //}

        //[HttpGet("seller/orders")]
        ////[Authorize(Roles = "Supplier")] // Allow sellers to access their own orders
        //public async Task<ActionResult> GetSellerOrders()
        //{
        //    try
        //    {
        //        // Get the current seller ID from th
        //        //var sellerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var sellerIdClaim = User.FindFirst("SupplierId")?.Value;
        //        if (!Guid.TryParse(sellerIdClaim, out var sellerId))
        //            return Unauthorized();

        //        // Get all orders with their items and products
        //        var allOrders = await _uow.OrderRepository.GetAllAsync(
        //            query => query
        //                .Include(o => o.OrderItems)
        //                .ThenInclude(oi => oi.Product)
        //                .ThenInclude(p => p.ProductSuppliers)
        //                .AsSplitQuery()
        //        );

        //        // Filter orders that contain products from this supplier
        //        var sellerOrders = allOrders.Where(order =>
        //            order.OrderItems.Any(item =>
        //                item.Product != null &&
        //                item.Product.ProductSuppliers.Any(ps => ps.SupplierId == sellerId)
        //            )
        //        ).ToList();

        //        if (!sellerOrders.Any())
        //            return NotFound($"No orders found for seller {sellerId}");

        //        var mapped = _mapper.Map<List<OrderResDto>>(sellerOrders);
        //        return Ok(mapped);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $" {ex.Message} :: {ex.InnerException}");
        //    }
        //}

        //[HttpGet("supplier/{supplierId}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> GetOrdersBySupplier(Guid supplierId)
        //{
        //    try
        //    {
        //        // Check if user has admin role
        //        if (!User.IsInRole("Admin"))
        //            return Forbid();

        //        // Get all orders with their items
        //        //var allOrders = await _uow.OrderRepository.GetAllAsyncInclude(
        //        //    includeProperties: "OrderItems,OrderItems.Product"
        //        //);

        //        var allOrders = await _uow.OrderRepository.GetAllAsync(
        //            query => query
        //                .Include(o => o.OrderItems)
        //                .ThenInclude(oi => oi.Product)
        //                .ThenInclude(p => p.ProductSuppliers)
        //        );


        //        // Filter orders that contain products from this supplier
        //        var supplierOrders = allOrders.Where(order =>
        //            order.OrderItems.Any(item =>
        //                item.Product != null &&
        //                item.Product.ProductSuppliers.Any(ps => ps.SupplierId == supplierId)
        //            )
        //        ).ToList();

        //        if (!supplierOrders.Any())
        //            return NotFound($"No orders found for supplier {supplierId}");

        //        var mapped = _mapper.Map<IEnumerable<OrderResDto>>(supplierOrders);

        //        return Ok(mapped);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $" {ex.Message} :: {ex.InnerException}");
        //    }
        //}

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
