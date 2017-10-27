using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Waitlist.UI.Utility.Extensions
{
	public static class Misc
	{
		/// <summary>
		/// Disposes an object (if its not null) inside a try/catch w/ an empty catch.
		/// WARNING: surpresses the error!
		/// </summary>
		public static void DisposeWithCare ( this IDisposable oDisposable )
		{
			if ( oDisposable != null )
			{
				try
				{
					oDisposable.Dispose ( );
				}
				catch ( Exception e )
				{
					Utilis.Logger.Log ( e, oDisposable.GetType ( ).Name + " Dispose" );
				}
			}
		}

		public static void DisposeItems ( this IEnumerable<IDisposable> aDisposables )
		{
			foreach ( IDisposable oDisposable in aDisposables )
			{
				oDisposable.DisposeWithCare ( );
			}
		}

		public static bool NextBool ( this Random oRandom )
		{
			return oRandom.NextDouble ( ) >= .5;
		}

		/// <summary>
		/// Returns a bool w/ the dTrueOdds of being true where dTrueOdds is between 0 and 1
		/// </summary>
		public static bool NextBool ( this Random oRandom, double dTrueOdds )
		{
			return oRandom.NextDouble ( ) >= ( 1 - dTrueOdds );
		}

		public static T NextEnum<T> ( this Random oRandom )
		{
			if ( !typeof ( T ).IsEnum )
				throw new Exception ( "Type of T must be an enum" );

			Array aValues = Enum.GetValues ( typeof ( T ) );

			return (T)aValues.GetValue ( oRandom.Next ( 0, aValues.Length ) );
		}

		public static string ToPrettyString ( this TimeSpan ts )
		{
			if ( ts.TotalMinutes >= 1 )
				return ts.ToString ( );
			else if ( ts.TotalSeconds >= 1 )
				return ts.TotalSeconds.ToString ( "#,###.##" ) + " s";
			else
				return ts.TotalMilliseconds.ToString ( "#,###.##" ) + " ms";
		}
	}
}
