// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Message
{
    public class AddMessageRequest : BaseRequest
    {
        public string SaasUserId { get; set; }
        public Guid ChannelId { get; set; }
        public MessageType Type { get; set; }
        public string Body { get; set; }
    }
}
