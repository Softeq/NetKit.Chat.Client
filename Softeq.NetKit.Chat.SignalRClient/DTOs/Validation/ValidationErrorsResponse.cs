// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Validation
{
    public class ValidationErrorsResponse
    {
        public string PropertyName { get; set; }

        public object AttemptedValue { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public object CustomState { get; set; }
    }
}