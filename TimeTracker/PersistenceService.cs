using System;
using System.IO;

namespace TimeTracker
{
  public class PersistenceService : IPersistenceService
  {
    public void Save(LogItem logItem)
    {
      var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TimeTracker.log");
      
      using (var streamWriter = new StreamWriter(filePath, true)) 
      {
        streamWriter.WriteLine(logItem.DateTime + " " + logItem.Text);
      }
    }
  }
}