// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Member
{
    public class InviteMultipleMembersRequest : BaseRequest
    {
        public InviteMultipleMembersRequest()
        {
            InvitedMembersIds = new List<Guid>();
        }

        public Guid ChannelId { get; set; }
        public IEnumerable<Guid> InvitedMembersIds { get; set; }
    }
}