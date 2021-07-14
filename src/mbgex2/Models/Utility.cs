using System;

namespace mbgex2
{
	internal sealed class Utility
	{
		public Utility(UtilityLineDto dto, DateTime date)
		{
			ServiceName = dto.servicename;
			FirmName = dto.firmname;

			Credited = TryParse(dto.credited);
			Recalc = TryParse(dto.recalc);
			Pays = TryParse(dto.pays);
			Penalty = TryParse(dto.penalty);
			Subs = TryParse(dto.subs);
			Saldo = TryParse(dto.saldo);
			PaysNew = TryParse(dto.paysnew);
			Month = date;
		}

		private static double? TryParse(string rawValue) => double.TryParse(rawValue, out var value) ? value : default;

		public DateTime Month { get; }

		public string ServiceName { get; }

		public string FirmName { get; }

		public double? Credited { get; }

		public double? Recalc { get; }

		public double? Pays { get; }

		public double? Penalty { get; }

		public double? Subs { get; }

		public double? Saldo { get; }

		public double? PaysNew { get; }
	}
}