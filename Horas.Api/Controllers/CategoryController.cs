

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public CategoryController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult>  GetById(Guid id)
        //{

        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var foundList = await _uow.CategoryRepository.GetAllAsync();
                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable< CategoryResDto>>(foundList);

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
        public async Task<ActionResult> Create(CategoryCreateDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requestDto == null)
                    return BadRequest();

                var mappedGo = _mapper.Map<Category>(requestDto);
                var created = await _uow.CategoryRepository.CreateAsync(mappedGo);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mappedCome = _mapper.Map<CategoryResDto>(created);
                    return Ok(mappedCome);
                }
                else return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
                var mappedGo = _mapper.Map<Category>(requestDto);
                var updated = await _uow.CategoryRepository.UpdateAsync(mappedGo);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mappedCome = _mapper.Map<CategoryResDto>(updated);
                    return Ok(mappedCome);
                }
                else return BadRequest();
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.CategoryRepository.DeleteAsync(id);
                if (deleted == null)
                {
                    return BadRequest($"No entity found with id: {id}");
                }
                int saved = await _uow.Complete();
                Console.WriteLine($"Saved result: {saved}");
                if (saved > 0)
                {
                    var mapped = _mapper.Map<CategoryResDto>(deleted);
                    return Ok(mapped);
                }
                else return BadRequest("Could not delete the entity.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

