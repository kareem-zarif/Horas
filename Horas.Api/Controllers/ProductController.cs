using Microsoft.TeamFoundation.Common;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using Product = Horas.Domain.Product;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IMediator _madiator;
        public ProductController(IUOW uow, IMapper mapper, IMediator madiator, IConfiguration config)
        {
            _uow = uow;
            _mapper = mapper;
            _madiator = madiator;
            _config = config;
        }




        //        [HttpGet("search")]
        //        public async Task<IActionResult> Search([FromQuery] string q)
        //        {
        //            if (string.IsNullOrWhiteSpace(q))
        //                return BadRequest("Search query is required.");

        //            // 1. Get all product names
        //            var productNames = (await _uow.ProductRepository.GetAllAsync())
        //                .Where(p => p.IsExist)
        //                .Select(p => p.Name)
        //                .ToList();

        //            if (!productNames.Any())
        //                return NotFound("No products found.");

        //            // 2. Prepare prompt for GPT
        //            //    string prompt = $@"
        //            //You are a smart autocomplete system for an eCommerce website.
        //            //Given a user input: '{q}'
        //            //And the available product names: [{string.Join(", ", productNames)}]
        //            //Return the most relevant product names as autocomplete suggestions.
        //            //Respond with only the names in a JSON array.";
        //            string prompt = $"""
        //You are an AI-powered autocomplete assistant for a large multilingual e-commerce platform that serves both B2C (individuals) and B2B (factories, suppliers).

        //Your task is to help users find the exact products they are likely searching for, based on a large product catalog that includes electronics, clothing, furniture, industrial tools, food, cosmetics, etc.

        //The platform supports both Arabic and English.

        //Here is a sample list of product names:
        //[{string.Join(", ", productNames.Take(1000))}]

        //A user typed: "{q}"

        //Your job is to:
        //1. Understand the user’s intent even if they only typed 2-3 letters.
        //2. Support Arabic and English input equally.
        //3. Return only the most relevant product suggestions from the list.
        //4. Do NOT return unrelated products.
        //5. Include exact matches, partial matches, possible typos, and related terms — only if they make sense.
        //6. If no reasonable match exists, return an empty list.

        //Return a clean JSON array of up to 10 product name suggestions only — no extra explanation.
        //""";


        //            // 3. Prepare OpenAI request
        //            var openAiApiKey = _config["OpenAI:ApiKey"];
        //            var requestBody = new
        //            {
        //                //  model = "gpt-3.5-turbo", // or "gpt-4" if you have access
        //                model = "mistralai/mistral-7b-instruct",                         //  model = "gpt-4o-mini",
        //                messages = new[]
        //                {
        //                    new { role = "system", content = "You are a helpful assistant." },
        //                    new { role = "user", content = prompt }
        //                },
        //                temperature = 0.2
        //            };

        //            using var client = new HttpClient();
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAiApiKey);

        //            var response = await client.PostAsync(
        //             //   "https://api.openai.com/v1/chat/completions",
        //           "https://openrouter.ai/api/v1/chat/completions",
        //                new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
        //            );

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                var error = await response.Content.ReadAsStringAsync();
        //                return StatusCode((int)response.StatusCode, $"OpenAI error: {error}");
        //            }

        //            var responseContent = await response.Content.ReadAsStringAsync();

        //            using var doc = JsonDocument.Parse(responseContent);
        //            var completion = doc.RootElement
        //                .GetProperty("choices")[0]
        //                .GetProperty("message")
        //                .GetProperty("content")
        //                .GetString();

        //            try
        //            {
        //                var cleaned = completion.Trim().Trim('`');
        //                if (cleaned.StartsWith("json", StringComparison.OrdinalIgnoreCase))
        //                    cleaned = cleaned.Substring(4).Trim();

        //                // نحاول نقرأ الـ JSON كـ object
        //                var json = JsonDocument.Parse(cleaned);
        //                if (json.RootElement.TryGetProperty("suggestions", out var suggestionsElement) && suggestionsElement.ValueKind == JsonValueKind.Array)
        //                {
        //                    var suggestions = suggestionsElement.EnumerateArray().Select(x => x.GetString()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        //                    return Ok(suggestions);
        //                }

        //                throw new Exception("suggestions not found");
        //            }
        //            catch
        //            {
        //                return Ok(new { raw = completion, fullResponse = responseContent }); // fallback
        //            }

        //        }





        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Search query is required.");

            // 1. Get all product names
            var productNames = (await _uow.ProductRepository.GetAllAsync())
                .Where(p => p.IsExist)
                .Select(p => p.Name)
                .ToList();

            if (!productNames.Any())
                return NotFound("No products found.");

            // 2. Prepare GPT prompt
            string prompt = $"""
You are a smart autocomplete assistant for a multilingual e-commerce platform.

Here is a sample list of product names:
[{string.Join(", ", productNames.Take(1000))}]

User typed: "{q}"

Return the top 10 most relevant product names from the list above, based on exact match, partial match, typos, and user intent.

ONLY return a JSON array of strings like:
["Product A", "Product B"]
No explanation, no extra formatting.
""";

            // 3. Prepare OpenAI request
            var openAiApiKey = _config["OpenAI:ApiKey"];
            var requestBody = new
            {
                model = "mistralai/mistral-7b-instruct",  // Or "gpt-3.5-turbo", "gpt-4o", etc.
                messages = new[]
                {
            new { role = "system", content = "You are a helpful assistant." },
            new { role = "user", content = prompt }
        },
                temperature = 0.2
            };

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAiApiKey);

            var response = await client.PostAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"OpenAI error: {error}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            // 4. Parse result
            using var doc = JsonDocument.Parse(responseContent);
            var completion = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            try
            {
                // Clean result
                var cleaned = completion
                    .Trim()
                    .Trim('`')
                    .Replace("json", "", StringComparison.OrdinalIgnoreCase)
                    .Trim();

                var json = JsonDocument.Parse(cleaned);

                if (json.RootElement.ValueKind == JsonValueKind.Array)
                {
                    var suggestions = json.RootElement
                        .EnumerateArray()
                        .Select(x => x.GetString())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();

                    return Ok(suggestions);
                }

                throw new Exception("Invalid JSON array format.");
            }
            catch
            {
                return Ok(new
                {
                    raw = completion,
                    fullResponse = responseContent
                });
            }
        }







        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var foundList = await _uow.ProductRepository.GetAllAsyncInclude();
                if (foundList == null)
                    return NotFound();

                var mapped = _mapper.Map<IEnumerable<ProductResDto>>(foundList);

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
                var found = await _uow.ProductRepository.GetAsyncInclude(id);

                if (found == null)
                    return NotFound();

                var mapped = _mapper.Map<ProductResDto>(found);

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
            //Guid supplierIdFromDB = new Guid("fabd5059-4879-4ed1-ca38-08ddd201cb0d"); 
            var created = await _uow.ProductRepository.CreateAsync(product);
            int saved = await _uow.Complete();
            //ProductSupplier createdProductSupplier = new ProductSupplier
            //{
            //    ProductId = created.Id,
            //    SupplierId = supplierIdFromDB
            //};
            //await _uow.ProductSupplierRepository.CreateAsync(createdProductSupplier);

            //int saved2 = await _uow.Complete();

            //Product createdInclude;

            if (saved > 0)
            {
                //createdInclude = await _uow.ProductRepository.GetAsyncInclude(created.Id);
                //await _madiator.Publish(new NotificationEvent(
                //    $"A New Product Added: {created.Name}",
                //    supplierIdFromDB
                //    ));

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


        //the one i did
        //[HttpPut]
        //public async Task<IActionResult> Update([FromForm] ProductUpdateDto requestDto)
        //{
        //    ProductApprovalStatus previousStatus = ProductApprovalStatus.Pending;

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var found = await _uow.ProductRepository.GetAsync(requestDto.Id);
        //    if (found == null)
        //        return NotFound();

        //    _mapper.Map(requestDto, found);

        //    if (requestDto.Images != null && requestDto.Images.Count > 0)
        //    {
        //        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        //        if (found.ProductPicsPathes.Any())
        //        {
        //            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", found.ProductPicsPathes.First().TrimStart('/'));
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }

        //            found.ProductPicsPathes.Clear();
        //        }

        //        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(requestDto.Images.FirstOrDefault().FileName)}";
        //        var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        if (!Directory.Exists(uploadsFolder))
        //            Directory.CreateDirectory(uploadsFolder);

        //        using (var stream = new FileStream(newFilePath, FileMode.Create))
        //        {
        //            await requestDto.Images.FirstOrDefault()?.CopyToAsync(stream);
        //        }

        //        found.ProductPicsPathes.Add($"/uploads/{uniqueFileName}");
        //    }

        //    var updated = await _uow.ProductRepository.UpdateAsync(found);
        //    int saved = await _uow.Complete();
        //    if (saved > 0)
        //    {
        //        if (previousStatus != found.ApprovalStatus)
        //        {
        //            string message = null;

        //            if (found.ApprovalStatus == ProductApprovalStatus.Approved)
        //                message = $"Product Approved {found.Name}";

        //            else if (found.ApprovalStatus == ProductApprovalStatus.Rejected)
        //                message = $"Product Rejected {found.Name}";

        //            if (message != null)
        //            {
        //                await _madiator.Publish(new ProductChangedEvent(
        //                    updated.Id,
        //                    message: message
        //                    ));
        //            }
        //        }

        //        var mapped = _mapper.Map<ProductResDto>(updated);
        //        return Ok(mapped);
        //    }
        //    else
        //        return BadRequest();
        //}


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

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateDto requestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Get the existing product to preserve current status and other properties
                var found = await _uow.ProductRepository.GetAsync(requestDto.Id);
                if (found == null)
                    return NotFound("Product not found");

                // Store the previous status BEFORE updating
                ProductApprovalStatus previousStatus = found.ApprovalStatus;

                // Only update the fields that are provided in the request
                // This prevents null reference exceptions during mapping
                if (!string.IsNullOrEmpty(requestDto.Name))
                    found.Name = requestDto.Name;

                if (!string.IsNullOrEmpty(requestDto.Description))
                    found.Description = requestDto.Description;

                if (requestDto.PricePerPiece > 0)
                    found.PricePerPiece = requestDto.PricePerPiece;

                if (requestDto.PricePer50Piece.HasValue && requestDto.PricePer50Piece > 0)
                    found.PricePer50Piece = requestDto.PricePer50Piece;

                if (requestDto.PricePer100Piece.HasValue && requestDto.PricePer100Piece > 0)
                    found.PricePer100Piece = requestDto.PricePer100Piece;

                if (requestDto.NoINStock >= 0)
                    found.NoINStock = requestDto.NoINStock;

                if (requestDto.MinNumToFactoryOrder > 0)
                    found.MinNumToFactoryOrder = requestDto.MinNumToFactoryOrder;

                if (requestDto.ApprovalStatus != ProductApprovalStatus.Pending) // Only update if explicitly set
                    found.ApprovalStatus = requestDto.ApprovalStatus;

                if (requestDto.Shipping > 0)
                    found.Shipping = requestDto.Shipping;

                if (requestDto.SubCategoryId != Guid.Empty)
                    found.SubCategoryId = requestDto.SubCategoryId;

                if (requestDto.WarrantyNMonths.HasValue && requestDto.WarrantyNMonths >= 0)
                    found.WarrantyNMonths = requestDto.WarrantyNMonths;

                // Handle images if provided
                // Fix for CS1503: Argument 1: cannot convert from 'System.Guid' to 'string?'
                // The issue is that `requestDto.SubCategoryId` is of type `Guid`, but the code is treating it as a nullable string.
                // To fix this, we need to ensure that the `SubCategoryId` is converted to a string before assignment.

                if (requestDto.SubCategoryId != Guid.Empty) // Ensure the Guid is not empty
                    found.SubCategoryId = requestDto.SubCategoryId;
                if (requestDto.Images != null && requestDto.Images.Count > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    // Fix for CS1503: Argument 1: cannot convert from 'System.Guid' to 'string?'
                    // The issue is that `requestDto.SubCategoryId` is of type `Guid`, but the code is treating it as a nullable string.
                    // To fix this, we need to ensure that the `SubCategoryId` is converted to a string before assignment.

                    if (requestDto.SubCategoryId != Guid.Empty) // Ensure the Guid is not empty
                        found.SubCategoryId = requestDto.SubCategoryId;
                    // Delete old images if they exist
                    if (found.ProductPicsPathes.Any())
                    {
                        foreach (var oldImagePath in found.ProductPicsPathes)
                        {
                            var fullOldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldImagePath.TrimStart('/'));
                            if (System.IO.File.Exists(fullOldPath))
                            {
                                System.IO.File.Delete(fullOldPath);
                            }
                        }
                        found.ProductPicsPathes.Clear();
                    }

                    // Add new images
                    foreach (var image in requestDto.Images)
                    {
                        if (image != null && image.Length > 0)
                        {
                            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                            var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                            if (!Directory.Exists(uploadsFolder))
                                Directory.CreateDirectory(uploadsFolder);

                            using (var stream = new FileStream(newFilePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            found.ProductPicsPathes.Add($"/uploads/{uniqueFileName}");
                        }
                    }
                }

                // Update the product
                var updated = await _uow.ProductRepository.UpdateAsync(found);
                int saved = await _uow.Complete();

                if (saved > 0)
                {
                    // Check if approval status changed
                    if (previousStatus != found.ApprovalStatus)
                    {
                        string message = null;

                        if (found.ApprovalStatus == ProductApprovalStatus.Approved)
                            message = $"Product Approved: {found.Name}";
                        else if (found.ApprovalStatus == ProductApprovalStatus.Rejected)
                            message = $"Product Rejected: {found.Name}";

                        if (message != null)
                        {
                            await _madiator.Publish(new ProductChangedEvent(
                                updated.Id,
                                message: message
                            ));
                        }
                    }

                    var mapped = _mapper.Map<ProductResDto>(updated);
                    return Ok(mapped);
                }
                else
                    return BadRequest("Failed to save changes");
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine($"Error in Product Update: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var found = await _uow.ProductRepository.GetAsyncInclude(id);

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

                var deleted = await _uow.ProductRepository.DeleteAsync(id);

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
