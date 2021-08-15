using System.Collections.Generic;

namespace mbgex2.Domain
{
	public record AccountData
	(
		int AccountId,
		IReadOnlyCollection<Utility> Data
	);
}
