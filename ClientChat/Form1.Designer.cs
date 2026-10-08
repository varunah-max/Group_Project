namespace ClientChat
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
            label1 = new Label();
            txtIpAddress = new TextBox();
            label2 = new Label();
            txtPort = new TextBox();
            btnConnect = new Button();
            lblStatus = new Label();
            rbGeneral = new RadioButton();
            rbPrivate = new RadioButton();
            rbGroup = new RadioButton();
            label3 = new Label();
            rtbChat = new RichTextBox();
            lstUsers = new ListBox();
            btnApprove = new Button();
            btnDelete = new Button();
            btnBan = new Button();
            label4 = new Label();
            cmbBanTime = new ComboBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            label5 = new Label();
            txtNickname = new TextBox();
            txtPass = new TextBox();
            label6 = new Label();
            label7 = new Label();
            btnLogin = new Button();
            btnRegistration = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 29);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 0;
            label1.Text = "IP-Адреса:";
            // 
            // txtIpAddress
            // 
            txtIpAddress.Location = new Point(113, 26);
            txtIpAddress.Name = "txtIpAddress";
            txtIpAddress.Size = new Size(125, 27);
            txtIpAddress.TabIndex = 1;
            txtIpAddress.Text = "127.0.0.1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(278, 31);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 2;
            label2.Text = "Port:";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(322, 26);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(125, 27);
            txtPort.TabIndex = 3;
            txtPort.Text = "5000";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(472, 24);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(94, 29);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "Під'єднатися";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click_1;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(601, 29);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(164, 20);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Статус: Не підключено";
            // 
            // rbGeneral
            // 
            rbGeneral.AutoSize = true;
            rbGeneral.Checked = true;
            rbGeneral.Location = new Point(113, 78);
            rbGeneral.Name = "rbGeneral";
            rbGeneral.Size = new Size(129, 24);
            rbGeneral.TabIndex = 7;
            rbGeneral.TabStop = true;
            rbGeneral.Text = "Загальний чат";
            rbGeneral.UseVisualStyleBackColor = true;
            // 
            // rbPrivate
            // 
            rbPrivate.AutoSize = true;
            rbPrivate.Location = new Point(248, 78);
            rbPrivate.Name = "rbPrivate";
            rbPrivate.Size = new Size(131, 24);
            rbPrivate.TabIndex = 8;
            rbPrivate.TabStop = true;
            rbPrivate.Text = "Один-на-один";
            rbPrivate.UseVisualStyleBackColor = true;
            // 
            // rbGroup
            // 
            rbGroup.AutoSize = true;
            rbGroup.Location = new Point(385, 78);
            rbGroup.Name = "rbGroup";
            rbGroup.Size = new Size(157, 24);
            rbGroup.TabIndex = 9;
            rbGroup.TabStop = true;
            rbGroup.Text = "Один-до-багатьох";
            rbGroup.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 78);
            label3.Name = "label3";
            label3.Size = new Size(59, 20);
            label3.TabIndex = 10;
            label3.Text = "Режим:";
            // 
            // rtbChat
            // 
            rtbChat.Location = new Point(27, 119);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(570, 254);
            rtbChat.TabIndex = 11;
            rtbChat.Text = "";
            // 
            // lstUsers
            // 
            lstUsers.FormattingEnabled = true;
            lstUsers.Location = new Point(615, 119);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(150, 104);
            lstUsers.TabIndex = 12;
            // 
            // btnApprove
            // 
            btnApprove.Location = new Point(615, 274);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(94, 29);
            btnApprove.TabIndex = 13;
            btnApprove.Text = "Схвалити";
            btnApprove.UseVisualStyleBackColor = true;
            btnApprove.Click += btnApprove_Click_1;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(615, 309);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Видалити";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click_1;
            // 
            // btnBan
            // 
            btnBan.Location = new Point(615, 344);
            btnBan.Name = "btnBan";
            btnBan.Size = new Size(94, 29);
            btnBan.TabIndex = 15;
            btnBan.Text = "Забанити";
            btnBan.UseVisualStyleBackColor = true;
            btnBan.Click += btnBan_Click_1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(614, 237);
            label4.Name = "label4";
            label4.Size = new Size(173, 20);
            label4.TabIndex = 16;
            label4.Text = "Панель Адміністратора";
            // 
            // cmbBanTime
            // 
            cmbBanTime.FormattingEnabled = true;
            cmbBanTime.Items.AddRange(new object[] { "10 хв", "1 год" });
            cmbBanTime.Location = new Point(615, 379);
            cmbBanTime.Name = "cmbBanTime";
            cmbBanTime.Size = new Size(151, 28);
            cmbBanTime.TabIndex = 17;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(27, 380);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(444, 58);
            txtMessage.TabIndex = 18;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(498, 380);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(99, 42);
            btnSend.TabIndex = 19;
            btnSend.Text = "Надіслати";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(865, 82);
            label5.Name = "label5";
            label5.Size = new Size(144, 20);
            label5.TabIndex = 20;
            label5.Text = "Логин/Регестрация";
            // 
            // txtNickname
            // 
            txtNickname.Location = new Point(865, 119);
            txtNickname.Name = "txtNickname";
            txtNickname.Size = new Size(144, 27);
            txtNickname.TabIndex = 21;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(865, 180);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(144, 27);
            txtPass.TabIndex = 22;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(807, 119);
            label6.Name = "label6";
            label6.Size = new Size(52, 20);
            label6.TabIndex = 23;
            label6.Text = "Логин";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(797, 187);
            label7.Name = "label7";
            label7.Size = new Size(62, 20);
            label7.TabIndex = 24;
            label7.Text = "Пароль";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(807, 228);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 29);
            btnLogin.TabIndex = 25;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegistration
            // 
            btnRegistration.Location = new Point(915, 228);
            btnRegistration.Name = "btnRegistration";
            btnRegistration.Size = new Size(106, 29);
            btnRegistration.TabIndex = 27;
            btnRegistration.Text = "Registration";
            btnRegistration.UseVisualStyleBackColor = true;
            btnRegistration.Click += btnRegistration_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1074, 450);
            Controls.Add(btnRegistration);
            Controls.Add(btnLogin);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(txtPass);
            Controls.Add(txtNickname);
            Controls.Add(label5);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(cmbBanTime);
            Controls.Add(label4);
            Controls.Add(btnBan);
            Controls.Add(btnDelete);
            Controls.Add(btnApprove);
            Controls.Add(lstUsers);
            Controls.Add(rtbChat);
            Controls.Add(label3);
            Controls.Add(rbGroup);
            Controls.Add(rbPrivate);
            Controls.Add(rbGeneral);
            Controls.Add(lblStatus);
            Controls.Add(btnConnect);
            Controls.Add(txtPort);
            Controls.Add(label2);
            Controls.Add(txtIpAddress);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Месенджер ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtIpAddress;
        private Label label2;
        private TextBox txtPort;
        private Button btnConnect;
        private Label lblStatus;
        private RadioButton rbGeneral;
        private RadioButton rbPrivate;
        private RadioButton rbGroup;
        private Label label3;
        private RichTextBox rtbChat;
        private ListBox lstUsers;
        private Button btnApprove;
        private Button btnDelete;
        private Button btnBan;
        private Label label4;
        private ComboBox cmbBanTime;
        private TextBox txtMessage;
        private Button btnSend;
        private Label label5;
        private TextBox txtNickname;
        private TextBox txtPass;
        private Label label6;
        private Label label7;
        private Button btnLogin;
        private Button btnRegistration;
    }
}
