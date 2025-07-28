namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public SubCategoryController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetSubCategories()
        {
            try
            {
                var foundList = await _uow.SubCategoryRepository.GetAllAsyncInclude();

                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<SubCategoryResDto>>(foundList);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubCategory(Guid id)
        {
            try
            {
                var found = await _uow.SubCategoryRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<SubCategoryResDto>(found);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromForm] SubCategoryCreateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var found = _mapper.Map<SubCategory>(requestDto);

            var mappedGo = await _uow.SubCategoryRepository.CreateAsync(found);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<SubCategoryResDto>(found);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] SubCategoryUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var old = await _uow.SubCategoryRepository.GetAsyncInclude(requestDto.Id);

            if (old == null)
                return NotFound();

            var mappedCategory = _mapper.Map<SubCategory>(requestDto);

            var mappedGo = await _uow.SubCategoryRepository.UpdateAsync(mappedCategory);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<SubCategoryResDto>(mappedGo);
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
                var deleted = await _uow.SubCategoryRepository.DeleteAsync(id);

                if (deleted == null)
                    return NotFound();

                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<SubCategoryResDto>(deleted);
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

