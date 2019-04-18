// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Chat.SignalRClient
{
    public static class ClientEvents
    {
        public const string MessageDeleted = "MessageDeleted";
        public const string MessageAdded = "MessageAdded";
        public const string MessageUpdated = "MessageUpdated";
        public const string LastReadMessageChanged = "LastReadMessageChanged";

        public const string MemberLeft = "MemberLeft";
        public const string MemberJoined = "MemberJoined";
        public const string MemberDeleted = "MemberDeleted";
        public const string YouAreDeleted = "YouAreDeleted";

        public const string ChannelAdded = "ChannelAdded";
        public const string ChannelClosed = "ChannelClosed";
        public const string ChannelUpdated = "ChannelUpdated";
    }
}
