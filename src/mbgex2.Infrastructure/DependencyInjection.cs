using mbgex2.Application;
using Microsoft.Extensions.DependencyInjection;

namespace mbgex2.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddTransient<ICommandProcessor, CommandProcessor>();
			services.AddTransient<IAuthClient, AuthClient>();
			services.AddTransient<IDataProvider, DataProvider>();
			services.AddTransient<IDataExporter, DataExporter>();
			services.AddTransient<ILogger, Logger>();

			return services;
		}
	}
}
