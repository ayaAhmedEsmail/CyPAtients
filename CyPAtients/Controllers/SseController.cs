using CyPatients.Service.SSE;
using Microsoft.AspNetCore.Mvc;

namespace CyPatients.Controllers
{
    [Route("hubs/patient")]
    [ApiController]
    public class SseController : Controller
    {
        private readonly SseConnectionManager _manger;
        public SseController(SseConnectionManager manger)
        {
            _manger = manger;
        }


        [HttpGet]
        public async Task Connect(CancellationToken cancellation) {
            
            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Connection", "Keep-alive");



            var id = Guid.NewGuid().ToString();
            var writer = new StreamWriter(Response.Body);


            _manger.addClient(id, writer);

            // sinding hartbeat every 5s to make sure the connection still alive
            using var timer =new PeriodicTimer(TimeSpan.FromSeconds(5));

            try
            {
                while (!cancellation.IsCancellationRequested && await timer.WaitForNextTickAsync(cancellation))
                {

                    await writer.WriteLineAsync(": heartbeat\n\n");
                    await writer.WriteLineAsync();
                    await writer.FlushAsync();

                }
            }
            // client closed 
            catch (OperationCanceledException) {
            }
            catch( Exception ex) {
                Console.WriteLine($"SSE Error: {ex.Message}");
            }
            finally { 
            _manger.removeClient(id);
            }
            
        }

    }
}
