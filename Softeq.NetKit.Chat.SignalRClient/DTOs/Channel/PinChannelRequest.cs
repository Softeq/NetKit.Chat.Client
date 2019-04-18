// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Channel
{
    public class PinChannelRequest : BaseRequest
    {
        public Guid ChannelId { get; set; }
        public bool IsPinned { get; set; }
    }
}