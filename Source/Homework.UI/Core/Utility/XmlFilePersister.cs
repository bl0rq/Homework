using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework.UI.Utility
{
	public interface IXmlFilePersister
	{
		void Save ( string sName, System.Xml.Linq.XElement xElement );
		System.Xml.Linq.XElement Load ( string sName );
		bool CanLoad ( string sName );
	}

	public class XmlFilePersister : IXmlFilePersister
	{
		private readonly string m_sPersistLocation;

		public XmlFilePersister ( string sPersistLocation )
		{
			m_sPersistLocation = sPersistLocation;

			if ( !System.IO.Directory.Exists ( m_sPersistLocation ) )
				System.IO.Directory.CreateDirectory ( m_sPersistLocation );
		}

		public void Save ( string sName, System.Xml.Linq.XElement xElement )
		{
			ValidateName ( sName );
			xElement.Save ( ToPath ( sName ) );
		}

		public System.Xml.Linq.XElement Load ( string sName )
		{
			ValidateName ( sName );

			string sPath = ToPath ( sName );

			return System.IO.File.Exists ( sPath )
			       	? System.Xml.Linq.XElement.Load ( sPath )
			       	: null;
		}

		public bool CanLoad ( string sName )
		{
			ValidateName ( sName );

			string sPath = ToPath ( sName );

			return System.IO.File.Exists ( sPath );
		}

		private void ValidateName ( string sName )
		{
			char[ ] aInvalidChars = System.IO.Path.GetInvalidFileNameChars ( );
			if ( sName.ToCharArray ( ).Any ( ch => aInvalidChars.Contains ( ch ) ) )
				throw new Exception ( "Name cannot contain: " + string.Join ( ",", aInvalidChars.Select ( ch => ch.ToString ( ) ).ToArray ( ) ) );
		}

		private string ToPath ( string sName )
		{
			return System.IO.Path.Combine ( m_sPersistLocation, sName + ".xml" );
		}
	}
}