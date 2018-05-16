using System;
using Microsoft.Win32;

namespace TimeTracker
{
  public interface ILoggingService
  {
    void LogException(Exception exception);
    void Log(DateTime dateTime, SessionSwitchReason sessionSwitchReason);
    void Log(DateTime dateTime, Reason reason);
  }
}