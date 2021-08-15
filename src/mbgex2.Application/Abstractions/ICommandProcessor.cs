using mbgex2.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mbgex2.Application
{
	public interface ICommandProcessor
	{
		Task ExportData(Command options);

		Task<IReadOnlyCollection<AccountData>> ShowLastMonth(Command options);
	}
}