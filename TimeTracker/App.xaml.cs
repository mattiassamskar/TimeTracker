using System;
using System.Windows;
using Microsoft.Win32;

namespace TimeTracker
{
  public partial class App : Application
  {
    private System.Windows.Forms.NotifyIcon _notifyIcon;

    public App()
    {
      Dispatcher.UnhandledException += (sender, args) => LoggingService.LogException(args.Exception);
    }

    protected override void OnStartup(StartupEventArgs args)
    {
      _notifyIcon = new System.Windows.Forms.NotifyIcon
      {
        Icon = Resource.TimeTracker,
        Visible = true,
        ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip()
      };

      _notifyIcon.ContextMenuStrip.Items.Add("Close").Click += (s, e) => Current.Shutdown();

      SystemEvents.SessionSwitch += (sender, e) => LoggingService.Log(DateTime.UtcNow, e.Reason);
      LoggingService.Log(DateTime.UtcNow, Reason.ApplicationStart);
      base.OnStartup(args);
    }

    protected override void OnExit(ExitEventArgs e)
    {
      LoggingService.Log(DateTime.UtcNow, Reason.ApplicationStop);
      _notifyIcon.Dispose();
      _notifyIcon = null;
      base.OnExit(e);
    }
  }
}
