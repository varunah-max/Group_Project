namespace Group_Project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtIpAddress = new TextBox();
            label1 = new Label();
            txtPort = new TextBox();
            label2 = new Label();
            btnConnect = new Button();
            lblStatus = new Label();
            rtbChat = new RichTextBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            rbChatPrivate = new RadioButton();
            rbChatGeneral = new RadioButton();
            rbChatGroup = new RadioButton();
            lstUsers = new ListBox();
            grpAdminPanel = new GroupBox();
            cmbBanTime = new ComboBox();
            btnAdminBan = new Button();
            btnAdminDelete = new Button();
            btnAdminApprove = new Button();
            label3 = new Label();
            grpAdminPanel.SuspendLayout();
            SuspendLayout();
            // 
            // txtIpAddress
            // 
            txtIpAddress.Location = new Point(84, 32);
            txtIpAddress.Name = "txtIpAddress";
            txtIpAddress.Size = new Size(168, 27);
            txtIpAddress.TabIndex = 0;
            txtIpAddress.TextChanged += txtIpAddress_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 35);
            label1.Name = "label1";
            label1.Size = new Size(76, 20);
            label1.TabIndex = 1;
            label1.Text = "IP Adress: ";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(337, 32);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(107, 27);
            txtPort.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(293, 35);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 3;
            label2.Text = "Port:";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(484, 30);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(94, 29);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(602, 34);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(154, 20);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Status: Not connected";
            lblStatus.Click += lblStatus_Click;
            // 
            // rtbChat
            // 
            rtbChat.Location = new Point(12, 120);
            rtbChat.Name = "rtbChat";
            rtbChat.Size = new Size(566, 230);
            rtbChat.TabIndex = 7;
            rtbChat.Text = "";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(13, 368);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(452, 77);
            txtMessage.TabIndex = 8;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(484, 368);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 77);
            btnSend.TabIndex = 9;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // rbChatPrivate
            // 
            rbChatPrivate.AutoSize = true;
            rbChatPrivate.Location = new Point(13, 90);
            rbChatPrivate.Name = "rbChatPrivate";
            rbChatPrivate.Size = new Size(113, 24);
            rbChatPrivate.TabIndex = 10;
            rbChatPrivate.TabStop = true;
            rbChatPrivate.Text = "Private 1 - 1 ";
            rbChatPrivate.UseVisualStyleBackColor = true;
            // 
            // rbChatGeneral
            // 
            rbChatGeneral.AutoSize = true;
            rbChatGeneral.Location = new Point(132, 90);
            rbChatGeneral.Name = "rbChatGeneral";
            rbChatGeneral.Size = new Size(115, 24);
            rbChatGeneral.TabIndex = 11;
            rbChatGeneral.TabStop = true;
            rbChatGeneral.Text = "General Chat";
            rbChatGeneral.UseVisualStyleBackColor = true;
            // 
            // rbChatGroup
            // 
            rbChatGroup.AutoSize = true;
            rbChatGroup.Location = new Point(268, 90);
            rbChatGroup.Name = "rbChatGroup";
            rbChatGroup.Size = new Size(133, 24);
            rbChatGroup.TabIndex = 12;
            rbChatGroup.TabStop = true;
            rbChatGroup.Text = "Group 1 - Many";
            rbChatGroup.UseVisualStyleBackColor = true;
            // 
            // lstUsers
            // 
            lstUsers.FormattingEnabled = true;
            lstUsers.Location = new Point(602, 120);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(150, 104);
            lstUsers.TabIndex = 13;
            // 
            // grpAdminPanel
            // 
            grpAdminPanel.Controls.Add(cmbBanTime);
            grpAdminPanel.Controls.Add(btnAdminBan);
            grpAdminPanel.Controls.Add(btnAdminDelete);
            grpAdminPanel.Controls.Add(btnAdminApprove);
            grpAdminPanel.Location = new Point(601, 255);
            grpAdminPanel.Name = "grpAdminPanel";
            grpAdminPanel.Size = new Size(152, 190);
            grpAdminPanel.TabIndex = 14;
            grpAdminPanel.TabStop = false;
            grpAdminPanel.Text = "Admin Panel";
            // 
            // cmbBanTime
            // 
            cmbBanTime.FormattingEnabled = true;
            cmbBanTime.Items.AddRange(new object[] { "10 minutes", "1 hour", "Forever" });
            cmbBanTime.Location = new Point(6, 156);
            cmbBanTime.Name = "cmbBanTime";
            cmbBanTime.Size = new Size(121, 28);
            cmbBanTime.TabIndex = 3;
            // 
            // btnAdminBan
            // 
            btnAdminBan.Location = new Point(6, 112);
            btnAdminBan.Name = "btnAdminBan";
            btnAdminBan.Size = new Size(94, 29);
            btnAdminBan.TabIndex = 2;
            btnAdminBan.Text = "Ban";
            btnAdminBan.UseVisualStyleBackColor = true;
            btnAdminBan.Click += btnAdminBan_Click;
            // 
            // btnAdminDelete
            // 
            btnAdminDelete.Location = new Point(6, 77);
            btnAdminDelete.Name = "btnAdminDelete";
            btnAdminDelete.Size = new Size(94, 29);
            btnAdminDelete.TabIndex = 1;
            btnAdminDelete.Text = "Delete";
            btnAdminDelete.UseVisualStyleBackColor = true;
            btnAdminDelete.Click += btnAdminDelete_Click;
            // 
            // btnAdminApprove
            // 
            btnAdminApprove.Location = new Point(6, 35);
            btnAdminApprove.Name = "btnAdminApprove";
            btnAdminApprove.Size = new Size(94, 29);
            btnAdminApprove.TabIndex = 0;
            btnAdminApprove.Text = "Approve";
            btnAdminApprove.UseVisualStyleBackColor = true;
            btnAdminApprove.Click += btnAdminApprove_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(600, 89);
            label3.Name = "label3";
            label3.Size = new Size(90, 20);
            label3.TabIndex = 15;
            label3.Text = "Urers Online";
            // 
            // Form1
            // 
            ClientSize = new Size(777, 457);
            Controls.Add(label3);
            Controls.Add(grpAdminPanel);
            Controls.Add(lstUsers);
            Controls.Add(rbChatGroup);
            Controls.Add(rbChatGeneral);
            Controls.Add(rbChatPrivate);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(rtbChat);
            Controls.Add(lblStatus);
            Controls.Add(btnConnect);
            Controls.Add(label2);
            Controls.Add(txtPort);
            Controls.Add(label1);
            Controls.Add(txtIpAddress);
            Name = "Form1";
            grpAdminPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private TextBox txtIpAddress;
        private Label label1;
        private TextBox txtPort;
        private Label label2;
        private Button btnConnect;
        private Label lblStatus;
        private RichTextBox rtbChat;
        private TextBox txtMessage;
        private Button btnSend;
        private RadioButton rbChatPrivate;
        private RadioButton rbChatGeneral;
        private RadioButton rbChatGroup;
        private ListBox lstUsers;
        private GroupBox grpAdminPanel;
        private Button btnAdminApprove;
        private Label label3;
        private Button btnAdminBan;
        private Button btnAdminDelete;
        private ComboBox cmbBanTime;
    }
}
