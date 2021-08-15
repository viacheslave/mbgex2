using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using mbgex2.Application;

namespace mbgex2.Infrastructure
{
	internal sealed class DataProvider : IDataProvider
	{
		private readonly ILogger _logger;

		public DataProvider(ILogger logger)
		{
			_logger = logger;
		}

		/// <summary>
		///		Gets raw data for accounts 1-3 (max 3) within provided dates range
		/// </summary>
		/// <param name="dates">Dates range</param>
		/// <returns>Raw data lines</returns>
		public async Task<IReadOnlyCollection<UtilityLinesDto>> Grab(Dates dates, AuthCookies authCookies)
		{
			_logger.Out($"Fetching accounts...");

			var results = new List<UtilityLinesDto>();

			foreach (var accountId in Enumerable.Range(1, 3))
			{
				for (var date = dates.StartDate; date <= dates.EndDate; date = date.AddMonths(1))
				{
					var result = await Grab(accountId, date, authCookies);
					results.Add(result);

					_logger.Out($"Fetched: {accountId}, {date:yyyy-MM}");
				}
			}

			return results
				.Where(r => r.Lines.Any())
				.ToList();
		}

		private async Task<UtilityLinesDto> Grab(int accountId, DateTime date, AuthCookies authCookies)
		{
			var request = new FlurlRequest(new Url($"https://erc.megabank.ua/ua/service/resp/debt/{accountId}/{date:yyyyMM}"));

			foreach (var (name, value) in authCookies.Cookies)
				request.WithCookie(name, value);

			var response = await request
				.GetStringAsync();

			var lines = JsonSerializer.Deserialize<UtilityLineDto[]>(response);

			return new UtilityLinesDto(accountId, date, lines);
		}
	}
}
