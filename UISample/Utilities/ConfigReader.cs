using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace UISample
{
    public class ConfigReader 
	{
        private readonly Configuration _config;

        public ConfigReader()
        {
            var configMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/App.qa.config"
            };

            _config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
        }

        public T ReadSetting<T>(string settingName)
        {
            var settingValue = _config.AppSettings.Settings[settingName]?.Value;
            if (settingValue == null)
            {
                return default;
            }

            return (T)Convert.ChangeType(settingValue, typeof(T));
        }

		public string BaseUrl => ReadSetting<string>("BaseUrl");
        public int TimeOut => ReadSetting<int>("TimeOut");
        public string Browser => ReadSetting<string>("Browser");
        public bool RunTestsRemotely => ReadSetting<bool>("RunTestsRemotely");
    }
}