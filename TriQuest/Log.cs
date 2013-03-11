using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriQuest
{
	public static class Log
	{
		public static void Append(string message)
		{
			Append(message, Color.White);
		}

		public static void Append(string message, Color color)
		{
			entries.Add(new LogEntry(message, color));
		}

		private static List<LogEntry> entries = new List<LogEntry>();

		public static IEnumerable<LogEntry> Entries { get { return entries; } }

		public static IEnumerable<LogEntry> UnreadEntries { get { return Entries.Skip(readCount); } }

		private static int readCount = 0;

		public static void MarkAllRead()
		{
			readCount = Entries.Count();
		}
	}

	public class LogEntry
	{
		public LogEntry(string message, Color color)
		{
			Message = message;
			Color = color;
		}
	
		public string Message { get; private set; }
		public Color Color { get; private set; }
	}
}
