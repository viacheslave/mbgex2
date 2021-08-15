using System;
using System.Threading.Tasks;
using CommandLine;
using mbgex2.Application;
using mbgex2.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace mbgex2
{
	static class Program
  {
    async static Task Main(string[] args)
    {
			Console.WriteLine("mbgex2 tool");
			Console.WriteLine("--------------------");

			Console.OutputEncoding = System.Text.Encoding.UTF8;

			var services = new ServiceCollection();
			services.AddServices();

			await Parser.Default
				.ParseArguments<CmdOptions>(args)
				.WithParsedAsync(async o =>
					await ProcessMode(services.BuildServiceProvider().GetRequiredService<ICommandProcessor>(), o));

      Console.WriteLine("All Done.");
    }

		private static async Task ProcessMode(ICommandProcessor commandProcessor, CmdOptions options)
		{
			if (!CmdOptionsProcessor.Process(options))
				return;

			switch (options.Mode)
			{
        case "export":
          await commandProcessor.ExportData(Map(options));
          break;
        default:
          var accountsData = await commandProcessor.ShowLastMonth(Map(options));
					var output = ConsoleFormatter.GetOutput(accountsData);
					Console.WriteLine(output);
					break;
			}
		}

		private static Command Map(CmdOptions options)
		{
			return new Command(
				options.Mode,
				options.Username,
				options.Password,
				options.StartDate,
				options.EndDate);
		}
	}
}
