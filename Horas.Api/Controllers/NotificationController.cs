
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public NotificationController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {

            try
            {

                var foundList = await _uow.NotificationRepository.GetAllAsyncInclude();
                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<NotificationResDto>>(foundList);

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

        public async Task<IActionResult> GetNotification(Guid id)
        {
            try
            {
                var found = await _uow.NotificationRepository.GetAsyncInclude(id);
                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<NotificationResDto>(found);

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
        public async Task<IActionResult> create([FromForm] NotificationCreateDto _CreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var notification = _mapper.Map<Notification>(_CreateDto);
            var created = await _uow.NotificationRepository.CreateAsync(notification);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<NotificationResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();

        }

        [HttpPut]

        public async Task<IActionResult> Update(NotificationUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var mappedGo = _mapper.Map<Notification>(requestDto);
            var updated = await _uow.NotificationRepository.UpdateAsync(mappedGo);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mappedCome = _mapper.Map<NotificationResDto>(updated);
                return Ok(mappedCome);
            }
            else return BadRequest();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.NotificationRepository.DeleteAsync(id);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<NotificationResDto>(deleted);
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
