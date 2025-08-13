using System.Net.Http.Headers;
using System.Text.Json;

namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string OPENAI_API_KEY = "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";


        // ✅ حطيت الـ Dictionary جوه الكلاس عشان يبقى متاح في الميثود
        private static readonly Dictionary<string, string> PresetQA = new()
        {
            { "ما هي مواعيد العمل؟", "مواعيد العمل من السبت إلى الخميس، من 9 صباحًا حتى 9 مساءً." },
            { "كيف يمكنني تتبع طلبي؟", "يمكنك تتبع طلبك عبر صفحة 'تتبع الطلب' باستخدام رقم الطلب." },
            { "هل يوجد شحن دولي؟", "نعم، نوفر الشحن الدولي إلى معظم الدول." },
            { "ما هي سياسة الاسترجاع؟", "يمكنك إرجاع المنتج خلال 14 يومًا من الاستلام إذا كان بحالته الأصلية." }
        };
        public ChatGPTController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskGPT([FromBody] ChatQuestion question)
        {

            if (string.IsNullOrWhiteSpace(question?.Question))
                return BadRequest(new { error = "السؤال مطلوب." });

            // ✅ هنا هيشوف هل السؤال من الأسئلة الجاهزة
            if (PresetQA.TryGetValue(question.Question.Trim(), out var presetAnswer))
            {
                return Ok(new { answer = presetAnswer });
            }

            var prompt = $"أجب على السؤال التالي بصيغة خدمة عملاء لمتجر إلكتروني.يجب أن تكون الإجابة واضحة وصريحة ومباشرة، بدون أي أسئلة إضافية أو شرح زائد لا تستخدم روابط أو إحالات خارجية إذا لم تكن المعلومة مؤكدة، قدم مدة تقديرية بناءً على المدة المعتادة أجب أولاً بالعربية، ثم في السطر التالي نفس الإجابة بالإنجليزية فقط: \n{question.Question}";

            var requestBody = new
            {
                model = "mistralai/mistral-7b-instruct",
                messages = new[]
                {
                    new { role = "system", content = "أنت مساعد خدمة عملاء محترف لمتجر إلكتروني. إجابتك يجب أن تكون دقيقة ومباشرة ومختصرة. لا تضف أي معلومات غير مرتبطة بالسؤال" },
                    new { role = "user", content = prompt }
                },
                temperature = 0.2
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
