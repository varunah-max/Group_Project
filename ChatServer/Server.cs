using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class Server
    {
        private readonly int _port;
        private TcpListener? _listener;

        private readonly List<TcpClient> _clients = new List<TcpClient>();
        private readonly object _clientsLock = new object();

        private readonly Dictionary<TcpClient, string> _clientNames = new Dictionary<TcpClient, string>();
        private readonly List<string> _bannedUsers = new List<string>();
        private readonly List<string> _pendingUsers = new List<string>();

        public Server(int port)
        {
            _port = port;
        }

        public async Task StartAsync()
        {
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();

            Console.WriteLine($"Server started successfully on port {_port}");
            Console.WriteLine("Waiting for clients...");

            while (true)
            {
                try
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();

                    lock (_clientsLock)
                    {
                        _clients.Add(client);
                    }

                    Console.WriteLine($"Client connected. Online: {_clients.Count}");
                    _ = HandleClientAsync(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection error: {ex.Message}");
                }
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[4096];

                while (client.Connected)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    if (string.IsNullOrWhiteSpace(message)) continue;

                    Console.WriteLine($"Received: {message}");
                    await ProcessMessageAsync(client, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally
            {
                RemoveClient(client);
                try { client.Close(); } catch { }
                Console.WriteLine("Client disconnected.");
            }
        }

        private async Task ProcessMessageAsync(TcpClient sender, string message)
        {
            if (message.StartsWith("GENERAL:"))
            {
                string text = message.Substring("GENERAL:".Length);
                await BroadcastMessageAsync($"GENERAL:{text}", sender);
                return;
            }

            if (message.StartsWith("PRIVATE:"))
            {
                await SendPrivateMessageAsync(sender, message);
                return;
            }

            if (message == "USERS")
            {
                await SendUsersListAsync(sender);
                return;
            }

            if (message == "PING")
            {
                await SendMessageAsync(sender, "PONG");
                return;
            }

            await ProcessAdminAndModesAsync(sender, message);
        }

        private async Task BroadcastMessageAsync(string message, TcpClient? sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            List<TcpClient> clients;

            lock (_clientsLock)
            {
                clients = new List<TcpClient>(_clients);
            }

            foreach (TcpClient client in clients)
            {
                if (client == sender || !client.Connected) continue;

                try
                {
                    await client.GetStream().WriteAsync(data, 0, data.Length);
                }
                catch
                {
                    RemoveClient(client);
                }
            }
            Console.WriteLine($"Multicast: {message}");
        }

        private async Task SendPrivateMessageAsync(TcpClient sender, string message)
        {
            string data = message.Substring("PRIVATE:".Length);
            int separator = data.IndexOf(':');

            if (separator <= 0)
            {
                await SendMessageAsync(sender, "ERROR: Invalid private message");
                return;
            }

            string username = data.Substring(0, separator);
            string text = data.Substring(separator + 1);

            await SendMessageAsync(sender, $"PRIVATE_TO:{username}:{text}");
        }

        private async Task SendUsersListAsync(TcpClient client)
        {
            List<TcpClient> clients;
            lock (_clientsLock)
            {
                clients = new List<TcpClient>(_clients);
            }
            await SendMessageAsync(client, $"USERS:{clients.Count}");
        }

        private async Task SendMessageAsync(TcpClient client, string message)
        {
            if (!client.Connected) return;
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                await client.GetStream().WriteAsync(data, 0, data.Length);
            }
            catch
            {
                RemoveClient(client);
            }
        }

        private void RemoveClient(TcpClient client)
        {
            lock (_clientsLock)
            {
                _clients.Remove(client);
                _clientNames.Remove(client);
            }
            Console.WriteLine($"Clients online: {_clients.Count}");
            _ = SendAdvancedUsersListAsync();
        }
        private async Task ProcessAdminAndModesAsync(TcpClient sender, string message)
        {
            if (message.StartsWith("LOGIN:") || message.StartsWith("REGISTER:"))
            {
                string command = message.StartsWith("LOGIN:") ? "LOGIN:" : "REGISTER:";
                string data = message.Substring(command.Length).Trim();

                string[] parts = data.Split(':');
                string name = parts[0];
                string password = parts.Length > 1 ? parts[1] : "";

                if (_bannedUsers.Contains(name))
                {
                    await SendMessageAsync(sender, "SYSTEM:Ви заблоковані!");
                    RemoveClient(sender);
                    sender.Close();
                    return;
                }

                lock (_clientsLock)
                {
                    _clientNames[sender] = name;
                    _pendingUsers.Add(name);
                }

                if (command == "REGISTER:")
                {
                    await SendMessageAsync(sender, "SYSTEM:Реєстрація успішна! Очікуйте на схвалення адміном.");
                }
                else
                {
                    await SendMessageAsync(sender, "SYSTEM:Ви успішно увійшли! Очікуйте на схвалення.");
                }

                await SendAdvancedUsersListAsync();
                return;
            }

            if (message.StartsWith("APPROVE:"))
            {
                string target = message.Substring("APPROVE:".Length).Trim();
                if (_pendingUsers.Contains(target)) _pendingUsers.Remove(target);
                await BroadcastMessageAsync($"SYSTEM:Користувача {target} схвалено адміністратором!", null);
                await SendAdvancedUsersListAsync();
                return;
            }

            if (message.StartsWith("KICK:"))
            {
                string target = message.Substring("KICK:".Length).Trim();
                KickOrBanUser(target, "SYSTEM:Вас видалено з чату.", false);
                return;
            }

            if (message.StartsWith("BAN:"))
            {
                string target = message.Substring("BAN:".Length).Trim();
                if (!_bannedUsers.Contains(target)) _bannedUsers.Add(target);
                KickOrBanUser(target, "SYSTEM:Вас забанено адміністратором!", true);
                return;
            }

            if (message.StartsWith("PRIVATE:"))
            {
                await SendPrivateMsgAsync(sender, message);
                return;
            }

            await SendMessageAsync(sender, "ERROR: Unknown command");
        }

        private async Task SendAdvancedUsersListAsync()
        {
            string usersInfo = "";
            lock (_clientsLock)
            {
                var list = new List<string>();
                foreach (var pair in _clientNames)
                {
                    string status = _pendingUsers.Contains(pair.Value) ? " (Очікує)" : "";
                    list.Add(pair.Value + status);
                }
                usersInfo = string.Join(",", list);
            }

            byte[] data = Encoding.UTF8.GetBytes($"USERS_LIST:{usersInfo}\n");
            List<TcpClient> clients;
            lock (_clientsLock) { clients = new List<TcpClient>(_clients); }

            foreach (var c in clients)
            {
                if (c.Connected) { try { await c.GetStream().WriteAsync(data, 0, data.Length); } catch { } }
            }
        }

        private async Task SendPrivateMsgAsync(TcpClient sender, string message)
        {
            string data = message.Substring("PRIVATE:".Length);
            int idx = data.IndexOf(':');
            if (idx <= 0) return;

            string targetName = data.Substring(0, idx).Trim();
            string text = data.Substring(idx + 1);

            string senderName = "Гість";
            lock (_clientsLock)
            {
                if (_clientNames.ContainsKey(sender)) senderName = _clientNames[sender];
            }

            TcpClient? targetClient = null;
            lock (_clientsLock)
            {
                targetClient = _clientNames.FirstOrDefault(x => x.Value.Trim().Equals(targetName, StringComparison.OrdinalIgnoreCase)).Key;
            }

            if (targetClient != null && targetClient.Connected)
            {
                byte[] dataToTarget = Encoding.UTF8.GetBytes($"PRIVATE_FROM:{senderName}:{text}\n");
                await targetClient.GetStream().WriteAsync(dataToTarget, 0, dataToTarget.Length);

                byte[] dataToSender = Encoding.UTF8.GetBytes($"PRIVATE_FROM:Ви -> {targetName}:{text}\n");
                await sender.GetStream().WriteAsync(dataToSender, 0, dataToSender.Length);
            }
            else
            {
                byte[] errorData = Encoding.UTF8.GetBytes($"SYSTEM:Користувач {targetName} не в мережі або не знайдений.\n");
                await sender.GetStream().WriteAsync(errorData, 0, errorData.Length);
            }
        }

        private void KickOrBanUser(string username, string reason, bool isBan)
        {
            TcpClient? target = null;
            lock (_clientsLock)
            {
                target = _clientNames.FirstOrDefault(x => x.Value.StartsWith(username)).Key;
            }
            if (target != null)
            {
                _ = SendMessageAsync(target, reason);
                Task.Delay(500).ContinueWith(_ =>
                {
                    RemoveClient(target);
                    try { target.Close(); } catch { }
                });
            }
        }
    }
}