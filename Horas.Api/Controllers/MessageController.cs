
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public MessageController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]

        public async Task<IActionResult> GetAll()
        {

            try
            {

                var foundList = await _uow.MessageRepository.GetAllAsync();
                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<MessageReadDto>>(foundList);

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

        public async Task<IActionResult> GetMessage(Guid id)
        {
            try
            {
                var found = await _uow.MessageRepository.GetAsync(id);
                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<MessageReadDto>(found);

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
        public async Task<IActionResult> create([FromForm] MessageCreateDto _CreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var Message = _mapper.Map<Message>(_CreateDto);
            var created = await _uow.MessageRepository.CreateAsync(Message);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<MessageReadDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();

        }

        [HttpPut]
        public async Task<IActionResult> Update(MessageUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var mappedGo = _mapper.Map<Message>(requestDto);
            var updated = await _uow.MessageRepository.UpdateAsync(mappedGo);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mappedCome = _mapper.Map<MessageReadDto>(updated);
                return Ok(mappedCome);
            }
            else return BadRequest();


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.MessageRepository.DeleteAsync(id);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<MessageReadDto>(deleted);
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
