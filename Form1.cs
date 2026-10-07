namespace Group_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DDF_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void txtIpAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIpAddress.Text) || string.IsNullOrWhiteSpace(txtPort.Text))
            {
                MessageBox.Show("Please enter IP address and port!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblStatus.Text = "Status: Connected";
            lblStatus.ForeColor = System.Drawing.Color.Green;

            lstUsers.Items.Clear();
            lstUsers.Items.Add("User1");
            lstUsers.Items.Add("User2");
            lstUsers.Items.Add("User3");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                return;
            }

            string mode = "General";
            if (rbChatPrivate.Checked) mode = "Private";
            if (rbChatGroup.Checked) mode = "Group";

            rtbChat.AppendText($"[Me] [{mode}]: {txtMessage.Text}\n");
            txtMessage.Clear();
        }

        private void btnAdminApprove_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem == null) return;
            rtbChat.AppendText($"[System]: User {lstUsers.SelectedItem} approved.\n");
        }

        private void btnAdminDelete_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem == null) return;
            rtbChat.AppendText($"[System]: User {lstUsers.SelectedItem} deleted.\n");
            lstUsers.Items.Remove(lstUsers.SelectedItem);
        }

        private void btnAdminBan_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem == null || cmbBanTime.SelectedItem == null) return;
            rtbChat.AppendText($"[System]: User {lstUsers.SelectedItem} banned for {cmbBanTime.SelectedItem}.\n");
            lstUsers.Items.Remove(lstUsers.SelectedItem);
        }
    }
}
