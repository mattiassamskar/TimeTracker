using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;

namespace TimeTracker
{
  public partial class App : Application
  {
    private readonly ILoggingService _loggingService;
    private readonly IPersistenceService _persistenceService;
    private System.Windows.Forms.NotifyIcon _notifyIcon;

    public App()
    {
      _notifyIcon = new System.Windows.Forms.NotifyIcon
      {
        Icon = Resource.TimeTracker,
        Visible = true,
        ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip()
      };
      _notifyIcon.ContextMenuStrip.Items.Add("Close").Click += (s, e) => Current.Shutdown();
      _loggingService = new LoggingService();
      _persistenceService = new PersistenceService();
    }

    protected override void OnStartup(StartupEventArgs args)
    {
      Log(Reason.ApplicationStart.ToString());
      SystemEvents.SessionSwitch += (sender, e) => Log(e.Reason.ToString());

      base.OnStartup(args);
    }

    protected override void OnExit(ExitEventArgs e)
    {
      Log(Reason.ApplicationStop.ToString());
      _notifyIcon.Dispose();
      _notifyIcon = null;

      base.OnExit(e);
    }

    private void Log(string text)
    {
      try
      {
        _loggingService.Log(text, _persistenceService.Save);
      }
      catch (Exception e)
      {
        using (var eventLog = new EventLog("Application"))
        {
          eventLog.Source = "Application";
          eventLog.WriteEntry(
            e.Message + Environment.NewLine + e.InnerException?.Message,
            EventLogEntryType.Warning,
            100,
            1);
        }
      }
    }
  }
}