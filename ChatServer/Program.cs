using ChatServer;

namespace ChatServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const int port = 5000;

            Server server = new Server(port);

            Console.WriteLine("=================================");
            Console.WriteLine("          CHAT SERVER");
            Console.WriteLine("=================================");
            Console.WriteLine($"Port: {port}");
            Console.WriteLine("Server is starting...");
            Console.WriteLine();

            await server.StartAsync();
        }
    }
}