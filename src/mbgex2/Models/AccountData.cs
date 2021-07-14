using System.Collections.Generic;

namespace mbgex2
{
	internal record AccountData
	(
		int AccountId,
		IReadOnlyCollection<Utility> Data
	);
}
