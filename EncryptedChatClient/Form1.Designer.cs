namespace EncryptedChatClient
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            chatContainer = new FlowLayoutPanel();
            conversationCotnainer = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel5 = new Panel();
            btnSend = new Button();
            panel4 = new Panel();
            txtToSend = new TextBox();
            chatMsgs = new FlowLayoutPanel();
            panelPersonName = new Panel();
            panel6 = new Panel();
            label1 = new Label();
            txtPersonName = new Label();
            notifyIcon1 = new NotifyIcon(components);
            panel7 = new Panel();
            btnSendImage = new Button();
            panel1.SuspendLayout();
            conversationCotnainer.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panelPersonName.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(chatContainer);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(330, 713);
            panel1.TabIndex = 2;
            // 
            // chatContainer
            // 
            chatContainer.AutoScroll = true;
            chatContainer.BackColor = Color.White;
            chatContainer.Dock = DockStyle.Fill;
            chatContainer.FlowDirection = FlowDirection.TopDown;
            chatContainer.Location = new Point(0, 0);
            chatContainer.Margin = new Padding(3, 4, 3, 4);
            chatContainer.Name = "chatContainer";
            chatContainer.Size = new Size(330, 713);
            chatContainer.TabIndex = 1;
            // 
            // conversationCotnainer
            // 
            conversationCotnainer.Controls.Add(panel2);
            conversationCotnainer.Dock = DockStyle.Fill;
            conversationCotnainer.Location = new Point(330, 0);
            conversationCotnainer.Margin = new Padding(3, 4, 3, 4);
            conversationCotnainer.Name = "conversationCotnainer";
            conversationCotnainer.Size = new Size(886, 713);
            conversationCotnainer.TabIndex = 3;
            conversationCotnainer.Visible = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(chatMsgs);
            panel2.Controls.Add(panelPersonName);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(886, 713);
            panel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(240, 242, 245);
            panel3.Controls.Add(panel7);
            panel3.Controls.Add(panel5);
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 636);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(886, 77);
            panel3.TabIndex = 3;
            // 
            // panel5
            // 
            panel5.Controls.Add(btnSend);
            panel5.Location = new Point(719, 13);
            panel5.Margin = new Padding(3, 4, 3, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(56, 44);
            panel5.TabIndex = 5;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(0, 128, 105);
            btnSend.Dock = DockStyle.Fill;
            btnSend.FlatAppearance.BorderColor = Color.FromArgb(240, 242, 245);
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Image = (Image)resources.GetObject("btnSend.Image");
            btnSend.Location = new Point(0, 0);
            btnSend.Margin = new Padding(3, 14, 3, 4);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(56, 44);
            btnSend.TabIndex = 4;
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(txtToSend);
            panel4.Location = new Point(12, 12);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(701, 45);
            panel4.TabIndex = 4;
            // 
            // txtToSend
            // 
            txtToSend.BorderStyle = BorderStyle.None;
            txtToSend.Dock = DockStyle.Fill;
            txtToSend.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            txtToSend.Location = new Point(0, 0);
            txtToSend.Margin = new Padding(15, 14, 15, 14);
            txtToSend.Multiline = true;
            txtToSend.Name = "txtToSend";
            txtToSend.Size = new Size(701, 45);
            txtToSend.TabIndex = 3;
            txtToSend.KeyDown += txtToSend_KeyDown;
            // 
            // chatMsgs
            // 
            chatMsgs.AutoScroll = true;
            chatMsgs.AutoScrollMargin = new Size(0, 50);
            chatMsgs.BackColor = Color.FromArgb(239, 234, 226);
            chatMsgs.FlowDirection = FlowDirection.TopDown;
            chatMsgs.Location = new Point(0, 76);
            chatMsgs.Margin = new Padding(3, 4, 3, 4);
            chatMsgs.Name = "chatMsgs";
            chatMsgs.Size = new Size(886, 564);
            chatMsgs.TabIndex = 2;
            chatMsgs.WrapContents = false;
            // 
            // panelPersonName
            // 
            panelPersonName.BackColor = Color.FromArgb(240, 242, 245);
            panelPersonName.Controls.Add(panel6);
            panelPersonName.Controls.Add(label1);
            panelPersonName.Controls.Add(txtPersonName);
            panelPersonName.Dock = DockStyle.Top;
            panelPersonName.Location = new Point(0, 0);
            panelPersonName.Margin = new Padding(3, 4, 3, 4);
            panelPersonName.Name = "panelPersonName";
            panelPersonName.Size = new Size(886, 76);
            panelPersonName.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.BackColor = Color.LimeGreen;
            panel6.Location = new Point(21, 50);
            panel6.Name = "panel6";
            panel6.Size = new Size(10, 10);
            panel6.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.DarkGray;
            label1.Location = new Point(34, 42);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(96, 23);
            label1.TabIndex = 1;
            label1.Text = "Active Now";
            // 
            // txtPersonName
            // 
            txtPersonName.AutoSize = true;
            txtPersonName.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            txtPersonName.Location = new Point(9, 11);
            txtPersonName.Margin = new Padding(0);
            txtPersonName.Name = "txtPersonName";
            txtPersonName.Size = new Size(82, 31);
            txtPersonName.TabIndex = 0;
            txtPersonName.Text = "Person";
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipText = "New Message";
            notifyIcon1.BalloonTipTitle = "Enc Chat";
            notifyIcon1.Text = "New Message";
            notifyIcon1.Visible = true;
            // 
            // panel7
            // 
            panel7.Controls.Add(btnSendImage);
            panel7.Location = new Point(798, 13);
            panel7.Margin = new Padding(3, 4, 3, 4);
            panel7.Name = "panel7";
            panel7.Size = new Size(56, 44);
            panel7.TabIndex = 6;
            // 
            // btnSendImage
            // 
            btnSendImage.BackColor = Color.FromArgb(0, 128, 105);
            btnSendImage.Dock = DockStyle.Fill;
            btnSendImage.FlatAppearance.BorderColor = Color.FromArgb(240, 242, 245);
            btnSendImage.FlatAppearance.BorderSize = 0;
            btnSendImage.FlatStyle = FlatStyle.Flat;
            btnSendImage.Image = (Image)resources.GetObject("btnSendImage.Image");
            btnSendImage.Location = new Point(0, 0);
            btnSendImage.Margin = new Padding(3, 14, 3, 4);
            btnSendImage.Name = "btnSendImage";
            btnSendImage.Size = new Size(56, 44);
            btnSendImage.TabIndex = 4;
            btnSendImage.UseVisualStyleBackColor = false;
            btnSendImage.Click += btnSendImage_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1216, 713);
            Controls.Add(conversationCotnainer);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Enc Chat";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            conversationCotnainer.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panelPersonName.ResumeLayout(false);
            panelPersonName.PerformLayout();
            panel7.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private FlowLayoutPanel chatContainer;
        private Panel conversationCotnainer;
        private Panel panel2;
        private Panel panelPersonName;
        private Label txtPersonName;
        private FlowLayoutPanel chatMsgs;
        private Panel panel3;
        private Panel panel5;
        private Button btnSend;
        private Panel panel4;
        private TextBox txtToSend;
        private Label label1;
        private Panel panel6;
        private NotifyIcon notifyIcon1;
        private Panel panel7;
        private Button btnSendImage;
    }
}