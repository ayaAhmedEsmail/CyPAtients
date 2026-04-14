using System.Collections.Concurrent;

namespace CyPatients.Service.SSE
{
    public class SseConnectionManager
    {
        private readonly ConcurrentDictionary<string, StreamWriter> _clients= new();

        public void addClient(string id, StreamWriter writer) {
            _clients[id] = writer;
        }

        public void removeClient(string id) {
            _clients.TryRemove(id, out _);
        }


        public async Task Boadcastasync(string eventName, string data) {
            foreach (var (id,writer) in _clients) {
                var disconnectedClients = new List<string>();
                try {
                    await writer.WriteLineAsync($"event:" + eventName);
                    await writer.WriteLineAsync($"data: "+ data);
                    await writer.WriteLineAsync();
                    await writer.FlushAsync();

                }
                catch {
                   removeClient(id);
                }
            }

        }
    }
}
