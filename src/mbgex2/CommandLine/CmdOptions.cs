using System;
using CommandLine;

namespace mbgex2
{
	internal class CmdOptions
	{
		[Option('m', "mode", Required = false, HelpText = "Mode, empty string or 'export'")]
		public string Mode { get; set; }

		[Option('u', "username", Required = false, HelpText = "Account Username")]
		public string Username { get; set; }

		[Option('p', "password", Required = false, HelpText = "Account Password")]
		public string Password { get; set; }

		[Option('f', "from", Required = false, HelpText = "Date from")]
		public string StartDate { get; set; }

		[Option('t', "to", Required = false, HelpText = "Date to")]
		public string EndDate { get; set; }

		public DateTime Start => DateTime.Parse(StartDate);

		public DateTime End => DateTime.Parse(EndDate);
	}
}
