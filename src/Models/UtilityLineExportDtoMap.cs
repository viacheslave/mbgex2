using CsvHelper.Configuration;

namespace mbgex2
{
	/// <summary>
	///		CSV class map
	/// </summary>
	public sealed class UtilityLineExportDtoMap : ClassMap<UtilityLineExportDto>
	{
		public UtilityLineExportDtoMap()
		{
			Map(m => m.AccountId).Index(0)
				.Name("ОС.РАХ");

			Map(m => m.Month).Index(1)
				.Name("ДАТА")
				.Convert(o => o.Value.Month.ToString("yyyy-MM-dd"));

			Map(m => m.ServiceName).Index(2)
				.Name("Послуга");

			Map(m => m.FirmName).Index(3)
				.Name("Постачальник послуги");

			Map(m => m.Credited).Index(4)
				.Name("Нараховано");

			Map(m => m.Recalc).Index(5)
				.Name("Перерахунок");

			Map(m => m.Pays).Index(6)
				.Name("Сплачено");

			Map(m => m.Penalty).Index(7)
				.Name("Пеня");

			Map(m => m.Subs).Index(8)
				.Name("Субсидія");

			Map(m => m.Saldo).Index(9)
				.Name("Борг (+), переплата(-)");

			Map(m => m.PaysNew).Index(10)
				.Name("Оплачено після");
		}
	}
}
