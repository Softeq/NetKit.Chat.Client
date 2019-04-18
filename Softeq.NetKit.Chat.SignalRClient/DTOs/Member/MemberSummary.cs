// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Chat.SignalRClient.DTOs.Member
{
    public class MemberSummary
    {
        public Guid Id { get; set; }
        public string SaasUserId { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        public bool IsAfk { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset LastActivity { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
    }
}
