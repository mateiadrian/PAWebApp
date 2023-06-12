namespace PAWebApp.Application.Exceptions
{
    public sealed class GlobalErrorModel
    {
        public int? ErrorCode { get; set; }

        public string Message { get; set; } = null!;

        public string? InnerException { get; set; }

        public string? StackTrace { get; set; }
    }
}
