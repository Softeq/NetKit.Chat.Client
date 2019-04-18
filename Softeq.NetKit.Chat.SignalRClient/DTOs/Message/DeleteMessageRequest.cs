// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Message
{
    public class DeleteMessageRequest : BaseRequest
    {
        public Guid ChannelId { get; set; }
        public Guid MessageId { get; set; }
    }
}
