using System;

namespace CMFG.DLX.Automation.UIFramework.Utilities
{
	public class RandomGenerator
	{
		private static readonly Random Random = new Random();

		public static int GetRandomNumber(int min, int max)
		{
			lock (Random)
			{
				return Random.Next(min, max);
			}
		}

		public static string GetRandomString(int length)
		{
			return Guid.NewGuid().ToString("N").Substring(0, length);
		}
	}
}