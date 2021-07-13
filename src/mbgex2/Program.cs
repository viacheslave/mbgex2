using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mbgex2
{
	static class Program
  {
    async static Task Main(string[] args)
    {
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			await ProcessMode(args[0], args);

      Console.WriteLine("All Done.");
    }

		private static async Task ProcessMode(string option, string[] args)
		{
			switch (option)
			{
        case "--export":
          await ExportData(args.Skip(1).ToArray());
          break;
        default:
          await ShowLastMonth(args);
          break;
			}
		}

		private static async Task ExportData(string[] args)
		{
			var options = GetOptions(args);

			// get data
			var data = await GetData(options);

			// export
			await new DataExporter().SaveRaw(data);

			new DataExporter().SaveByUtility(data);
		}

		private static async Task ShowLastMonth(string[] args)
		{
			var options = GetOptions(args);

			// get data
			var data = await GetData(options);

			// out
			var output = ConsoleFormatter.GetOutput(data);

			Console.WriteLine(output);
		}

		private static Options GetOptions(string[] args)
		{
			var dateDefault = DateTime.Now.Date.AddMonths(-1);

			var options = new Options(args[0], args[1], dateDefault, dateDefault);

			if (args.Length >= 3)
				options = options with { StartDate = DateTime.Parse(args[2]) };

			if (args.Length >= 4)
				options = options with { EndDate = DateTime.Parse(args[3]) };

			return options;
		}

		private static async Task<IReadOnlyCollection<UtilityLinesDto>> GetData(Options options)
		{
			// get auth cookies
			var authCookies = await new AuthClient()
				.GetAuthCookies(
					new UserCredentials(options.Login, options.Password));

			// get data
			var data = await new DataProvider(authCookies)
				.Grab(
					new Dates(options.StartDate, options.EndDate));

			return data;
		}

		private record Options
		(
			string Login,
			string Password,
			DateTime StartDate,
			DateTime EndDate
		);
	}
}
