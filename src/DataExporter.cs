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

namespace mbgex2
{
	internal sealed class DataExporter
	{
		private DirectoryInfo _exportFolder;

		private readonly JsonSerializerOptions _serializerOptions = new()
		{
			WriteIndented = true,
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
		};

		public DataExporter()
		{
			EnsureExportFolder();
		}

		/// <summary>
		///		Exports raw data, into JSON files
		/// </summary>
		/// <param name="dtos">Utilities raw data</param>
		public async Task SaveRaw(IReadOnlyCollection<UtilityLinesDto> dtos)
		{
			Logger.Out($"Exporting raw data...");

			foreach (var dto in dtos)
			{
				var fileName = Path.Combine(
					_exportFolder.FullName, 
					$"raw-acc{dto.AccountId}-{dto.Month:yyyyMM}.json");

				var json = JsonSerializer.Serialize(dto.Lines.Select(l => l.Value), _serializerOptions);

				await File.WriteAllTextAsync(fileName, json);

				Logger.Out($"Raw: {fileName}");
			}

			Logger.Out($"Raw export folder: {_exportFolder.FullName}");
		}

		/// <summary>
		///		Exports data by account and utility, into CSV files
		/// </summary>
		/// <param name="dtos">Utilities raw data</param>
		public void SaveByUtility(IReadOnlyCollection<UtilityLinesDto> dtos)
		{
			var csvConfiguration = new CsvConfiguration(CultureInfo.GetCultureInfo("uk-UA"))
			{
			};

			Logger.Out($"Exporting CSV data...");

			var utilities = dtos
				.SelectMany(dto => dto.Lines.Select(l => new Utility(l.Value, dto.AccountId, dto.Month)))
				.ToList();

			foreach (var accountGrp in utilities.GroupBy(u => u.AccountId))
			{
				var accountId = accountGrp.Key;

				foreach (var utilityGrp in accountGrp.GroupBy(u => u.ServiceName))
				{
					var utility = utilityGrp.Key;
					var models = utilityGrp.ToList();

					var exported = models
						.Select(model => new UtilityLineExportDto {
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

					var fileName = Path.Combine(
						_exportFolder.FullName,
						$"acc{accountId}-{utility}.csv");

					using var tw = new StreamWriter(fileName);
					using var csv = new CsvWriter(tw, csvConfiguration);

					csv.Context.RegisterClassMap<UtilityLineExportDtoMap>();
					csv.WriteRecords(exported);
				}
			}

			Logger.Out($"CSV export folder: {_exportFolder.FullName}");
		}

		private void EnsureExportFolder()
		{
			var folder = $"export-{DateTime.Now:yyyyMMdd-HHmm}";

			_exportFolder = new DirectoryInfo(folder);

			if (!Directory.Exists(folder))
				_exportFolder = Directory.CreateDirectory(folder);
		}
	}
}
