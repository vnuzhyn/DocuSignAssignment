using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace CMFG.DLX.Automation.UIFramework.Utilities
{
	public class EnvironmentReader
	{
		private readonly Configuration _config;

		public EnvironmentReader()
		{
			var environmentName = GetEnvironmentName();
			var configMap = new ExeConfigurationFileMap
			{
				ExeConfigFilename = $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/App.{environmentName}.config"
			};

			_config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
		}

		/// <summary>
		///     Reads configuration settings from App.{}environment.config
		/// </summary>
		/// <typeparam name="T">The parameter type</typeparam>
		/// <param name="settingName">The setting name</param>
		/// <returns>Typed setting value</returns>
		protected T ReadSetting<T>(string settingName)
		{
			var settingValue = _config.AppSettings.Settings[settingName]?.Value;
			if (settingValue == null)
			{
				return default;
			}

			return (T) Convert.ChangeType(settingValue, typeof(T));
		}

		public string GetEnvironmentName()
		{
			var environmentName = Environment.GetEnvironmentVariable("SPECFLOW_ENVIRONMENT");
			if (string.IsNullOrWhiteSpace(environmentName))
			{
				throw new Exception("SPECFLOW_ENVIRONMENT environment variable is not defined");
			}

			return environmentName.ToLower();
		}

        public string LoxUrl => ReadSetting<string>("LOXURL");
        public string DlxApiBaseUrl => ReadSetting<string>("DLXAPIBaseUrl");
	}
}