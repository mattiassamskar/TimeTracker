using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace TimeTracker
{
  public class LoggingService : ILoggingService
  {
    private readonly IPersistenceService _persistenceService;

    public LoggingService(IPersistenceService persistenceService)
    {
      _persistenceService = persistenceService;
    }

    public void LogException(Exception exception)
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
    
    public void Log(DateTime dateTime, SessionSwitchReason sessionSwitchReason)
    {
      Log(dateTime, sessionSwitchReason.ToString());
    }

    public void Log(DateTime dateTime, Reason reason)
    {
      Log(dateTime, reason.ToString());
    }

    private void Log(DateTime dateTime, string reason)
    {
      try
      {
        _persistenceService.Save(dateTime, reason);
      }
      catch (Exception e)
      {
        LogException(e);
      }
    }
  }
}