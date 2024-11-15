using System;
using System.IO;

namespace GA.GArkanoid.Error
{
	public class LoadException : IOException
	{
		public LoadException() : base()
		{
		}
		
		public LoadException(string message) : base(message)
		{
		}
	}
}