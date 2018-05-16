using System;
using System.Windows;
using Microsoft.Win32;
using SimpleInjector;

namespace TimeTracker
{
  public partial class App : Application
  {
    private ILoggingService _loggingService;
    private System.Windows.Forms.NotifyIcon _notifyIcon;

    protected override void OnStartup(StartupEventArgs args)
    {
      Bootstrap();

      base.OnStartup(args);
    }

    protected override void OnExit(ExitEventArgs e)
    {
      _loggingService.Log(DateTime.UtcNow, Reason.ApplicationStop);
      _notifyIcon.Dispose();
      _notifyIcon = null;

      base.OnExit(e);
    }

    private void Bootstrap()
    {
      var container = new Container();
      container.Register<ILoggingService, LoggingService>(Lifestyle.Singleton);
      container.Register<IPersistenceService, PersistenceService>(Lifestyle.Singleton);
      container.Verify();

      _loggingService = container.GetInstance<ILoggingService>();
      _loggingService.Log(DateTime.UtcNow, Reason.ApplicationStart);

      SystemEvents.SessionSwitch += (sender, e) => _loggingService.Log(DateTime.UtcNow, e.Reason);

      _notifyIcon = new System.Windows.Forms.NotifyIcon
      {
        Icon = Resource.TimeTracker,
        Visible = true,
        ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip()
      };
      _notifyIcon.ContextMenuStrip.Items.Add("Close").Click += (s, e) => Current.Shutdown();
    }
  }
}
