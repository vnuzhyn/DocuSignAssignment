using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;

namespace UISample.Utilities
{
	public class ScreenshotUtil
	{
		public static void TakeScreenshot(IWebDriver driver, string featureTitle, string scenarioTitle, string textToWrite = "", int textSize = 13)
		{
			try
			{
				var fileName = $"{featureTitle}_{scenarioTitle}_{DateTime.Now:yyyy-MM-dd_HH_mm_ss}";

				var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

				if (!Directory.Exists(artifactDirectory))
				{
					Directory.CreateDirectory(artifactDirectory);
				}

				if (!(driver is ITakesScreenshot takesScreenshot))
				{
					return;
				}

				var screenShot = takesScreenshot.GetScreenshot();
				var bitmap = Image.FromStream(new MemoryStream(screenShot.AsByteArray)) as Bitmap;
				using (var graphics = Graphics.FromImage(bitmap ?? throw new InvalidOperationException()))
				{
					using var arialFont = new Font("Arial", textSize);
					graphics.DrawString(textToWrite, arialFont, Brushes.Black, new PointF(50f, 100f));
				}

				var screenShotFilePath = Path.Combine(artifactDirectory, $"{fileName}.png");
				bitmap.Save(screenShotFilePath, ImageFormat.Png);
				Console.WriteLine($"Screenshot: {new Uri(screenShotFilePath)}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while taking Screenshot: {ex}");
			}
		}
	}
}