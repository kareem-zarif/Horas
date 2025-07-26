using AutoMapper;
using Horas.Api.Dtos.Cart;
using Horas.Domain;
using Horas.Domain.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public CartController(IUOW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var foundList = await _uow.CartRepository.GetAllAsyncInclude();

                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<CartResDto>>(foundList);

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
        public async Task<ActionResult> GetCart(Guid id)
        {
            try
            {
                var found = await _uow.CartRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<CartResDto>(found);

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
        public async Task<IActionResult> CreateCart([FromBody] CartCreateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = _mapper.Map<Cart>(requestDto);

            var created = await _uow.CartRepository.CreateAsync(cart);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(Guid id, [FromBody] CartUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var found = await _uow.CartRepository.GetAsyncInclude(id);

            if (found == null)
                return NotFound();

            var cart = _mapper.Map<Cart>(requestDto);

            cart.Id = id;

            var updated = await _uow.CartRepository.UpdateAsync(cart);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartResDto>(updated);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var found = await _uow.CartRepository.GetAsyncInclude(id);

            if (found == null)
                return NotFound();

            var deleted = await _uow.CartRepository.DeleteAsync(id);

            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<CartResDto>(deleted);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }
    }
}
