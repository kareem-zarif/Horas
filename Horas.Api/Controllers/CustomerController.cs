

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public CustomerController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
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
        [HttpPost]
        public async Task<IActionResult> create([FromForm] CustomerCreateDto _CreateDto)
        {
            if(!ModelState.IsValid)
            return BadRequest();
            var Customer = _mapper.Map<Customer>(_CreateDto);
           var created =await _uow.CustomerRepository.CreateAsync(Customer);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CustomerResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();

        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var mappedGo = _mapper.Map<Customer>(requestDto);
            var updated = await _uow.CustomerRepository.UpdateAsync(mappedGo);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
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
                else return BadRequest();
}
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}






 

