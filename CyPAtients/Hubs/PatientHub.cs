using Microsoft.AspNetCore.SignalR;

namespace CyPatients.Hubs
{
    public class PatientHub : Hub<IpatientHub>
    {
        
        public override async Task OnConnectedAsync()
        {
            try
            {
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" OnConnectedAsync exception: {ex}");
                throw;
            }
        }


        // Log when a client disconnects from the hub
        public override async Task OnDisconnectedAsync(Exception? exception)
        {


            if (exception == null)
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine("onDisconnected " + exception);


        }
    }
}
