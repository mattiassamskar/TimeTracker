using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace TimeTracker.Function
{
  public static class UploadFunction
  {
    [FunctionName("UploadFunction")]
    public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
      HttpRequestMessage req, TraceWriter log)
    {
      log.Info("C# HTTP trigger function processed a request.");

      dynamic data = await req.Content.ReadAsAsync<object>();
      var logItem = data.ToString();
      
      return req.CreateResponse(HttpStatusCode.OK);
    }
  }
}