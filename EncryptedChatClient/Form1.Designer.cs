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
            panel1 = new Panel();
            chatContainer = new FlowLayoutPanel();
            leftPanelHeader = new FlowLayoutPanel();
            pictureBox1 = new PictureBox();
            conversationCotnainer = new Panel();
            panel2 = new Panel();
            chatMsgs = new FlowLayoutPanel();
            panelPersonName = new Panel();
            txtPersonName = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            txtToSend = new TextBox();
            btnSend = new Button();
            panel1.SuspendLayout();
            leftPanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            conversationCotnainer.SuspendLayout();
            panel2.SuspendLayout();
            panelPersonName.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(chatContainer);
            panel1.Controls.Add(leftPanelHeader);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(340, 543);
            panel1.TabIndex = 2;
            // 
            // chatContainer
            // 
            chatContainer.AutoScroll = true;
            chatContainer.BorderStyle = BorderStyle.FixedSingle;
            chatContainer.Dock = DockStyle.Fill;
            chatContainer.FlowDirection = FlowDirection.TopDown;
            chatContainer.Location = new Point(0, 78);
            chatContainer.Name = "chatContainer";
            chatContainer.Size = new Size(340, 465);
            chatContainer.TabIndex = 1;
            // 
            // leftPanelHeader
            // 
            leftPanelHeader.BorderStyle = BorderStyle.FixedSingle;
            leftPanelHeader.Controls.Add(pictureBox1);
            leftPanelHeader.Dock = DockStyle.Top;
            leftPanelHeader.Location = new Point(0, 0);
            leftPanelHeader.Name = "leftPanelHeader";
            leftPanelHeader.Size = new Size(340, 78);
            leftPanelHeader.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo_no_background;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(93, 77);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // conversationCotnainer
            // 
            conversationCotnainer.Controls.Add(panel2);
            conversationCotnainer.Dock = DockStyle.Fill;
            conversationCotnainer.Location = new Point(340, 0);
            conversationCotnainer.Name = "conversationCotnainer";
            conversationCotnainer.Size = new Size(801, 543);
            conversationCotnainer.TabIndex = 3;
            conversationCotnainer.Visible = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(chatMsgs);
            panel2.Controls.Add(panelPersonName);
            panel2.Controls.Add(flowLayoutPanel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(801, 543);
            panel2.TabIndex = 0;
            // 
            // chatMsgs
            // 
            chatMsgs.AutoScroll = true;
            chatMsgs.AutoSize = true;
            chatMsgs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            chatMsgs.Dock = DockStyle.Fill;
            chatMsgs.FlowDirection = FlowDirection.TopDown;
            chatMsgs.Location = new Point(0, 78);
            chatMsgs.Name = "chatMsgs";
            chatMsgs.Size = new Size(801, 370);
            chatMsgs.TabIndex = 2;
            chatMsgs.WrapContents = false;
            // 
            // panelPersonName
            // 
            panelPersonName.BorderStyle = BorderStyle.FixedSingle;
            panelPersonName.Controls.Add(txtPersonName);
            panelPersonName.Dock = DockStyle.Top;
            panelPersonName.Location = new Point(0, 0);
            panelPersonName.Name = "panelPersonName";
            panelPersonName.Size = new Size(801, 78);
            panelPersonName.TabIndex = 1;
            // 
            // txtPersonName
            // 
            txtPersonName.AutoSize = true;
            txtPersonName.Location = new Point(15, 21);
            txtPersonName.Name = "txtPersonName";
            txtPersonName.Size = new Size(96, 37);
            txtPersonName.TabIndex = 0;
            txtPersonName.Text = "Person";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Controls.Add(txtToSend);
            flowLayoutPanel1.Controls.Add(btnSend);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.Location = new Point(0, 448);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(801, 95);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // txtToSend
            // 
            txtToSend.Location = new Point(15, 15);
            txtToSend.Margin = new Padding(15);
            txtToSend.Name = "txtToSend";
            txtToSend.Size = new Size(667, 43);
            txtToSend.TabIndex = 0;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(700, 15);
            btnSend.Margin = new Padding(3, 15, 3, 3);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(89, 52);
            btnSend.TabIndex = 1;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1141, 543);
            Controls.Add(conversationCotnainer);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Enc Chat";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            leftPanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            conversationCotnainer.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panelPersonName.ResumeLayout(false);
            panelPersonName.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private FlowLayoutPanel chatContainer;
        private FlowLayoutPanel leftPanelHeader;
        private PictureBox pictureBox1;
        private Panel conversationCotnainer;
        private Panel panel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private TextBox txtToSend;
        private Button btnSend;
        private Panel panelPersonName;
        private Label txtPersonName;
        private FlowLayoutPanel chatMsgs;
    }
}