using System.Collections.Generic;

namespace mbgex2
{
	internal sealed class AuthCookies
	{
		public IReadOnlyCollection<(string name, string value)> Cookies { get; init; }
	}
}
