using System;

namespace mbgex2.Domain
{
	public record Utility(
		DateTime Month,
		string ServiceName,
		string FirmName,
		double? Credited,
		double? Recalc,
		double? Pays,
		double? Penalty,
		double? Subs,
		double? Saldo,
		double? PaysNew
	);
}