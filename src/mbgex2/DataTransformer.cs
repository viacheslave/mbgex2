using System.Collections.Generic;
using System.Linq;

namespace mbgex2
{
	internal static class DataTransformer
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
					grp.SelectMany(i => i.Lines.Select(line => new Utility(line, i.Month))).ToList()))
				.ToList();

			return accounts;
		}
	}
}
