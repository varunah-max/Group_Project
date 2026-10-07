using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class Server
    {
        private readonly int _port;
        private TcpListener? _listener;

        private readonly List<TcpClient> _clients =
            new List<TcpClient>();

        private readonly object _clientsLock =
            new object();

        public Server(int port)
        {
            _port = port;
        }

        public async Task StartAsync()
        {
            _listener = new TcpListener(
                IPAddress.Any,
                _port
            );

            _listener.Start();

            Console.WriteLine(
                $"Server started successfully on port {_port}"
            );

            Console.WriteLine(
                "Waiting for clients..."
            );

            while (true)
            {
                try
                {
                    TcpClient client =
                        await _listener.AcceptTcpClientAsync();

                    lock (_clientsLock)
                    {
                        _clients.Add(client);
                    }

                    Console.WriteLine(
                        $"Client connected. " +
                        $"Online: {_clients.Count}"
                    );

                    _ = HandleClientAsync(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Connection error: {ex.Message}"
                    );
                }
            }
        }

        private async Task HandleClientAsync(
            TcpClient client)
        {
            try
            {
                using NetworkStream stream =
                    client.GetStream();

                byte[] buffer = new byte[4096];

                while (client.Connected)
                {
                    int bytesRead =
                        await stream.ReadAsync(
                            buffer,
                            0,
                            buffer.Length
                        );

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    string message =
                        Encoding.UTF8.GetString(
                            buffer,
                            0,
                            bytesRead
                        ).Trim();

                    if (string.IsNullOrWhiteSpace(message))
                    {
                        continue;
                    }

                    Console.WriteLine(
                        $"Received: {message}"
                    );

                    await ProcessMessageAsync(
                        client,
                        message
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Client error: {ex.Message}"
                );
            }
            finally
            {
                RemoveClient(client);

                try
                {
                    client.Close();
                }
                catch
                {
                }

                Console.WriteLine(
                    "Client disconnected."
                );
            }
        }

        private async Task ProcessMessageAsync(
            TcpClient sender,
            string message)
        {
            // Загальний чат
            if (message.StartsWith("GENERAL:"))
            {
                string text =
                    message.Substring("GENERAL:".Length);

                await BroadcastMessageAsync(
                    $"GENERAL:{text}",
                    sender
                );

                return;
            }

            // Особисте повідомлення
            if (message.StartsWith("PRIVATE:"))
            {
                await SendPrivateMessageAsync(
                    sender,
                    message
                );

                return;
            }

            // Запит списку користувачів
            if (message == "USERS")
            {
                await SendUsersListAsync(sender);

                return;
            }

            // Перевірка з'єднання
            if (message == "PING")
            {
                await SendMessageAsync(
                    sender,
                    "PONG"
                );

                return;
            }

            // Невідома команда
            await SendMessageAsync(
                sender,
                "ERROR: Unknown command"
            );
        }

        private async Task BroadcastMessageAsync(
            string message,
            TcpClient sender)
        {
            byte[] data =
                Encoding.UTF8.GetBytes(
                    message + "\n"
                );

            List<TcpClient> clients;

            lock (_clientsLock)
            {
                clients = new List<TcpClient>(
                    _clients
                );
            }

            foreach (TcpClient client in clients)
            {
                if (client == sender)
                {
                    continue;
                }

                if (!client.Connected)
                {
                    continue;
                }

                try
                {
                    NetworkStream stream =
                        client.GetStream();

                    await stream.WriteAsync(
                        data,
                        0,
                        data.Length
                    );
                }
                catch
                {
                    RemoveClient(client);
                }
            }

            Console.WriteLine(
                $"Broadcast: {message}"
            );
        }

        private async Task SendPrivateMessageAsync(
            TcpClient sender,
            string message)
        {
            string data =
                message.Substring("PRIVATE:".Length);

            int separator =
                data.IndexOf(':');

            if (separator <= 0)
            {
                await SendMessageAsync(
                    sender,
                    "ERROR: Invalid private message"
                );

                return;
            }

            string username =
                data.Substring(
                    0,
                    separator
                );

            string text =
                data.Substring(
                    separator + 1
                );

            // Поки що повертаємо повідомлення відправнику.
            // Після підключення бази тут буде пошук
            // конкретного користувача.

            await SendMessageAsync(
                sender,
                $"PRIVATE_TO:{username}:{text}"
            );
        }

        private async Task SendUsersListAsync(
            TcpClient client)
        {
            List<TcpClient> clients;

            lock (_clientsLock)
            {
                clients = new List<TcpClient>(
                    _clients
                );
            }

            string response =
                $"USERS:{clients.Count}";

            await SendMessageAsync(
                client,
                response
            );
        }

        private async Task SendMessageAsync(
            TcpClient client,
            string message)
        {
            if (!client.Connected)
            {
                return;
            }

            try
            {
                byte[] data =
                    Encoding.UTF8.GetBytes(
                        message + "\n"
                    );

                NetworkStream stream =
                    client.GetStream();

                await stream.WriteAsync(
                    data,
                    0,
                    data.Length
                );
            }
            catch
            {
                RemoveClient(client);
            }
        }

        private void RemoveClient(
            TcpClient client)
        {
            lock (_clientsLock)
            {
                _clients.Remove(client);
            }

            Console.WriteLine(
                $"Clients online: {_clients.Count}"
            );
        }
    }
}