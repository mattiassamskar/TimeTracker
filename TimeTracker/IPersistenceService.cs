namespace TimeTracker
{
  public interface IPersistenceService
  {
    void Save(LogItem logItem);
  }
}