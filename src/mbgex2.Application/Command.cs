using System;

namespace mbgex2.Application
{
	public record Command (
			string Mode,
			string Username,
			string Password,
			string StartDate,
			string EndDate
		)
	{
		public DateTime Start => DateTime.Parse(StartDate);
		public DateTime End => DateTime.Parse(EndDate);
	}
}
