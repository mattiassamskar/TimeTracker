using System;

namespace TimeTracker
{
  public interface IPersistenceService
  {
    void Save(DateTime dateTime, string reason);
  }
}