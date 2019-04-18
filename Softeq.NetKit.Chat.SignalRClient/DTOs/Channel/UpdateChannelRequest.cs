// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Channel
{
    public class UpdateChannelRequest : BaseRequest
    {
        public string SaasUserId { get; set; }
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string WelcomeMessage { get; set; }
        // Private channel
        public ChannelType Type { get; set; }
        public List<Guid> AllowedMembers { get; set; }
        public string PhotoUrl { get; set; }
    }
}
