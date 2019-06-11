// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Chat.TransportModels.Enums;
using Softeq.NetKit.Chat.TransportModels.Models.CommonModels.Request.Channel;
using Softeq.NetKit.Chat.TransportModels.Models.CommonModels.Request.Member;
using Softeq.NetKit.Chat.TransportModels.Models.CommonModels.Request.Message;
using Softeq.NetKit.Chat.TransportModels.Models.CommonModels.Response.Channel;
using Softeq.NetKit.Chat.TransportModels.Models.CommonModels.Response.Message;
using Softeq.NetKit.Chat.TransportModels.Models.SignalRModels.Client;

namespace Softeq.NetKit.Chat.SignalRClient.Sample
{
    public static class HubCommands
    {
        #region Channel Hub Commands

        public static async Task<ChannelSummaryResponse> CreateChannelAsync(SignalRClient client)
        {
            string groupName = Guid.NewGuid() + "test";

            // Create the channel called test
            var createChannelRequest = new CreateChannelRequest
            {
                AllowedMembers = new List<string>(),
                Description = "test",
                Name = groupName,
                PhotoUrl = "https://softeqnonamemessaging.blob.core.windows.net/temp/1000_abf08299411.jpg",
                Type = ChannelType.Public,
                WelcomeMessage = "Hi test"
            };

            Console.WriteLine("Creating the channel.");
            var createdChannel = await client.CreateChannelAsync(createChannelRequest);
            Console.WriteLine("Channel was created.");
            Console.WriteLine();

            return createdChannel;
        }

        public static async Task<ChannelSummaryResponse> CreateDirectChannelAsync(SignalRClient client, Guid memberId)
        {
            var createDirectChannelRequest = new CreateDirectChannelRequest
            {
                MemberId = memberId
            };

            Console.WriteLine("Creating a direct channel.");
            var createdChannel = await client.CreateDirectChannelAsync(createDirectChannelRequest);
            Console.WriteLine("Direct channel was created.");
            Console.WriteLine();

            return createdChannel;
        }

        public static async Task<ChannelSummaryResponse> UpdateChannelAsync(SignalRClient signalRClient, Guid channelId)
        {
            // Update the channel called test
            var updateChannelRequest = new UpdateChannelRequest
            {
                ChannelId = channelId,
                Description = "test",
                WelcomeMessage = "Hi test",
                Name = Guid.NewGuid() + "test",
                PhotoUrl = "https://softeqnonamemessaging.blob.core.windows.net/temp/1000_abf08299411.jpg"
            };

            Console.WriteLine("Updating the channel.");
            var updatedChannel = await signalRClient.UpdateChannelAsync(updateChannelRequest);
            Console.WriteLine("Channel was updated.");
            Console.WriteLine();

            return updatedChannel;
        }

        public static async Task MuteChannelAsync(SignalRClient signalRClient, Guid channelId)
        {
            // Close the channel called test
            var muteChannelRequest = new MuteChannelRequest
            {
                ChannelId = channelId,
                IsMuted = true
            };

            Console.WriteLine("Mutting the channel");
            await signalRClient.MuteChannelAsync(muteChannelRequest);
            Console.WriteLine("Channel was muted.");
            Console.WriteLine();
        }

        public static async Task PinChannelAsync(SignalRClient signalRClient, Guid channelId)
        {
            // Close the channel called test
            var pinChannelRequest = new PinChannelRequest
            {
                ChannelId = channelId,
                IsPinned = true
            };

            Console.WriteLine("Pinning the channel");
            await signalRClient.PinChannelAsync(pinChannelRequest);
            Console.WriteLine("Channel was pinned.");
            Console.WriteLine();
        }

        public static async Task CloseChannelAsync(SignalRClient signalRClient, Guid channelId)
        {
            // Close the channel called test
            var closeChannelRequest = new ChannelRequest
            {
                ChannelId = channelId
            };

            Console.WriteLine("Closing the channel");
            await signalRClient.CloseChannelAsync(closeChannelRequest);
            Console.WriteLine("Channel was closed.");
            Console.WriteLine();
        }

        public static async Task JoinToChannelAsync(SignalRClient signalRClient, Guid channelId)
        {
            // Join to the channel call test
            var channelRequestModel = new ChannelRequest
            {
                ChannelId = channelId
            };

            Console.WriteLine("Join to the channel");
            await signalRClient.JoinToChannelAsync(channelRequestModel);
            Console.WriteLine("User joined to the channel.");
            Console.WriteLine();
        }

