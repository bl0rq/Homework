using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.UI.Core.Messages
{
    public class LoadingProgress : Utilis.Messaging.BaseAsyncMessage
    {
        public string Message { get; private set; }

        public LoadingProgress ( string message )
        {
            Message = message;
        }
    }
}
