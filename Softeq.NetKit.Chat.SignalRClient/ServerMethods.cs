// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Chat.SignalRClient
{
    internal static class ServerMethods
    {
        // Client
        public const string DeleteClientAsync = "DeleteClientAsync";
        public const string AddClientAsync = "AddClientAsync";
        public const string GetClientAsync = "GetClientAsync";

        // Message
        public const string AddMessageAsync = "AddMessageAsync";
        public const string DeleteMessageAsync = "DeleteMessageAsync";
        public const string UpdateMessageAsync = "UpdateMessageAsync";
        public const string MarkAsReadMessageAsync = "MarkAsReadMessageAsync";

        // Channel
        public const string CreateChannelAsync = "CreateChannelAsync";
        public const string CreateDirectChannelAsync = "CreateDirectChannelAsync";
        public const string UpdateChannelAsync = "UpdateChannelAsync";
        public const string MuteChannelAsync = "MuteChannelAsync";
        public const string PinChannelAsync = "PinChannelAsync";
        public const string CloseChannelAsync = "CloseChannelAsync";
        public const string JoinToChannelAsync = "JoinToChannelAsync";
        public const string LeaveChannelAsync = "LeaveChannelAsync";
       
        // Member
        public const string InviteMemberAsync = "InviteMemberAsync";
        public const string DeleteMemberAsync = "DeleteMemberAsync";
        public const string InviteMultipleMembersAsync = "InviteMultipleMembersAsync";
    }
}