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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            chatContainer = new FlowLayoutPanel();
            conversationCotnainer = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            txtToSend = new TextBox();
            btnSend = new Button();
            chatMsgs = new FlowLayoutPanel();
            panelPersonName = new Panel();
            txtPersonName = new Label();
            panel1.SuspendLayout();
            conversationCotnainer.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panelPersonName.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(chatContainer);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(295, 588);
            panel1.TabIndex = 2;
            // 
            // chatContainer
            // 
            chatContainer.AutoScroll = true;
            chatContainer.BackColor = Color.FromArgb(240, 242, 245);
            chatContainer.Dock = DockStyle.Fill;
            chatContainer.FlowDirection = FlowDirection.TopDown;
            chatContainer.Location = new Point(0, 0);
            chatContainer.Name = "chatContainer";
            chatContainer.Size = new Size(295, 588);
            chatContainer.TabIndex = 1;
            // 
            // conversationCotnainer
            // 
            conversationCotnainer.Controls.Add(panel2);
            conversationCotnainer.Dock = DockStyle.Fill;
            conversationCotnainer.Location = new Point(295, 0);
            conversationCotnainer.Name = "conversationCotnainer";
            conversationCotnainer.Size = new Size(841, 588);
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
            panel2.Name = "panel2";
            panel2.Size = new Size(841, 588);
            panel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(240, 242, 245);
            panel3.Controls.Add(txtToSend);
            panel3.Controls.Add(btnSend);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 510);
            panel3.Name = "panel3";
            panel3.Size = new Size(841, 78);
            panel3.TabIndex = 3;
            // 
            // txtToSend
            // 
            txtToSend.BorderStyle = BorderStyle.None;
            txtToSend.Location = new Point(30, 24);
            txtToSend.Margin = new Padding(16, 12, 16, 12);
            txtToSend.Name = "txtToSend";
            txtToSend.Size = new Size(711, 31);
            txtToSend.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.FlatAppearance.BorderColor = Color.FromArgb(240, 242, 245);
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Image = (Image)resources.GetObject("btnSend.Image");
            btnSend.Location = new Point(760, 24);
            btnSend.Margin = new Padding(3, 12, 3, 3);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(64, 31);
            btnSend.TabIndex = 3;
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // chatMsgs
            // 
            chatMsgs.AutoScroll = true;
            chatMsgs.AutoSize = true;
            chatMsgs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            chatMsgs.BackColor = Color.White;
            chatMsgs.Dock = DockStyle.Fill;
            chatMsgs.FlowDirection = FlowDirection.TopDown;
            chatMsgs.Location = new Point(0, 45);
            chatMsgs.Name = "chatMsgs";
            chatMsgs.Size = new Size(841, 543);
            chatMsgs.TabIndex = 2;
            chatMsgs.WrapContents = false;
            // 
            // panelPersonName
            // 
            panelPersonName.BackColor = Color.FromArgb(240, 242, 245);
            panelPersonName.Controls.Add(txtPersonName);
            panelPersonName.Dock = DockStyle.Top;
            panelPersonName.Location = new Point(0, 0);
            panelPersonName.Name = "panelPersonName";
            panelPersonName.Size = new Size(841, 45);
            panelPersonName.TabIndex = 1;
            // 
            // txtPersonName
            // 
            txtPersonName.AutoSize = true;
            txtPersonName.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtPersonName.Location = new Point(10, 9);
            txtPersonName.Margin = new Padding(0);
            txtPersonName.Name = "txtPersonName";
            txtPersonName.Size = new Size(90, 29);
            txtPersonName.TabIndex = 0;
            txtPersonName.Text = "Person";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(16F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1136, 588);
            Controls.Add(conversationCotnainer);
            Controls.Add(panel1);
            Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Enc Chat";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            conversationCotnainer.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panelPersonName.ResumeLayout(false);
            panelPersonName.PerformLayout();
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
        private TextBox txtToSend;
        private Button btnSend;
    }
}