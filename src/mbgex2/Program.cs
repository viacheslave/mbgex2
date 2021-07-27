using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;

namespace mbgex2
{
	static class Program
  {
    async static Task Main(string[] args)
    {
			Console.WriteLine("mbgex2 tool");
			Console.WriteLine("--------------------");

			Console.OutputEncoding = System.Text.Encoding.UTF8;

			await Parser.Default.ParseArguments<CmdOptions>(args).WithParsedAsync(async o => await ProcessMode(o));

      Console.WriteLine("All Done.");
    }

		private static async Task ProcessMode(CmdOptions options)
		{
			if (!CmdOptionsProcessor.Process(options))
				return;

			switch (options.Mode)
			{
        case "export":
          await ExportData(options);
          break;
        default:
          await ShowLastMonth(options);
          break;
			}
		}

		private static async Task ExportData(CmdOptions options)
		{
			// get data
			var data = await GetRawData(options);
			var accounts = DataTransformer.BuildAccounts(data);

			// export
			var exporter = new DataExporter();
			await exporter.SaveRaw(data);
			exporter.SaveAccounts(accounts);

			Logger.Out($"Export folder: {exporter.ExportFolder.FullName}");
		}

		private static async Task ShowLastMonth(CmdOptions options)
		{
			// get data
			var data = await GetRawData(options);
			var accounts = DataTransformer.BuildAccounts(data);

			// out
			var output = ConsoleFormatter.GetOutput(accounts);

			Console.WriteLine(output);
		}

		private static async Task<IReadOnlyCollection<UtilityLinesDto>> GetRawData(CmdOptions options)
		{
			// get auth cookies
			var authCookies = await new AuthClient()
				.GetAuthCookies(
					new UserCredentials(options.Username, options.Password));

			// get data
			var data = await new DataProvider(authCookies)
				.Grab(
					new Dates(options.Start, options.End));

			return data;
		}
	}
}
