using System;

namespace mbgex2.Infrastructure
{
	/// <summary>
	///		CSV line
	/// </summary>
	public class UtilityLineExportDto
	{
		public int AccountId { get; set; }

		public DateTime Month { get; set; }

		public string ServiceName { get; set; }

		public string FirmName { get; set; }

		public double? Credited { get; set; }

		public double? Recalc { get; set; }

		public double? Pays { get; set; }

		public double? Penalty { get; set; }

		public double? Subs { get; set; }

		public double? Saldo { get; set; }

		public double? PaysNew { get; set; }
	}
}
