
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public ReviewController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {

            try
            {

                var foundList = await _uow.ReviewRepository.GetAllAsyncInclude();
                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<ReviewResDto>>(foundList);

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

        public async Task<IActionResult> GetReview(Guid id)
        {
            try
            {
                var found = await _uow.ReviewRepository.GetAsync(id);
                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<ReviewResDto>(found);

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
        public async Task<IActionResult> create([FromBody] ReviewCreateDto _CreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var Review = _mapper.Map<Review>(_CreateDto);
            var created = await _uow.ReviewRepository.CreateAsync(Review);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<ReviewResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();

        }

        [HttpPut]
        public async Task<IActionResult> Update(ReviewUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var mappedGo = _mapper.Map<Review>(requestDto);
            var updated = await _uow.ReviewRepository.UpdateAsync(mappedGo);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mappedCome = _mapper.Map<ReviewResDto>(updated);
                return Ok(mappedCome);
            }
            else return BadRequest();


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.ReviewRepository.DeleteAsync(id);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<ReviewResDto>(deleted);
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
