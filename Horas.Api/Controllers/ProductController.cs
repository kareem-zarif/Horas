using AutoMapper;
using Horas.Api.Dtos.Product;
using Horas.Domain;
using Horas.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public ProductController(IUOW uow, IMapper mapper)
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
                var foundList = await _uow.PrdouctRepository.GetAllAsync();
                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<ProductResDto>>(foundList);

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
        public async Task<ActionResult> Create(ProductCreateDto requestDto)
        {
            try
            {
                if (requestDto == null)
                    return BadRequest();

                var mappedGo = _mapper.Map<Product>(requestDto);
                var created = await _uow.PrdouctRepository.CreateAsync(mappedGo);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mappedCome = _mapper.Map<ProductResDto>(created);
                    return Ok(mappedCome);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto requestDto)
        {
            try
            {
                var mappedGo = _mapper.Map<Product>(requestDto);
                var updated = await _uow.PrdouctRepository.UpdateAsync(mappedGo);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mappedCome = _mapper.Map<ProductResDto>(updated);
                    return Ok(mappedCome);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.PrdouctRepository.DeleteAsync(id);
                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<ProductResDto>(deleted);
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
