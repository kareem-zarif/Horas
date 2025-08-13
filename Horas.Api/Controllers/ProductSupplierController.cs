

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSupplierController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public ProductSupplierController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var found = await _uow.ProductSupplierRepository.GetAllAsyncInclude();

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<ProductSupplierResDto>>(found);

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductSupplier(Guid id)
        {
            try
            {
                var found = await _uow.ProductSupplierRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<ProductSupplierResDto>(found);

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
        public async Task<IActionResult> Create([FromForm] ProductSupplierCreateDto productSupplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var productSupplier = _mapper.Map<ProductSupplier>(productSupplierDto);

            var created = await _uow.ProductSupplierRepository.CreateAsync(productSupplier);

            var saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<ProductSupplierResDto>(created);
                return Ok(mapped);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ProductSupplierUpdateDto productSupplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (id != productSupplierDto.Id)
                return BadRequest("Id mismatch");

            var productSupplier = _mapper.Map<ProductSupplier>(productSupplierDto);

            //productSupplier.Id = id;

            var updated = await _uow.ProductSupplierRepository.UpdateAsync(productSupplier);

            var saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<ProductSupplierResDto>(updated);
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
                var deleted = await _uow.ProductSupplierRepository.DeleteAsync(id);

                var saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<ProductSupplierResDto>(deleted);
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
