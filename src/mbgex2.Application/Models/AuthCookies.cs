using System.Collections.Generic;

namespace mbgex2.Application
{
	public sealed class AuthCookies
	{
		public IReadOnlyCollection<(string name, string value)> Cookies { get; init; }
	}
}
