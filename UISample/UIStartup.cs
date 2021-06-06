using CMFG.DLX.Automation.UIFramework.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace UISample
{
    public static class UIStartup
	{
		private static IServiceProvider _serviceProvider;

		internal static IServiceProvider ServiceProvider => _serviceProvider;

        internal static ILogger<T> GetLogger<T>()
		{
			return ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger<T>();
		}

		public static void ConfigureServices(IServiceCollection services)
		{
			services.TryAddTransient<IDebugSettings, DefaultDebugSettings>();
			services.TryAddTransient<IWaitProvider, DefaultWaitProvider>();
		}

		public static void SetServiceProvider(IServiceProvider serviceProvider)
		{
			if (_serviceProvider != null)
			{
				throw new InvalidOperationException("Service provider has already been initialized");
			}

			_serviceProvider = serviceProvider;
		}

		public static void ClearServiceProvider()
		{
			_serviceProvider = null;
		}
    }
}