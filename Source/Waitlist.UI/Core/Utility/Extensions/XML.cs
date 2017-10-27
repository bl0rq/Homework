using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Waitlist.UI.Utility.Extensions
{
	public static class Xml
	{
		public static T GetValue<T> ( this System.Xml.Linq.XElement xElement, T Default, Func<string, T> fnConverter )
		{
			if ( xElement == null || string.IsNullOrEmpty ( xElement.Value ) )
				return Default;
			else
				try
				{
					return fnConverter ( xElement.Value );
				}
				catch ( Exception e )
				{
					return Default;
				}
		}

		public static string GetValue ( this System.Xml.Linq.XElement xElement )
		{
			if ( xElement == null )
				return null;
			else
				return xElement.Value;
		}

		public static string GetValue ( this System.Xml.Linq.XElement xElement, string sDefault )
		{
			if ( xElement == null )
				return sDefault;
			else
				return xElement.Value ?? sDefault;
		}

		public static T GetValue<T> ( this System.Xml.Linq.XAttribute xElement, T Default, Func<string, T> fnConverter )
		{
			if ( xElement == null )
				return Default;
			else
				return fnConverter ( xElement.Value );
		}

		public static string GetValue ( this System.Xml.Linq.XAttribute xElement )
		{
			if ( xElement == null )
				return "";
			else
				return xElement.Value;
		}

		public static T GetValue<T> ( this System.Xml.Linq.XElement xElement, Func<T> fnDefault, Func<string, T> fnConverter )
		{
			if ( xElement == null )
				return fnDefault ( );
			else
				try
				{
					return fnConverter ( xElement.Value );
				}
				catch ( Exception e )
				{
					return fnDefault ( );
				}
		}
	}
}