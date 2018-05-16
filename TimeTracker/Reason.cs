namespace TimeTracker
{
  public enum Reason
  {
    None,
    ApplicationStart,
    ApplicationStop,
    SessionLock,
    SessionUnlock,
    SessionLogon,
    SessionLogoff,
    RemoteDisconnect,
    RemoteConnect
  }
}