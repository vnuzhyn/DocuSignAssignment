using System;

namespace CMFG.DLX.Automation.UIFramework.Utilities
{
	public class DateTimeUtil
	{
		public static string DateTimeNow()
		{
			return $"{DateTime.Now:MMddyyyyHHmm}";
		}

		public static string DateToday()
		{
			return $"{DateTime.Now:MM/dd/yyyy}";
		}

		public static string DateTomorrow()
		{
			return $"{DateTime.Now.AddDays(1):MM/dd/yyyy}";
		}

		public static string DateAfterTomorrow()
		{
			return $"{DateTime.Now.AddDays(2):MM/dd/yyyy}";
		}

		public static string DateYesterday()
		{
			return $"{DateTime.Now.AddDays(-1):MM/dd/yyyy}";
		}

		public static string ConvertToShortDate(string dateToConvert)
		{
			return Convert.ToDateTime(dateToConvert).ToString("M/d/yyyy");
		}
	}
}