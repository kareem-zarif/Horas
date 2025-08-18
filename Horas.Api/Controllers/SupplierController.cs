namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public SupplierController(IUOW uow, IMapper mapper, IMediator mediator)
        {
            _uow = uow;
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var found = await _uow.SupplierRepository.GetAllAsyncInclude();

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<SupplierResDto>>(found);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetSupplier(Guid id)
        {
            try
            {
                var found = await _uow.SupplierRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<SupplierResDto>(found);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //from Accoount/Register Customer
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] SupplierRegisterDto supplierCreateDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var supplier = _mapper.Map<Supplier>(supplierCreateDto);

        //    var created = await _uow.SupplierRepository.CreateAsync(supplier);

        //    var saved = await _uow.Complete();
        //    if (saved > 0)
        //    {
        //        await _mediator.Publish(new NotificationEvent(
        //           message: $"Welcome  '{supplier.FirstName} your account has been added successfully'",
        //           personId: supplier.Id
        //         ));

        //        var mapped = _mapper.Map<SupplierResDto>(created);
        //        return Ok(mapped);
        //    }
        //    else
        //        return BadRequest();
        //}


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] SupplierUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var found = await _uow.SupplierRepository.GetAsync(requestDto.Id);

            if (found == null)
                return NotFound();

            //var supplier = _mapper.Map<Supplier>(requestDto);
            _mapper.Map(requestDto, found); // ← عدّل على نفس الكائن

            var updated = await _uow.SupplierRepository.UpdateAsync(found);

            if (updated == null)
                return NotFound();

            var saved = await _uow.Complete();
            if (saved > 0)
            {
                await _mediator.Publish(new NotificationEvent(
                    message: $"Your Acc '{found.FirstName}' has been successfully updated.",
                    personId: found.Id
                ));

                var mapped = _mapper.Map<SupplierResDto>(updated);
                return Ok(mapped);
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.SupplierRepository.DeleteAsync(id);

                var saved = await _uow.Complete();

                if (saved > 0)
                {
                    var mapped = _mapper.Map<SupplierResDto>(deleted);
                    return Ok(mapped);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
