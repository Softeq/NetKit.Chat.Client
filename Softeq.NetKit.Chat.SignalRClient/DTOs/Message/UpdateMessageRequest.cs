// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Message
{
    public class UpdateMessageRequest : BaseRequest
    {
        public string SaasUserId { get; set; }
        public Guid MessageId { get; set; }
        public string Body { get; set; }

        // If message was encoded
        public bool HtmlEncoded { get; set; }
        public string HtmlContent { get; set; }
    }
}
