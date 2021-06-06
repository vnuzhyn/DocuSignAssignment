using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UISample.Driver
{
	public class ExceptionLogger
	{
		private static readonly string[] AssembliesToIgnore =
		{
			"CMFG.DLX.Automation.UIFramework", "xunit.assert"
		};

		public void Log(Exception e)
		{
			if (e == null)
			{
				return;
			}

			// Attempt to get the file from the stack trace, and if not, just quit without printing anything
			var stackTrace = new StackTrace(e, true);
			var frames = stackTrace.GetFrames();
			// Select the first frame that isn't in UI Framework or XUnit
			var frame = frames.FirstOrDefault(
				f =>
				{
					if (f == null)
					{
						return false;
					}

					var declaringType = f.GetMethod()?.DeclaringType;
					return declaringType != null && !AssembliesToIgnore.Contains(declaringType.Assembly.GetName().Name);
				});

			var filePath = frame?.GetFileName();
			if (filePath == null)
			{
				return;
			}

			var lineIndex = frame.GetFileLineNumber() - 1;
			IEnumerable<string> lines;
			try
			{
				lines = File.ReadLines(filePath);
			}
			catch (IOException)
			{
				return;
			}

			// Print 2 lines before and after, and highlight the line with the exception
			var exLines = lines.Select((l, i) => (l, i)).Skip(lineIndex - 2).Take(5);
			var fileName = Path.GetFileName(filePath);
			Console.WriteLine("======================================================");
			Console.WriteLine($"In {fileName}, line {frame.GetFileLineNumber()}:");
			Console.WriteLine("======================================================");
			foreach (var (line, i) in exLines)
			{
				var highlight = lineIndex == i ? ">>>" : "   ";
				Console.WriteLine($"{i + 1,4}:{highlight}{line}");
			}

			Console.WriteLine("======================================================");
		}
	}
}