using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mbgex2
{
	internal static class ConsoleFormatter
	{
		public static string GetOutput(IReadOnlyCollection<AccountData> accounts)
		{
			var sb = new StringBuilder();

			foreach (var account in accounts)
			{
				sb.AppendLine();
				sb.AppendLine(new string('-', 53));
				sb.AppendLine($"Account {account.AccountId}: {account.Data.Select(d => d.Month).First():yyyy-MM}");
				sb.AppendLine(new string('-', 53));

				foreach (var item in account.Data)
					sb.AppendLine($"{item.ServiceName,-32} : {item.Credited?.ToString(),7} {item.Saldo?.ToString(),10}");
			}

			return sb.ToString();
		}
	}
}
