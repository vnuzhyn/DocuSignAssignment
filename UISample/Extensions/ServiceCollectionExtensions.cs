using Microsoft.Extensions.DependencyInjection;

namespace UISample.Extensions
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