namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public PaymentMethodController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentMethodCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var paymentMethod = _mapper.Map<PaymentMethod>(dto);


            var created = await _uow.PaymentMethodRepository.CreateAsync(paymentMethod);
            int saved = await _uow.Complete();

            if (saved > 0)
            {
                var mapped = _mapper.Map<PaymentMethodResDto>(created);
                return Ok(mapped);

            }
            else
                return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var paymentMethod = await _uow.PaymentMethodRepository.GetAsyncInclude(id);

                if (paymentMethod == null)
                    return NotFound();

                var dto = _mapper.Map<PaymentMethodResDto>(paymentMethod);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var paymentMethods = await _uow.PaymentMethodRepository.GetAllAsyncInclude();

                if (paymentMethods == null || !paymentMethods.Any())
                    return NotFound();

                var dtos = _mapper.Map<IEnumerable<PaymentMethodResDto>>(paymentMethods);
                return Ok(dtos);

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

                var paymentMethod = await _uow.PaymentMethodRepository.GetAsync(id);

                if (paymentMethod == null)
                    return NotFound();

                var deleted = await _uow.PaymentMethodRepository.DeleteAsync(id);

                var saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<PaymentMethodResDto>(deleted);
                    return Ok(mapped);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the payment method.");
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PaymentMethodUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var paymentMethod = await _uow.PaymentMethodRepository.GetAsync(dto.Id);

            if (paymentMethod == null)
                return NotFound();

            var mappingGo = _mapper.Map<PaymentMethod>(dto);

            var updated = await _uow.PaymentMethodRepository.UpdateAsync(mappingGo);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var result = _mapper.Map<PaymentMethodResDto>(paymentMethod);
                return Ok(result);
            }
            else
            {
                return BadRequest("No changes were saved.");
            }
        }

    }
}
