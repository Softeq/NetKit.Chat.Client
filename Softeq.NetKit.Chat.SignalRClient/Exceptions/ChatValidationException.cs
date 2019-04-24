// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using Softeq.NetKit.Chat.SignalRClient.Exceptions;

namespace Softeq.NetKit.Chat.SignalRClient
{
    public class ChatValidationException : ChatException
    {
        public ChatValidationException(IEnumerable<string> errors)
            : base(string.Concat("Chat Validation Exception: ", string.Join("\n", errors)))
        {
            Errors = new List<string>(errors);
        }

        public IList<string> Errors { get; }
    }
}