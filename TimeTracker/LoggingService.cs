using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace TimeTracker
{
  public static class LoggingService
  {
    public static void LogException(Exception exception)
    {
      var message = "Exception:" + Environment.NewLine + 
                    exception.Message + Environment.NewLine +
                    "Inner exception:" + Environment.NewLine + 
                    exception.InnerException?.Message;

      using (var eventLog = new EventLog("Application")) 
      {
        eventLog.Source = "Application";
        eventLog.WriteEntry(message, EventLogEntryType.Warning, 100, 1); 
      }
    }
    
    public static void Log(DateTime dateTime, SessionSwitchReason sessionSwitchReason)
    {
      Log(dateTime, sessionSwitchReason.ToString());
    }

    public static void Log(DateTime dateTime, Reason reason)
    {
      Log(dateTime, reason.ToString());
    }

    private static void Log(DateTime dateTime, string reason)
    {
      try
      {
        Debug.Print(dateTime + " " + reason);
      }
      catch (Exception e)
      {
        LogException(e);
      }
    }
  }
}