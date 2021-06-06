using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UISample.Chrome
{
	internal class ChromeBrowser
	{
		public readonly string DebuggingPort = "23456";

		public void StartIfNotRunning()
		{
			var baseDir = Path.Combine(Directory.GetCurrentDirectory(), "Chrome");
			var path = Path.Combine(baseDir, "chrome-pid.txt");

			// Attempt to read PID from file, will be 0 if missing
			var pidString = "";
			try
			{
				pidString = File.ReadAllText(path);
			}
			catch (IOException)
			{
			}

			int.TryParse(pidString, out var pid);

			// Check if the Chrome process with that PID is already running
			if (pid > 0 && Process.GetProcesses().Any(p => p.Id == pid && p.ProcessName == "chrome"))
			{
				return;
			}

			// If the process was not found, then create a new one with remote debugging
			var processStartInfo = new ProcessStartInfo
			{
				FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
				UseShellExecute = false
			};

			processStartInfo.ArgumentList.Add($"--remote-debugging-port={DebuggingPort}");

			// Need to use its own user data, otherwise existing Chrome windows can interfere
			var userDataDir = Path.Combine(baseDir, "User Data");
			processStartInfo.ArgumentList.Add($"--user-data-dir={userDataDir}");

			// Start the process
			var process = new Process
			{
				StartInfo = processStartInfo
			};

			process.Start();

			// Store the PID so the process can be found on subsequent runs
			pid = process.Id;
			File.WriteAllText(path, pid.ToString());
		}
	}
}