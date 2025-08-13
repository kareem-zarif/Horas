using Horas.Api.Dtos.CustomerNotification;
using Horas.Data.Repos;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class PersonNotificationController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public PersonNotificationController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {

                var foundList = await _uow.PersonNotificationRepository.GetAllAsyncInclude();
                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<PersonNotificationRespDto>>(foundList);

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
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var found = await _uow.PersonNotificationRepository.GetAsyncInclude(id);
                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<PersonNotificationRespDto>(found);

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
        public async Task<IActionResult> create([FromForm] PersonNotificationCreateDto _CreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var Customer = _mapper.Map<PersonNotification>(_CreateDto);
            var created = await _uow.PersonNotificationRepository.CreateAsync(Customer);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<PersonNotificationRespDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();

        }

        [HttpPut]
        public async Task<IActionResult> Update(PersonNotificationUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var old = await _uow.PersonNotificationRepository.GetAsync(requestDto.Id);
            if (old == null)
                return NotFound();

            _mapper.Map(requestDto, old);
            var updated = await _uow.PersonNotificationRepository.UpdateAsync(old);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mappedCome = _mapper.Map<PersonNotificationRespDto>(updated);
                return Ok(mappedCome);
            }
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.PersonNotificationRepository.DeleteAsync(id);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<PersonNotificationRespDto>(deleted);
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
