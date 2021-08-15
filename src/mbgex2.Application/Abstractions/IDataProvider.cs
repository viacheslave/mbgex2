using System.Collections.Generic;
using System.Threading.Tasks;

namespace mbgex2.Application
{
	public interface IDataProvider
	{
		Task<IReadOnlyCollection<UtilityLinesDto>> Grab(Dates dates, AuthCookies authCookies);
	}
}