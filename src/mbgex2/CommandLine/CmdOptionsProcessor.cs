using System;
using CommandLine;

namespace mbgex2
{
	internal static class CmdOptionsProcessor
	{
		public static bool Process(CmdOptions options)
		{
			if (options.Username == null || options.Password == null)
				Console.WriteLine("Enter you megabank credentials.");

			if (options.Username == null)
			{
				Console.Write("Username: ");
				options.Username = Console.ReadLine();
			}

			if (options.Password == null)
			{
				Console.Write("Password: ");
				options.Password = Console.ReadLine();
			}

			if (string.IsNullOrEmpty(options.Username) || string.IsNullOrEmpty(options.Password))
			{
				Console.WriteLine("Cmd params validation failed (username/password). Quitting..");
				return false;
			}

			var dateDefault = DateTime.Now.Date.AddMonths(-1);

			options.StartDate ??= dateDefault.ToShortDateString();
			options.EndDate ??= dateDefault.ToShortDateString();

			if (!DateTime.TryParse(options.StartDate, out var _) || !DateTime.TryParse(options.EndDate, out var _))
			{
				Console.WriteLine("Cmd params validation failed (dates). Quitting..");
				return false;
			}

			Console.WriteLine();
			return true;
		}
	}
}