        public static async Task LeaveChannelAsync(SignalRClient signalRClient, Guid channelId)
        {
            // Leave the channel call test
            var channelRequestModel = new ChannelRequest
            {
                ChannelId = channelId
            };

            Console.WriteLine("Leave the channel");
            await signalRClient.LeaveChannelAsync(channelRequestModel);
            Console.WriteLine("User leaved the channel.");
            Console.WriteLine();
        }

        #endregion

        #region Message Hub Commands

        public static async Task<MessageResponse> AddMessageAsync(SignalRClient signalRClient, Guid channelId)
        {
            // Create the message called test

            var createMessageRequest = new AddMessageRequest
            {
                Body = "test",
                ChannelId = channelId,
                Type = MessageType.Default
            };

            Console.WriteLine("Creating the message");
            var createdMessage = await signalRClient.AddMessageAsync(createMessageRequest);
            Console.WriteLine("Message was created.");
            Console.WriteLine();

            return createdMessage;
        }

        public static async Task SetLastReadMessageAsync(SignalRClient signalRClient, Guid channelId, Guid messageId)
        {
            // Set last read message test
            var setLastReadMessageRequest = new SetLastReadMessageRequest
            {
                ChannelId = channelId,
                MessageId = messageId
            };

            Console.WriteLine("Trying to mark message as read.");
            await signalRClient.MarkAsReadMessageAsync(setLastReadMessageRequest);
            Console.WriteLine("Message was marked.");
            Console.WriteLine();
        }

        public static async Task UpdateMessageAsync(SignalRClient signalRClient, Guid messageId)
        {
            // Update the message called test
            var updateMessageRequest = new UpdateMessageRequest
            {
                MessageId = messageId,
                Body = Guid.NewGuid() + "test",
            };

            Console.WriteLine("Updating the message");
            await signalRClient.UpdateMessageAsync(updateMessageRequest);
            Console.WriteLine("Message was updated.");
            Console.WriteLine();
        }

        public static async Task DeleteMessageAsync(SignalRClient signalRClient, Guid messageId)
        {
            // // Delete the message called test
            var deleteMessageRequest = new DeleteMessageRequest
            {
                MessageId = messageId
            };

            Console.WriteLine("Deleting the message");
            await signalRClient.DeleteMessageAsync(deleteMessageRequest);
            Console.WriteLine("Message was deleted.");
            Console.WriteLine();
        }

        #endregion

        #region Member Hub Commands

        public static async Task<ClientResponse> GetClientAsync(SignalRClient signalRClient)
        {
            Console.WriteLine("Getting the client");

            try
            {
                var res = await signalRClient.GetClientAsync();

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task InviteMemberAsync(SignalRClient signalRClient, Guid channelId, Guid memberId)
        {
            // Invite member test
            var inviteMessageRequest = new InviteMemberRequest
            {
                ChannelId = channelId,
                MemberId = memberId
            };

            Console.WriteLine("Inviting a member.");
            await signalRClient.InviteMemberAsync(inviteMessageRequest);
            Console.WriteLine("Member was invited.");
            Console.WriteLine();
        }

        public static async Task DeleteMemberAsync(SignalRClient signalRClient, Guid channelId, Guid memberId)
        {
            // Invite member test
            var deleteMessageRequest = new DeleteMemberRequest
            {
                ChannelId = channelId,
                MemberId = memberId
            };

            Console.WriteLine("Deleting a member.");
            await signalRClient.DeleteMemberAsync(deleteMessageRequest);
            Console.WriteLine("Member was deleted.");
            Console.WriteLine();
        }

        public static async Task InviteMultipleMembersAsync(SignalRClient signalRClient, Guid channelId, Guid memberId)
        {
            // Invite member test
            var inviteMultipleMembersRequest = new InviteMultipleMembersRequest
            {
                ChannelId = channelId,
                InvitedMembersIds = new List<Guid> { memberId }
            };

            Console.WriteLine("Inviting members.");
            await signalRClient.InviteMultipleMembersAsync(inviteMultipleMembersRequest);
            Console.WriteLine("Members were invited.");
            Console.WriteLine();
        }

        #endregion
    }
}
