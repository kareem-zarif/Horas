
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
        public async Task<ActionResult> GetSubCategory(Guid id)
        {
            try
            {
                var found = await _uow.SubCategoryRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<SubCategoryResDto>(found);

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
        public async Task<ActionResult> Create([FromForm]SubCategoryCreateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var found = _mapper.Map<SubCategory>(requestDto);

            if (found == null)
                return BadRequest();

            await _uow.SubCategoryRepository.CreateAsync(found);
            await _uow.Complete();

            var mapped = _mapper.Map<SubCategoryResDto>(found);

            return CreatedAtAction(nameof(GetSubCategory), new { id = mapped.Id }, mapped);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] Guid id, [FromForm] SubCategoryUpdateDto requestDto)
        {
            if (!ModelState.IsValid || id != requestDto.Id)
                return BadRequest(ModelState);

            var subCategory = await _uow.SubCategoryRepository.GetAsyncInclude(id);

            if (subCategory == null)
                return NotFound();

            var mappedCategory = _mapper.Map<SubCategory>(subCategory);

            await _uow.SubCategoryRepository.UpdateAsync(mappedCategory);
            await _uow.Complete();

            var mapped = _mapper.Map<SubCategoryResDto>(mappedCategory);
            return Ok(mapped);


            //==================================================================================

            //if (requestDto == null)
            //    return BadRequest();

            //if (id != requestDto.Id)
            //    return BadRequest("Id does not match object Id");

            //try
            //{
            //    var SubCategoryFromDb = await _uow.SubCategoryRepository.GetAsync(id);

            //    if (SubCategoryFromDb == null)
            //        return NotFound();

            //    var subCategory = _mapper.Map<SubCategory>(SubCategoryFromDb);

            //    if (subCategory == null)
            //        return BadRequest();

            //    await _uow.SubCategoryRepository.UpdateAsync(subCategory);
            //    await _uow.Complete();

            //    var mapped = _mapper.Map<SubCategoryResDto>(subCategory);

            //    return Ok(mapped);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var subCategory = await _uow.SubCategoryRepository.GetAsyncInclude(id);

                if (subCategory == null)
                    return NotFound();

                await _uow.SubCategoryRepository.DeleteAsync(id);

                await _uow.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

