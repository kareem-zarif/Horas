namespace Horas.Api.Helpers
{
    public class ApiValidationErrorResponse : ApiResponse
    {

        public ApiValidationErrorResponse(IEnumerable<string> errors = null) : base(400)
        {
            Errors = errors;
        }
        public IEnumerable<string> Errors { get; set; }

    }
}
