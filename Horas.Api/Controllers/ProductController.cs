using AutoMapper;
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
                return Ok(foundList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
