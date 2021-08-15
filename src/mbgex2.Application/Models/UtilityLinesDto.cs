using System;
using System.Collections.Generic;

namespace mbgex2.Application
{
	public sealed record UtilityLinesDto
	(
		int AccountId,
		DateTime Month,
		IReadOnlyCollection<UtilityLineDto> Lines
	);
}
