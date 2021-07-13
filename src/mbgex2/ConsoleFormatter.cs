using System.Collections.Generic;
using System.Text;

namespace mbgex2
{
	internal static class ConsoleFormatter
	{
		public static string GetOutput(IReadOnlyCollection<UtilityLinesDto> data)
		{
			var sb = new StringBuilder();

			foreach (var dataItem in data)
			{
				sb.AppendLine();
				sb.AppendLine(new string('-', 53));
				sb.AppendLine($"Account {dataItem.AccountId}: {dataItem.Month:yyyy-MM}");
				sb.AppendLine(new string('-', 53));

				foreach (var lineItem in dataItem.Lines)
				{
					var utility = new Utility(lineItem.Value, dataItem.AccountId, dataItem.Month);
					sb.AppendLine($"{utility.ServiceName,-32} : {utility.Credited?.ToString(),7} {utility.Saldo?.ToString(),10}");
				}
			}

			return sb.ToString();
		}
	}
}
