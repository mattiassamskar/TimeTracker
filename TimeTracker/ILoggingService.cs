using System;

namespace TimeTracker
{
  public interface ILoggingService
  {
    void Log(string text, Action<LogItem> saveAction);
  }
}