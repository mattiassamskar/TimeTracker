using System;

namespace TimeTracker
{
  public class LoggingService : ILoggingService
  {
    public void Log(string text, Action<LogItem> saveAction)
    {
        saveAction(new LogItem
        {
          DateTime = DateTime.UtcNow,
          Text = text
        });
    }
  }
}