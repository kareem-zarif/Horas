using Stripe.Checkout;

namespace Horas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private const string WebhookSecret = "whsec_4ac24f63a44ee4d412aa6c8fa7b88b6d277798d8d4025bb75c02b6e2659c48cd";
        private readonly IUOW _uow;
        private readonly StripeSettings _stripeSettings;
        private readonly IConfiguration _config;

        public StripeController(IUOW uow, IConfiguration config, StripeSettings stripeSettings)
        {
            _uow = uow;
            _config = config;
            _stripeSettings = stripeSettings;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CreateCheckoutSession([FromForm] string orderId, [FromForm] string paymentMethodId = null)
        {
            var order = await _uow.OrderRepository.GetAsyncInclude(Guid.Parse(orderId));
            if (order == null) return NotFound();

            // Use paymentMethodId from request if provided, otherwise fall back to order's PaymentMethodId
            Guid? effectivePaymentMethodId = paymentMethodId != null ? Guid.Parse(paymentMethodId) : order.PaymentMethodId;

            if (!effectivePaymentMethodId.HasValue)
                return BadRequest("No valid payment method ID provided");


            var foundPaymentMethod = await _uow.PaymentMethodRepository.GetAsyncByExpression(x => x.Id == effectivePaymentMethodId.Value);

            if (foundPaymentMethod == null)
            {
                return BadRequest("No payment method found for this order");
            }

            if (foundPaymentMethod.PaymentType != PaymentMethodType.Cash && foundPaymentMethod.PaymentType != PaymentMethodType.Stripe)
            {
                return BadRequest("Unsupported payment method type");
            }

            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                return BadRequest("Order has no items");
            }

            if (order.OrderItems == null || !order.OrderItems.Any())
                return BadRequest("Order has no items");

            var domain = _config["AppSettings:Domain"] ?? "http://localhost:4200";//where frontend display
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(order.TotalAmount * 100), // Convert to cents
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Order #" + order.Id
                        }
                    },
                    Quantity = 1
                }

            },
                Mode = "payment",
                SuccessUrl = $"{domain}/success?orderId={orderId}",
                CancelUrl = $"{domain}/cancel",
                CustomerEmail = order.Customer?.Email,
                Metadata = new Dictionary<string, string> { { "orderId", orderId } }
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { sessionId = session.Id, redirectionUrl = session.Url });
        }


        //[HttpPost("checkout")]
        //public async Task<IActionResult> CreateCheckoutSession([FromForm] Guid orderId)
        //{
        //    var order = await _uow.OrderRepository.GetAsync(o => o.Id == orderId,
        //        includeProperties: "OrderItems,OrderItems.Product,Customer");

        //    if (order == null)
        //        return NotFound("Order not found");

        //    var foundPaymentMethod = await _uow.PaymentMethodRepository.GetAsync(pm => pm.Id == order.PaymentMethodId);

        //    if (foundPaymentMethod == null || foundPaymentMethod.PaymentType != PaymentMethodType.Stripe)
        //        return BadRequest("Invalid or unsupported payment method");

        //    if (order.OrderItems == null || !order.OrderItems.Any())
        //        return BadRequest("Order has no items");


        //    var lineItems = order.OrderItems.Select(oi => new SessionLineItemOptions
        //    {
        //        PriceData = new SessionLineItemPriceDataOptions
        //        {
        //            Currency = "EGP",
        //            ProductData = new SessionLineItemPriceDataProductDataOptions
        //            {
        //                Name = oi.Product.Name,
        //                Description = oi.Product.Description,
        //            },
        //            UnitAmount = (long)(oi.UnitPrice * 100)
        //        },
        //        Quantity = oi.Quantity,

        //    }).ToList();


        //    if (!lineItems.Any())
        //        return BadRequest("No valid line items to process");


        //    var origin = $"{Request.Scheme}://{Request.Host}";
        //    var options = new SessionCreateOptions
        //    {
        //        PaymentMethodTypes = new List<string> { "card" },
        //        LineItems = lineItems,
        //        Mode = "payment",
        //        //SuccessUrl = $"{origin}/confirmation?orderId={orderId}",
        //        //CancelUrl = $"{origin}/cancel?orderId={orderId}",
        //        SuccessUrl = $"{origin}/confirmation.html",
        //        CancelUrl = $"{origin}/cancel.html",
        //        CustomerEmail = order.Customer?.Email,
        //        Metadata = new Dictionary<string, string> { { "orderId", orderId.ToString() } }
        //    };

        //    var service = new SessionService();
        //    var session = await service.CreateAsync(options);

        //    return Ok(new { sessionId = session.Id, redirectionUrl = session.Url });
        //}


        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WebhookSecret);

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = (Session)stripeEvent.Data.Object;

                if (session == null || !session.Metadata.ContainsKey("orderId") || !Guid.TryParse(session.Metadata["orderId"], out var orderId))
                    return BadRequest("Invalid or missing orderId in session metadata");

                var order = await _uow.OrderRepository.GetAsync(o => o.Id == orderId);

                if (order == null)
                    return NotFound("Order not found");

                order.OrderStatus = OrderStatus.Confirmed;

                await _uow.OrderStatusHistoryRepository.CreateAsync(new OrderStatusHistory
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    OrderStatus = OrderStatus.Confirmed,
                    CreatedOn = DateTime.UtcNow,
                    IsExist = true
                });
                await _uow.Complete();
            }
            return Ok();
        }

    }
}