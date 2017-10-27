using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Waitlist.UI.Core.Service.Exception
{
	public class QueueException : System.Exception
	{
		public enum ResponseCodes { PersonExists, InvalidName, InvalidPhone }

		public QueueException ( ResponseCodes eResponseCode )
		{
			ResponseCode = eResponseCode;
		}

		public ResponseCodes ResponseCode { get; private set; }
	}
}
