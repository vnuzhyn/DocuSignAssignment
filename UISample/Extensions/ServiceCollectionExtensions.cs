using Microsoft.Extensions.DependencyInjection;

namespace CMFG.DLX.Automation.UIFramework.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddDebugSettings<TImplementation>(this IServiceCollection services)
			where TImplementation : class, IDebugSettings
		{
			services.AddTransient<IDebugSettings, TImplementation>();
		}
	}
}