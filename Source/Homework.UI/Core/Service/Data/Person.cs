using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework.UI.Core.Service.Data
{
	public class Person : IEquatable<Person>
	{
		public Guid Id { get; internal set; }
		public string Name { get; internal set; }
		public string Phone { get; internal set; }

		public bool Equals ( Person other )
		{
			if ( ReferenceEquals ( null, other ) ) return false;
			if ( ReferenceEquals ( this, other ) ) return true;
			return string.Equals ( other.Name, Name, StringComparison.CurrentCultureIgnoreCase )
				&& string.Equals ( other.Phone, Phone, StringComparison.CurrentCultureIgnoreCase );
		}

		public override bool Equals ( object obj )
		{
			if ( ReferenceEquals ( null, obj ) ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			
			return obj.GetType ( ) == typeof ( Person ) && Equals ( (Person) obj );
		}

		public override int GetHashCode ( )
		{
			unchecked
			{
				return ( ( Name != null ? Name.GetHashCode ( ) : 0 ) * 397 ) ^ ( Phone != null ? Phone.GetHashCode ( ) : 0 );
			}
		}

		public static bool operator == ( Person left, Person right )
		{
			return Equals ( left, right );
		}

		public static bool operator != ( Person left, Person right )
		{
			return !Equals ( left, right );
		}
	}
}
