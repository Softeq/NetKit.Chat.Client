// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Chat.SignalRClient.Exceptions
{
    public class ChatException : Exception
    {
        public ChatException(string message) : base(message)
        {
        }
    }
}