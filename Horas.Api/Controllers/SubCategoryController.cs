
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
                var foundList = await _uow.SubCategoryRepository.GetAllAsync();

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
                var found = await _uow.SubCategoryRepository.GetAsync(id);

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
        public async Task<ActionResult> Create(SubCategoryCreateDto requestDto)
        {
            if (requestDto == null)
                return BadRequest();

            try
            {
                var subCategory = _mapper.Map<SubCategory>(requestDto);

                if (subCategory == null)
                    return BadRequest();

                await _uow.SubCategoryRepository.CreateAsync(subCategory);
                await _uow.Complete();

                var mapped = _mapper.Map<SubCategoryResDto>(subCategory);

                return CreatedAtAction(nameof(GetSubCategories), new { id = subCategory.Id }, mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SubCategoryUpdateDto requestDto)
        {
            if (requestDto == null || id != requestDto.Id)
                return BadRequest();
            try
            {
                var subCategory = await _uow.SubCategoryRepository.GetAsync(id);

                if (subCategory == null)
                    return NotFound();

                subCategory.Name = requestDto.Name;
                subCategory.Description = requestDto.Description;
                subCategory.CategoryId = requestDto.CategoryId;

                    await _uow.SubCategoryRepository.UpdateAsync(subCategory);
                await _uow.Complete();

                var mapped = _mapper.Map<SubCategoryResDto>(subCategory);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

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
                var subCategory = await _uow.SubCategoryRepository.GetAsync(id);

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

