// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Softeq.NetKit.Chat.Common.Configuration;

namespace Softeq.NetKit.Chat.SignalRClient.Sample
{
    class Program
    {
        private const string EnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";
        private const string AuthTokenUrl = "connect/token";
        private static AuthMicroserviceConfiguration _authMicroserviceConfiguration;
        private static ChatMicroserviceConfiguration _chatMicroserviceConfiguration;

        static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable(EnvironmentVariableName);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();
            
            _authMicroserviceConfiguration = GetAuthMicroserviceConfiguration(configuration);
            _chatMicroserviceConfiguration = GetChatMicroserviceConfiguration(configuration);

            if (_authMicroserviceConfiguration != null && _chatMicroserviceConfiguration != null)
            {

                try
                {
                    var signalRClient = new SignalRClient(_chatMicroserviceConfiguration.ChatUrl, GetJwtTokenAsync);
                    var manualResetEventSlim = new ManualResetEventSlim();

                    var runningClientTask = RunClientAsync(signalRClient, manualResetEventSlim);
                    runningClientTask.Wait();

                    manualResetEventSlim.Wait();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }

        private static AuthMicroserviceConfiguration GetAuthMicroserviceConfiguration(IConfiguration configuration)
        {
            return new AuthMicroserviceConfiguration
            {
                Url = configuration[ConfigurationSettings.AuthUrl],
                UserName = configuration[ConfigurationSettings.AuthUserName],
                Password = configuration[ConfigurationSettings.AuthPassword],
                InvitedUserName = configuration[ConfigurationSettings.AuthInvitedUserName],
                ClientId = configuration[ConfigurationSettings.AuthIdentityClientId],
                ClientSecret = configuration[ConfigurationSettings.AuthIdentityClientSecret],
                Scope = configuration[ConfigurationSettings.AuthIdentityScope]
            };
        }

        private static ChatMicroserviceConfiguration GetChatMicroserviceConfiguration(IConfiguration configuration)
        {
            return new ChatMicroserviceConfiguration
            {
                ChatUrl = configuration[ConfigurationSettings.ChatUrl]
            };
        }

        private static async Task RunClientAsync(SignalRClient signalRClient, ManualResetEventSlim wh)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Choose required option for testing from the list below:");
                    Console.WriteLine("1 - Channels management");
                    Console.WriteLine("2 - Messages management");
                    Console.WriteLine("3 - Members management");
                    Console.WriteLine("0 - Close");
                    bool isParsed = int.TryParse(Console.ReadLine(), out int choiceNumber);
                    if (isParsed)
                    {
                        await HandleChoiceNumberAsync(choiceNumber, signalRClient);
                    }
                    else
                    {
                        Console.WriteLine("Error. Choose a valid digit!");
                    }

                    if (isParsed && choiceNumber == 0)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.GetBaseException().Message);
            }
            finally
            {
                wh.Set();
            }
        }

        private static async Task HandleChoiceNumberAsync(int choiceNumber, SignalRClient signalRClient)
        {
            switch (choiceNumber)
            {
                case 0:
                    break;
                case 1:
                    await ExecuteChannelManagementLogic(signalRClient);
                    Console.WriteLine("Testing has passed successfully.");
                    break;
                case 2:
                    await ExecuteMessageManagementLogic(signalRClient);
                    Console.WriteLine("Testing has passed successfully.");
                    break;
                case 3:
                    await ExecuteMembersManagementLogic(signalRClient);
                    Console.WriteLine("Testing has passed successfully.");
                    break;
                default:
                    Console.WriteLine("Error. Choose a valid digit!");
                    break;
            }
        }

        private static async Task ExecuteChannelManagementLogic(SignalRClient signalRClient)
        {
            await ConnectAsync(signalRClient);

            var channel = await HubCommands.CreateChannelAsync(signalRClient);

            // switch user
            var userName = _authMicroserviceConfiguration.UserName;
            var invitedUserName = _authMicroserviceConfiguration.InvitedUserName;

            _authMicroserviceConfiguration.UserName = invitedUserName;
            await ConnectAsync(signalRClient);

            var secondMember = await HubCommands.GetClientAsync(signalRClient);

            // switch user
            _authMicroserviceConfiguration.UserName = userName;
            _authMicroserviceConfiguration.InvitedUserName = invitedUserName;
            await ConnectAsync(signalRClient);

           var  client = await HubCommands.GetClientAsync(signalRClient);

            await HubCommands.InviteMemberAsync(signalRClient, channel.Id, secondMember.MemberId);

            await HubCommands.UpdateChannelAsync(signalRClient, channel.Id);
            await HubCommands.MuteChannelAsync(signalRClient, channel.Id);
            await HubCommands.PinChannelAsync(signalRClient, channel.Id);

            await HubCommands.DeleteMemberAsync(signalRClient, channel.Id, secondMember.MemberId);

            await HubCommands.CloseChannelAsync(signalRClient, channel.Id);

            // switch user
            _authMicroserviceConfiguration.UserName = invitedUserName;
            await ConnectAsync(signalRClient);

            await HubCommands.CreateDirectChannelAsync(signalRClient, client.MemberId);

            _authMicroserviceConfiguration.UserName = userName;
            _authMicroserviceConfiguration.InvitedUserName = invitedUserName;

            await signalRClient.DisconnectAsync();
        }

        private static async Task ExecuteMessageManagementLogic(SignalRClient signalRClient)
        {
            await ConnectAsync(signalRClient);
            var channel = await HubCommands.CreateChannelAsync(signalRClient);
            var message = await HubCommands.AddMessageAsync(signalRClient, channel.Id);
            await HubCommands.SetLastReadMessageAsync(signalRClient, channel.Id, message.Id);
            await HubCommands.UpdateMessageAsync(signalRClient, message.Id);
            await HubCommands.DeleteMessageAsync(signalRClient, message.Id);
            await signalRClient.DisconnectAsync();
        }

        private static async Task ExecuteMembersManagementLogic(SignalRClient signalRClient)
        {
            var userName = _authMicroserviceConfiguration.UserName;
            var invitedUserName = _authMicroserviceConfiguration.InvitedUserName;

            _authMicroserviceConfiguration.UserName = invitedUserName;
            await ConnectAsync(signalRClient);

            var secondMember = await HubCommands.GetClientAsync(signalRClient);

            _authMicroserviceConfiguration.UserName = userName;
            _authMicroserviceConfiguration.InvitedUserName = invitedUserName;
            await ConnectAsync(signalRClient);

            var channel = await HubCommands.CreateChannelAsync(signalRClient);
            await HubCommands.InviteMemberAsync(signalRClient, channel.Id, secondMember.MemberId);
            await HubCommands.DeleteMemberAsync(signalRClient, channel.Id, secondMember.MemberId);
            await HubCommands.InviteMultipleMembersAsync(signalRClient, channel.Id, secondMember.MemberId);
            await HubCommands.DeleteMemberAsync(signalRClient, channel.Id, secondMember.MemberId);
            await signalRClient.DisconnectAsync();
        }

        private static async Task ConnectAsync(SignalRClient client)
        {
            await client.ConnectAsync();

            Console.WriteLine("Logged on successfully.");
            Console.WriteLine();
        }

        private static async Task<string> GetJwtTokenAsync()
        {
            var httpClient = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { HttpConstants.GrantTypeKey, HttpConstants.GrantTypeValue },
                { HttpConstants.PasswordKey, _authMicroserviceConfiguration.Password },
                { HttpConstants.UserNameKey, _authMicroserviceConfiguration.UserName },
                { HttpConstants.ScopeKey, _authMicroserviceConfiguration.Scope },
                { HttpConstants.ClientIdKey, _authMicroserviceConfiguration.ClientId },
                { HttpConstants.ClientSecretKey, _authMicroserviceConfiguration.ClientSecret }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await httpClient.PostAsync($"{_authMicroserviceConfiguration.Url}/{AuthTokenUrl}", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseString);
            var accessToken = json.Value<string>(HttpConstants.AccessTokenKey);

            return accessToken;
        }
    }
}
