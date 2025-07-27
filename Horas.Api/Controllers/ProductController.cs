
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


        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(Guid id)
        {
            try
            {
                var found = await _uow.PrdouctRepository.GetAsync(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<ProductResDto>(found);

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
        public async Task<ActionResult> Create([FromForm] ProductCreateDto requestDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(requestDto);

            if (requestDto.Images != null && requestDto.Images.Count > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                foreach (var image in requestDto.Images)
                {
                    if (image != null && image.Length > 0)
                    {
                        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        var relativePath = $"/uploads/{uniqueFileName}";
                        product.ProductPicsPathes.Add(relativePath);
                    }
                }
            }

            var created = await _uow.PrdouctRepository.CreateAsync(product);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<ProductResDto>(created);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }



        //[HttpPost]
        //public async Task<ActionResult> Create(ProductCreateDto requestDto)
        //{
        //    try
        //    {
        //        if (requestDto == null)
        //            return BadRequest();

        //        var mappedGo = _mapper.Map<Product>(requestDto);

        //        var created = await _uow.PrdouctRepository.CreateAsync(mappedGo);

        //        int saved = await _uow.Complete();
        //        if (saved > 0)
        //        {
        //            var mappedCome = _mapper.Map<ProductResDto>(created);
        //            return Ok(mappedCome);
        //        }
        //        else return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}



        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromForm] ProductUpdateDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var found = await _uow.PrdouctRepository.GetAsync(requestDto.Id);

            _mapper.Map(requestDto, found);

            if (requestDto.Images != null && requestDto.Images.Count > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (found.ProductPicsPathes.Any())
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", found.ProductPicsPathes.First().TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    found.ProductPicsPathes.Clear();
                }

                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(requestDto.Images.FirstOrDefault().FileName)}";
                var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await requestDto.Images.FirstOrDefault()?.CopyToAsync(stream);
                }

                found.ProductPicsPathes.Add($"/uploads/{uniqueFileName}");
            }

            var updated = await _uow.PrdouctRepository.UpdateAsync(found);
            int saved = await _uow.Complete();
            if (saved > 0)
            {
                var mapped = _mapper.Map<ProductResDto>(updated);
                return Ok(mapped);
            }
            else
                return BadRequest();
        }


        //[HttpPut]
        //public async Task<IActionResult> Update(ProductUpdateDto requestDto)
        //{
        //    try
        //    {
        //        var mappedGo = _mapper.Map<Product>(requestDto);
        //        var updated = await _uow.PrdouctRepository.UpdateAsync(mappedGo);
        //        int saved = await _uow.Complete();
        //        if (saved > 0)
        //        {
        //            var mappedCome = _mapper.Map<ProductResDto>(updated);
        //            return Ok(mappedCome);
        //        }
        //        else return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var found = await _uow.PrdouctRepository.GetAsync(id);

                if (found == null)
                    return NotFound("Product not found");

                if (found.ProductPicsPathes != null && found.ProductPicsPathes.Any())
                {
                    foreach (var imagePath in found.ProductPicsPathes)
                    {
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }

                var deleted = await _uow.PrdouctRepository.DeleteAsync(id);

                int saved = await _uow.Complete();
                if (saved > 0)
                {
                    var mapped = _mapper.Map<ProductResDto>(deleted);
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


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    try
        //    {
        //        var deleted = await _uow.PrdouctRepository.DeleteAsync(id);
        //        int saved = await _uow.Complete();
        //        if (saved > 0)
        //        {
        //            var mapped = _mapper.Map<ProductResDto>(deleted);
        //            return Ok(mapped);
        //        }
        //        else return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
