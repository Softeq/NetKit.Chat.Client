// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Softeq.NetKit.Chat.SignalRClient.Abstract;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Channel;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Member;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Message;
using Softeq.NetKit.Chat.SignalRClient.DTOs.Client;

namespace Softeq.NetKit.Chat.SignalRClient
{
    public class SignalRClient : ISignalRClient
    {
        private const string CreateChannelCommandName = "CreateChannelAsync";
        private const string CreateDirectChannelCommandName = "CreateDirectChannelAsync";
        private const string UpdateChannelCommandName = "UpdateChannelAsync";
        private const string MuteChannelCommandName = "MuteChannelAsync";
        private const string PinChannelCommandName = "PinChannelAsync";
        private const string CloseChannelCommandName = "CloseChannelAsync";
        private const string JoinToChannelCommandName = "JoinToChannelAsync";
        private const string LeaveChannelCommandName = "LeaveChannelAsync";
        private const string AddMessageCommandName = "AddMessageAsync";
        private const string DeleteMessageCommandName = "DeleteMessageAsync";
        private const string UpdateMessageCommandName = "UpdateMessageAsync";
        private const string MarkAsReadMessageCommandName = "MarkAsReadMessageAsync";
        private const string GetClientCommandName = "GetClientAsync";
        private const string InviteMemberCommandName = "InviteMemberAsync";
        private const string DeleteMemberCommandName = "DeleteMemberAsync";
        private const string InviteMultipleMembersCommandName = "InviteMultipleMembersAsync";
        private const string DeleteClientCommandName = "DeleteClientAsync";
        private const string AddClientCommandName = "AddClientAsync";

        private HubConnection _connection;
        public string SourceUrl { get; }

        public event Action<ChannelSummaryResponse> ChannelUpdated;
        public event Action<ChannelSummaryResponse> ChannelAdded;
        public event Action<ChannelSummaryResponse> ChannelClosed;

        public event Action<MessageResponse> MessageAdded;
        public event Action<MessageResponse> MessageDeleted;
        public event Action<MessageResponse> MessageUpdated;
        public event Action<Guid> LastReadMessageUpdated;

        public event Action<MemberSummary, ChannelSummaryResponse> MemberJoined;
        public event Action<MemberSummary> MemberLeft;
        public event Action<MemberSummary, Guid> MemberDeleted;
        public event Action<MemberSummary, Guid> YouAreDeleted;


        public SignalRClient(string url)
        {
            SourceUrl = url;
        }
        
        public async Task<ClientResponse> ConnectAsync(string accessToken)
        {
            Console.WriteLine("Connecting to {0}", SourceUrl);
            _connection = new HubConnectionBuilder()
                .WithUrl($"{SourceUrl}/chat", options =>
                {
                    options.Headers.Add("Authorization", "Bearer " + accessToken);
                })
                .Build();

            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                _connection.DisposeAsync().GetAwaiter().GetResult();
            };

            _connection.Closed += e =>
            {
                Console.WriteLine("Connection closed...");
                return Task.CompletedTask;
            };

            SubscribeToEvents();

            // Handle the connected connection
            while (true)
            {
                try
                {
                    await _connection.StartAsync();
                    Console.WriteLine("Connected to {0}", SourceUrl);
                    break;
                }
                catch (IOException ex)
                {
                    // Process being shutdown
                    Console.WriteLine(ex);
                    break;
                }
                catch (OperationCanceledException ex)
                {
                    // The connection closed
                    Console.WriteLine(ex);
                    break;
                }
                catch (Exception ex)
                {
                    // Send could have failed because the connection closed
                    Console.WriteLine(ex);
                    Console.WriteLine("Failed to connect, trying again in 5000(ms)");
                    await Task.Delay(5000);
                }
            }

            var client = await _connection.InvokeAsync<ClientResponse>(AddClientCommandName);
            return client;
        }

        #region Channel
        
        public async Task<ChannelSummaryResponse> CreateChannelAsync(CreateChannelRequest model)
        {
            return await _connection.InvokeAsync<ChannelSummaryResponse>(CreateChannelCommandName, model);
        }

        public async Task<ChannelSummaryResponse> CreateDirectChannelAsync(CreateDirectChannelRequest model)
        {
            return await _connection.InvokeAsync<ChannelSummaryResponse>(CreateDirectChannelCommandName, model);
        }

        public async Task<ChannelSummaryResponse> UpdateChannelAsync(UpdateChannelRequest request)
        {
            return await _connection.InvokeAsync<ChannelSummaryResponse>(UpdateChannelCommandName, request);
        }

        public async Task MuteChannelAsync(MuteChannelRequest request)
        {
            await _connection.InvokeAsync(MuteChannelCommandName, request);
        }

