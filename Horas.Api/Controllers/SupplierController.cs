using AutoMapper;
using Horas.Api.Dtos.Supplier;
using Horas.Domain;
using Horas.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public SupplierController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var found = await _uow.SupplierRepository.GetAllAsyncInclude();

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<SupplierResDto>>(found);

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
        public async Task<ActionResult> GetSupplier(Guid id)
        {
            try
            {
                var found = await _uow.SupplierRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<SupplierResDto>(found);

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
        public async Task<IActionResult> Create([FromBody] SupplierCreateDto supplierCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var supplier = _mapper.Map<Supplier>(supplierCreateDto);

            var created = await _uow.SupplierRepository.CreateAsync(supplier);
            
            var saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<SupplierResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] SupplierUpdateDto supplierUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != supplierUpdateDto.Id)
                return BadRequest("ID mismatch");

            var found = await _uow.SupplierRepository.GetAsyncInclude(id);

            if (found == null)
                return NotFound();

            var supplier = _mapper.Map(supplierUpdateDto, found);

            var updated = await _uow.SupplierRepository.UpdateAsync(supplier);

            if (updated == null)
                return NotFound();

            var saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<SupplierResDto>(updated);
                return Ok(mapped);
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _uow.SupplierRepository.DeleteAsync(id);

                var saved = await _uow.Complete();

                if (saved > 0)
                {
                    var mapped = _mapper.Map<SupplierResDto>(deleted);
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
