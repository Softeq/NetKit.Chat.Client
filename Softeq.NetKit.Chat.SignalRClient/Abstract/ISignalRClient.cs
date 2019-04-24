// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Softeq.NetKit.Chat.SignalRClient.DTOs;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Channel;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Member;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Message;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Client;

namespace Softeq.NetKit.Chat.SignalRClient.Abstract
{
    public interface ISignalRClient
    {
        Task<ClientResponse> ConnectAsync(string accessToken);
        Task Disconnect();

        Task<ChannelSummaryResponse> CreateChannelAsync(CreateChannelRequest request);
        Task<ChannelSummaryResponse> CreateDirectChannelAsync(CreateDirectChannelRequest request);
        Task<ChannelSummaryResponse> UpdateChannelAsync(UpdateChannelRequest request);
        Task PinChannelAsync(PinChannelRequest request);
        Task MuteChannelAsync(MuteChannelRequest request);
        Task CloseChannelAsync(ChannelRequest request);
        Task JoinToChannelAsync(ChannelRequest request);
        Task LeaveChannelAsync(ChannelRequest request);

        Task<MessageResponse> AddMessageAsync(AddMessageRequest request);
        Task DeleteMessageAsync(DeleteMessageRequest request);
        Task UpdateMessageAsync(UpdateMessageRequest request);
        Task MarkAsReadMessageAsync(SetLastReadMessageRequest request);

        Task<ClientResponse> GetClientAsync();
        Task InviteMemberAsync(InviteMemberRequest request);
        Task DeleteMemberAsync(DeleteMemberRequest request);
        Task InviteMultipleMembersAsync(InviteMultipleMembersRequest request);
    }
}
