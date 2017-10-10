using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Homework.UI.Utility.Extensions;

namespace Homework.UI.Core.Service.Adapter
{
	public class PersonFromXml : IFromXml<Data.Person>
	{
		public Data.Person Adapt ( System.Xml.Linq.XElement xElement )
		{
			return new Data.Person ( )
				{
					Id = xElement.Attribute ( "Id" ).GetValue ( Guid.NewGuid ( ), s => new Guid ( s ) ),
					Name = xElement.Element ( "Name" ).GetValue ( ),
					Phone = xElement.Element ( "Phone" ).GetValue ( )
				};
		}
	}

	public class PersonToXml : IToXml<Data.Person>
	{
		public System.Xml.Linq.XElement Adapt ( Data.Person dPerson )
		{
			return new System.Xml.Linq.XElement (
				"Person",
					new System.Xml.Linq.XAttribute ( "Id", dPerson.Id.ToString ( ) ),
					new System.Xml.Linq.XElement ( "Name", dPerson.Name ),
					new System.Xml.Linq.XElement ( "Phone", dPerson.Phone ) );
		}
	}
}
