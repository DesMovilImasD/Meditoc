using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Multimedia
{
    public class MessageReceivedArgs : EventArgs
    {
        public string Name { get; private set; }
        public string Message { get; private set; }

        public MessageReceivedArgs(string name, string message)
        {
            this.Name = name;
            this.Message = message;
        }
    }
}
