using mbgex2.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mbgex2.Application
{
	public class CommandProcessor : ICommandProcessor
	{
		private readonly IDataProvider _dataProvider;
		private readonly IDataExporter _dataExporter;
		private readonly IAuthClient _authClient;
		private readonly ILogger _logger;

		public CommandProcessor(IDataProvider dataProvider, IDataExporter dataExporter, IAuthClient authClient, ILogger logger)
		{
			_dataProvider = dataProvider;
			_dataExporter = dataExporter;
			_authClient = authClient;
			_logger = logger;
		}

		public async Task ExportData(Command options)
		{
			// get data
			var data = await GetRawData(options);
			var accounts = DataMapper.BuildAccounts(data);

			// export
			await _dataExporter.SaveRaw(data);
			_dataExporter.SaveAccounts(accounts);

			_logger.Out($"Export folder: {_dataExporter.ExportFolder.FullName}");
		}

		public async Task<IReadOnlyCollection<AccountData>> ShowLastMonth(Command options)
		{
			// get data
			var data = await GetRawData(options);
			return DataMapper.BuildAccounts(data);
		}

		private async Task<IReadOnlyCollection<UtilityLinesDto>> GetRawData(Command options)
		{
			// get auth cookies
			var authCookies = await _authClient.GetAuthCookies(
					new UserCredentials(options.Username, options.Password));

			// get data
			var data = await _dataProvider.Grab(
				new Dates(options.Start, options.End), authCookies);

			return data;
		}
	}
}
