using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientChat
{
    public partial class Form1 : Form
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIpAddress.Text) || string.IsNullOrWhiteSpace(txtPort.Text))
            {
                MessageBox.Show("Будь ласка, введіть IP та Порт!");
                return;
            }

            try
            {
                _client?.Close();
                _client = new TcpClient();
                _client.Connect(txtIpAddress.Text, int.Parse(txtPort.Text));
                _stream = _client.GetStream();

                lblStatus.Text = "Статус: Підключено";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                _ = ReceiveMessagesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка підключення: {ex.Message}");
                lblStatus.Text = "Статус: Помилка";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text) || _client == null || !_client.Connected) return;

            if (rbGeneral.Checked)
            {
                // Загальний чат
                SendMessage("GENERAL:" + txtMessage.Text);
                rtbChat.AppendText($"[Я -> Всім]: {txtMessage.Text}\n");
            }
            else if (rbPrivate.Checked)
            {
                if (lstUsers.SelectedItem == null)
                {
                    MessageBox.Show("Будь ласка, виберіть користувача зі списку праворуч для особистого повідомлення!");
                    return;
                }

                string selectedUser = lstUsers.SelectedItem.ToString().Replace(" (Очікує)", "").Trim();
                SendMessage($"PRIVATE:{selectedUser}:{txtMessage.Text}");
                rtbChat.AppendText($"[Я -> {selectedUser}]: {txtMessage.Text}\n");
            }
            else
            {
                SendMessage("GENERAL:" + txtMessage.Text);
                rtbChat.AppendText($"[Я]: {txtMessage.Text}\n");
            }

            txtMessage.Clear();
        }

        private void SendMessage(string message)
        {
            if (_stream != null && _client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                _stream.Write(data, 0, data.Length);
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            byte[] buffer = new byte[4096];
            try
            {
                while (_client.Connected)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                    Invoke(new Action(() =>
                    {
                        if (message.StartsWith("GENERAL:"))
                        {
                            rtbChat.AppendText($"{message.Substring(8)}\n");
                        }
                        else if (message.StartsWith("PRIVATE_FROM:"))
                        {
                            string data = message.Substring("PRIVATE_FROM:".Length);
                            int idx = data.IndexOf(':');
                            if (idx > 0)
                            {
                                string senderName = data.Substring(0, idx);
                                string text = data.Substring(idx + 1);
                                rtbChat.AppendText($"[Особисто від {senderName}]: {text}\n");
                            }
                        }
                        else if (message.StartsWith("USERS_LIST:"))
                        {
                            string namesData = message.Substring("USERS_LIST:".Length);
                            lstUsers.Items.Clear();
                            if (!string.IsNullOrEmpty(namesData))
                            {
                                foreach (var user in namesData.Split(','))
                                {
                                    if (!string.IsNullOrWhiteSpace(user))
                                    {
                                        lstUsers.Items.Add(user.Trim());
                                    }
                                }
                            }
                        }
                        else if (message.StartsWith("SYSTEM:"))
                        {
                            rtbChat.AppendText($"[Система]: {message.Substring(7)}\n");
                        }
                        else
                        {
                            rtbChat.AppendText($"{message}\n");
                        }
                    }));
                }
            }
            catch
            {
                Invoke(new Action(() =>
                {
                    lblStatus.Text = "Статус: Відключено";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }));
            }
        }

        // паенель адміна 

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                string user = lstUsers.SelectedItem.ToString().Replace(" (Очікує)", "").Trim();
                SendMessage($"APPROVE:{user}");
            }
            else
            {
                MessageBox.Show("Виберіть користувача зі списку!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                string user = lstUsers.SelectedItem.ToString().Replace(" (Очікує)", "").Trim();
                SendMessage($"KICK:{user}");
            }
            else
            {
                MessageBox.Show("Виберіть користувача зі списку!");
            }
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                string user = lstUsers.SelectedItem.ToString().Replace(" (Очікує)", "").Trim();
                SendMessage($"BAN:{user}");
            }
            else
            {
                MessageBox.Show("Виберіть користувача зі списку!");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_client == null || !_client.Connected)
            {
                MessageBox.Show("Спочатку натисніть 'Під'єднати'!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNickname.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Будь ласка, введіть логін та пароль!");
                return;
            }

            string nickname = txtNickname.Text.Trim();
            string password = txtPass.Text.Trim();

            SendMessage($"LOGIN:{nickname}:{password}");
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            if (_client == null || !_client.Connected)
            {
                MessageBox.Show("Спочатку натисніть 'Під'єднати'!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNickname.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Будь ласка, введіть логін та пароль для реєстрації!");
                return;
            }

            string nickname = txtNickname.Text.Trim();
            string password = txtPass.Text.Trim();

            // Відправляємо на сервер команду реєстрації
            SendMessage($"REGISTER:{nickname}:{password}");
        }

        private void btnApprove_Click_1(object sender, EventArgs e)
        {
            if (_client == null || !_client.Connected) return;

            if (lstUsers.SelectedItem != null)
            {
                string user = lstUsers.SelectedItem.ToString().Replace(" (Очікує)", "").Trim();

                SendMessage($"APPROVE:{user}");
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть користувача зі списку праворуч!");
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (_client == null || !_client.Connected) return;

            if (lstUsers.SelectedItem != null)
            {
                string user = lstUsers.SelectedItem.ToString().Replace(" (Очікує)", "").Trim();
                SendMessage($"KICK:{user}");
            }
            else
            {
                MessageBox.Show("Виберіть користувача зі списку!");
            }
        }

        private void btnBan_Click_1(object sender, EventArgs e)
        {
            if (_client == null || !_client.Connected) return;

            if (lstUsers.SelectedItem != null)
            {
                string user = lstUsers.SelectedItem.ToString().Replace(" (Очікує)", "").Trim();
                SendMessage($"BAN:{user}");
            }
            else
            {
                MessageBox.Show("Виберіть користувача зі списку!");
            }
        }
    }
}