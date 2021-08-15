using mbgex2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mbgex2.Application
{
	internal static class DataMapper
	{
		/// <summary>
		///		Builds accounts data from DTOs
		/// </summary>
		/// <param name="dtos">Raw DTOs</param>
		/// <returns>Collection of accounts</returns>
		public static IReadOnlyCollection<AccountData> BuildAccounts(IReadOnlyCollection<UtilityLinesDto> dtos)
		{
			var accounts = dtos
				.GroupBy(dto => dto.AccountId)
				.Select(grp => new AccountData(
					grp.Key, 
					grp.SelectMany(i => i.Lines.Select(line => BuildUtility(line, i.Month))).ToList()))
				.ToList();

			return accounts;
		}

		private static Utility BuildUtility(UtilityLineDto utilityLineDto, DateTime date)
		{
			return new Utility(
				Month: date,
				ServiceName: utilityLineDto.servicename,
				FirmName: utilityLineDto.firmname,

				Credited: TryParse(utilityLineDto.credited),
				Recalc: TryParse(utilityLineDto.recalc),
				Pays: TryParse(utilityLineDto.pays),
				Penalty: TryParse(utilityLineDto.penalty),
				Subs: TryParse(utilityLineDto.subs),
				Saldo: TryParse(utilityLineDto.saldo),
				PaysNew: TryParse(utilityLineDto.paysnew)
			);

			static double? TryParse(string rawValue) => double.TryParse(rawValue, out var value) ? value : default;
		}
	}
}
