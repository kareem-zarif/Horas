using Address = Horas.Domain.Address;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AddressController(IUOW uow, IMapper mapper, IMediator mediator)
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
        public async Task<IActionResult> Create([FromBody] AddressCreateDto requestDto)
        {

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState Errors:");
                foreach (var error in ModelState)
                    Console.WriteLine($"Field: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");

                return BadRequest(ModelState);
            }

            var found = _mapper.Map<Address>(requestDto);

            if (found == null)
                return BadRequest();

            var created = await _uow.AddressRepository.CreateAsync(found);

            var saved = await _uow.Complete();
            if (saved > 0)
            {
                await _mediator.Publish(new NotificationEvent(

                  message: $"Your address has been added successfully  ",
                  personId: created.PersonId

                ));

                var mapped = _mapper.Map<AddressResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AddressUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var found = await _uow.AddressRepository.GetAsync(requestDto.Id);

            if (found == null)
                return NotFound();

            _mapper.Map(requestDto, found);

            var updated = await _uow.AddressRepository.UpdateAsync(found);

            var saved = await _uow.Complete();
            if (saved > 0)
            {

                await _mediator.Publish(new NotificationEvent(

                  message: $"Your Address Has Been Updated Successfully ",
                  personId: updated.PersonId

                ));
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
                var found = await _uow.AddressRepository.DeleteAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var deleted = await _uow.AddressRepository.DeleteAsync(id);

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