        public async Task PinChannelAsync(PinChannelRequest request)
        {
            await _connection.InvokeAsync(PinChannelCommandName, request);
        }

        public async Task CloseChannelAsync(ChannelRequest request)
        {
            await _connection.InvokeAsync(CloseChannelCommandName, request);
        }

        public async Task JoinToChannelAsync(ChannelRequest model)
        {
            await _connection.InvokeAsync(JoinToChannelCommandName, model);
        }

        public async Task LeaveChannelAsync(ChannelRequest model)
        {
            await _connection.InvokeAsync(LeaveChannelCommandName, model);
        }

        #endregion

        #region Message

        public async Task<MessageResponse> AddMessageAsync(AddMessageRequest request)
        {
            return await _connection.InvokeAsync<MessageResponse>(AddMessageCommandName, request);
        }

        public async Task DeleteMessageAsync(DeleteMessageRequest model)
        {
            await _connection.InvokeAsync(DeleteMessageCommandName, model);
        }

        public async Task UpdateMessageAsync(UpdateMessageRequest request)
        {
            await _connection.InvokeAsync<MessageResponse>(UpdateMessageCommandName, request);

        }

        public async Task MarkAsReadMessageAsync(SetLastReadMessageRequest request)
        {
            await _connection.InvokeAsync(MarkAsReadMessageCommandName, request);
        }

        #endregion

        #region Members

        public async Task<ClientResponse> GetClientAsync()
        {
            return await _connection.InvokeAsync<ClientResponse>(GetClientCommandName);
        }

        public async Task InviteMemberAsync(InviteMemberRequest model)
        {
            await _connection.InvokeAsync(InviteMemberCommandName, model);
        }

        public async Task DeleteMemberAsync(DeleteMemberRequest request)
        {
            await _connection.InvokeAsync(DeleteMemberCommandName, request);
        }

        public async Task InviteMultipleMembersAsync(InviteMultipleMembersRequest request)
        {
            await _connection.InvokeAsync(InviteMultipleMembersCommandName, request);
        }

        #endregion

        public async Task Disconnect()
        {
            await _connection.InvokeAsync(DeleteClientCommandName);
            await _connection.StopAsync();
        }

        private void SubscribeToEvents()
        {
            #region Channel

            _connection.On<ChannelSummaryResponse>(ClientEvents.ChannelAdded, channel =>
            {
                Execute(ChannelAdded, channelCreated => channelCreated(channel));
            });

            _connection.On<ChannelSummaryResponse>(ClientEvents.ChannelUpdated, channel =>
            {
                Execute(ChannelUpdated, channelUpdated => channelUpdated(channel));
            });

            _connection.On<ChannelSummaryResponse>(ClientEvents.ChannelClosed, channel =>
            {
                Execute(ChannelClosed, channelClosed => channelClosed(channel));
            });

            #endregion

            #region Message

            _connection.On<MessageResponse>(ClientEvents.MessageAdded, message =>
            {
                Execute(MessageAdded, messageCreated => messageCreated(message));
            });

            _connection.On<MessageResponse>(ClientEvents.MessageUpdated, message =>
            {
                Execute(MessageUpdated, messageUpdated => messageUpdated(message));
            });

            _connection.On<MessageResponse>(ClientEvents.MessageDeleted, message =>
            {
                Execute(MessageDeleted, messageDeleted => messageDeleted(message));
            });

            _connection.On<Guid>(ClientEvents.LastReadMessageChanged, channelId =>
            {
                Execute(LastReadMessageUpdated, lastReadMessageUpdated => lastReadMessageUpdated(channelId));
            });

            #endregion

            #region Member

            _connection.On<MemberSummary, ChannelSummaryResponse>(ClientEvents.MemberJoined, (member, channel) =>
            {
                Execute(MemberJoined, memberJoined => memberJoined(member, channel));
            });

            _connection.On<MemberSummary>(ClientEvents.MemberLeft, member =>
            {
                Execute(MemberLeft, memberLeft => memberLeft(member));
            });

            _connection.On<MemberSummary, Guid>(ClientEvents.MemberDeleted, (member, channelId) =>
            {
                Execute(MemberDeleted, memberDeleted => memberDeleted(member, channelId));
            });

            _connection.On<MemberSummary, Guid>(ClientEvents.YouAreDeleted, (member, channelId) =>
            {
                Execute(YouAreDeleted, youAreDeleted => youAreDeleted(member, channelId));
            });

            #endregion
        }

        private void Execute<T>(T handlers, Action<T> action) where T : class
        {
            Task.Factory.StartNew(() =>
            {
                if (handlers != null)
                {
                    action(handlers);
                }
            });
        }
    }
}
