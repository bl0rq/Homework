using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework.UI.Core.Service.Adapter
{
	public interface IToXml<T>
	{
		System.Xml.Linq.XElement Adapt ( T o );
	}

	public interface IFromXml<T>
	{
		T Adapt ( System.Xml.Linq.XElement xElement );
	}
}