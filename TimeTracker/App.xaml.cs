using System;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;

namespace TimeTracker
{
  public partial class App : Application
  {
    private TaskbarIcon _notifyIcon;

    public App()
    {
      Dispatcher.UnhandledException += (sender, args) => LoggingService.LogException(args.Exception);
    }

    protected override void OnStartup(StartupEventArgs args)
    {
      _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
      SystemEvents.SessionSwitch += (sender, e) => LoggingService.Log(DateTime.UtcNow, e.Reason);
      LoggingService.Log(DateTime.UtcNow, Reason.ApplicationStart);
      base.OnStartup(args);
    }

    protected override void OnExit(ExitEventArgs e)
    {
      LoggingService.Log(DateTime.UtcNow, Reason.ApplicationStop);
      _notifyIcon.Dispose();
      base.OnExit(e);
    }
  }
}
