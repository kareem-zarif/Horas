using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string OPENAI_API_KEY = ""; 

        public ChatGPTController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskGPT([FromBody] ChatQuestion question)
        {
            var prompt = $"جاوب على السؤال ده بصيغة خدمة عملاء لمتجر إلكتروني: {question.Question}";

            var requestBody = new
            {
                model = "mistralai/mistral-7b-instruct",
                messages = new[]
                {
                    new { role = "system", content = "أجب عن السؤال التالي بإجابة منطقية ومختصرة وواضحة، واذكر الإجابة أولًا بالعربية ثم نفس الإجابة بالإنجليزية." },
                  new { role = "user", content = question.Question }

                }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OPENAI_API_KEY);

            var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, error);
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(responseContent);
            var answer = jsonDoc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Ok(new { answer });
        }
    }

 
}
