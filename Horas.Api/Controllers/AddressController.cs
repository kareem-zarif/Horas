using Address = Horas.Domain.Address;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public AddressController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var found = await _uow.AddressRepository.GetAllAsyncInclude();

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<AddressResDto>>(found);

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
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var foundItem = await _uow.AddressRepository.GetAsyncInclude(id);

                if (foundItem == null)
                    return NotFound();

                var mapped = _mapper.Map<AddressResDto>(foundItem);

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
        public async Task<IActionResult> Create([FromForm] AddressCreateDto requestDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();


            var found = _mapper.Map<Address>(requestDto);

            if (found == null)
                return BadRequest();

            var created = await _uow.AddressRepository.CreateAsync(found);

            var saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<AddressResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] AddressUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var found = await _uow.AddressRepository.GetAsyncInclude(requestDto.Id);

            if (found == null)
                return NotFound();

            _mapper.Map(requestDto, found);

            var updated = await _uow.AddressRepository.UpdateAsync(found);

            var saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<AddressResDto>(updated);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var found = await _uow.AddressRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var deleted = await _uow.AddressRepository.DeleteAsyncInclude(id);

                var saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<AddressResDto>(deleted);
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
