// Do not add to globbal usings
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CustomerController(IUOW uow, IMapper mapper, IMediator mediator)
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

                var foundList = await _uow.CustomerRepository.GetAllAsyncInclude();
                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<CustomerResDto>>(foundList);

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

        public async Task<IActionResult> GetCustomer(Guid id)
        {
            try
            {
                var found = await _uow.CustomerRepository.GetAsyncInclude(id);
                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<CustomerResDto>(found);

                if (mapped == null)
                    return NotFound();

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //post from Account / Post / Supplier
        //[HttpPost]
        //public async Task<IActionResult> create([FromForm] CustomerCreateDto _CreateDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var Customer = _mapper.Map<Customer>(_CreateDto);
        //    var created = await _uow.CustomerRepository.CreateAsync(Customer);
        //    int saved = await _uow.Complete();
        //    if (saved > 0)
        //    {
        //        await _mediator.Publish(new NotificationEvent(
        //        message: $"Welcome Your account has been created successfully ",
        //        personId: created.Id

        //      ));
        //        var mapped = _mapper.Map<CustomerResDto>(created);
        //        return Ok(mapped);
        //    }
        //    else
        //        return BadRequest();

        //}

        [HttpPut]
        public async Task<IActionResult> Update(CustomerUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var old = await _uow.CustomerRepository.GetAsync(requestDto.Id);
            if (old == null)
                return NotFound();

            _mapper.Map(requestDto, old);
            var updated = await _uow.CustomerRepository.UpdateAsync(old);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                await _mediator.Publish(new NotificationEvent(
                 message: "Your profile has been updated successfully.",
                 personId: updated.Id

               ));
                var mappedCome = _mapper.Map<CustomerResDto>(updated);
                return Ok(mappedCome);
            }
            else return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.CustomerRepository.DeleteAsync(id);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<CustomerResDto>(deleted);
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








