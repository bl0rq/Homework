using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Waitlist.UI.Core.Service.Exception
{
	public class DequeueException : System.Exception
	{
		public enum ResponseCodes { PersonNotFound, InvalidPerson }

		public DequeueException ( ResponseCodes eResponseCode )
		{
			ResponseCode = eResponseCode;
		}

		public ResponseCodes ResponseCode { get; private set; }
	}
}
