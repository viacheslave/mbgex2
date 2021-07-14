using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace mbgex2
{
	internal sealed class DataProvider
	{
		private readonly AuthCookies _authCookies;

		public DataProvider(AuthCookies authCookies)
		{
			_authCookies = authCookies ?? throw new ArgumentNullException(nameof(authCookies));
		}

		/// <summary>
		///		Gets raw data for accounts 1-3 (max 3) within provided dates range
		/// </summary>
		/// <param name="dates">Dates range</param>
		/// <returns>Raw data lines</returns>
		public async Task<IReadOnlyCollection<UtilityLinesDto>> Grab(Dates dates)
		{
			Logger.Out($"Fetching accounts...");

			var results = new List<UtilityLinesDto>();

			foreach (var accountId in Enumerable.Range(1, 3))
			{
				for (var date = dates.StartDate; date <= dates.EndDate; date = date.AddMonths(1))
				{
					var result = await Grab(accountId, date);
					results.Add(result);

					Logger.Out($"Fetched: {accountId}, {date:yyyy-MM}");
				}
			}

			return results
				.Where(r => r.Lines.Any())
				.ToList();
		}

		private async Task<UtilityLinesDto> Grab(int accountId, DateTime date)
		{
			var request = new FlurlRequest(new Url($"https://erc.megabank.ua/ua/service/resp/debt/{accountId}/{date:yyyyMM}"));

			foreach (var authCookie in _authCookies.Cookies)
				request.WithCookie(authCookie.name, authCookie.value);

			var response = await request
				.GetStringAsync();

			var lines = JsonSerializer.Deserialize<UtilityLineDto[]>(response);

			return new UtilityLinesDto(accountId, date, lines);
		}
	}
}
