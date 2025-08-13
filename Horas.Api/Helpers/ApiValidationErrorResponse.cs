namespace Horas.Api.Helpers
{
    public class ApiValidationErrorResponse : ApiResponse
    {

        public ApiValidationErrorResponse(IEnumerable<string> errors = null, string details = null) : base(400)
        {
            Errors = errors;
            Details = details ?? string.Join(", ", Errors);
        }
        public IEnumerable<string> Errors { get; set; }

    }
}
