// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Channel;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Member;

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Message
{
    public class ForwardMessageResponse
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public Guid ChannelId { get; set; }
        public Guid? OwnerId { get; set; }
        public ChannelSummaryResponse Channel { get; set; }
        public MemberSummary Owner { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}