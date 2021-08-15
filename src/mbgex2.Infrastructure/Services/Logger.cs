using mbgex2.Application;
using System;

namespace mbgex2.Infrastructure
{
	public class Logger : ILogger
	{
		public void Out(string msg) => Console.WriteLine(msg);
	}
}
