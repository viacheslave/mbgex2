using mbgex2.Domain;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace mbgex2.Application
{
	public interface IDataExporter
	{
		DirectoryInfo ExportFolder { get; }

		void SaveAccounts(IReadOnlyCollection<AccountData> accounts);

		Task SaveRaw(IReadOnlyCollection<UtilityLinesDto> dtos);
	}
}