using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using mbgex2.Application;
using mbgex2.Domain;

namespace mbgex2.Infrastructure
{
	internal sealed class DataExporter : IDataExporter
	{
		private readonly ILogger _logger;

		public DirectoryInfo ExportFolder { get; private set; }

		private readonly JsonSerializerOptions _serializerOptions = new()
		{
			WriteIndented = true,
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
		};

		public DataExporter(ILogger logger)
		{
			_logger = logger;

			EnsureExportFolder();
		}

		/// <summary>
		///		Exports raw data, into JSON files
		/// </summary>
		/// <param name="dtos">Utilities raw data</param>
		public async Task SaveRaw(IReadOnlyCollection<UtilityLinesDto> dtos)
		{
			_logger.Out($"Exporting raw data...");

			foreach (var dto in dtos)
			{
				var fileName = Path.Combine(ExportFolder.FullName, $"raw-acc{dto.AccountId}-{dto.Month:yyyyMM}.json");

				var json = JsonSerializer.Serialize(dto.Lines, _serializerOptions);

				await File.WriteAllTextAsync(fileName, json);

				_logger.Out($"Raw: {fileName}");
			}
		}

		/// <summary>
		///		Exports data by account and utility, into CSV files
		/// </summary>
		/// <param name="accounts">Accounts data</param>
		public void SaveAccounts(IReadOnlyCollection<AccountData> accounts)
		{
			var csvConfiguration = new CsvConfiguration(CultureInfo.GetCultureInfo("uk-UA"));

			_logger.Out($"Exporting CSV data...");

			foreach (var account in accounts)
			{
				var accountId = account.AccountId;

				foreach (var utilityGrp in account.Data.GroupBy(u => u.ServiceName))
				{
					var utility = utilityGrp.Key;
					var models = utilityGrp.ToList();

					var exported = models
						.Select(model => new UtilityLineExportDto
						{
							AccountId = accountId,
							Month = model.Month,
							ServiceName = model.ServiceName,
							FirmName = model.FirmName,
							Credited = model.Credited,
							Pays = model.Pays,
							PaysNew = model.PaysNew,
							Subs = model.Subs,
							Saldo = model.Saldo,
							Penalty = model.Penalty,
							Recalc = model.Recalc
						})
						.ToList();

					var fileName = Path.Combine(ExportFolder.FullName, $"acc{accountId}-{utility}.csv");

					using var tw = new StreamWriter(fileName);
					using var csv = new CsvWriter(tw, csvConfiguration);

					csv.Context.RegisterClassMap<UtilityLineExportDtoMap>();
					csv.WriteRecords(exported);
				}
			}
		}

		private void EnsureExportFolder()
		{
			var folder = $"export-{DateTime.Now:yyyyMMdd-HHmm}";

			ExportFolder = new DirectoryInfo(folder);

			if (!Directory.Exists(folder))
				ExportFolder = Directory.CreateDirectory(folder);
		}
	}
}
