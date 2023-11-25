using EcnryptedChatClient.Services;
using EncChatCommonLib.Models;
using EncChatCommonLib.Services;
using EncryptedChatClient.Models;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace EncryptedChatClient
{
    public partial class Form1 : Form
    {
        private static Socket _clientSocket;
        private static readonly EncryptionService _encryptionService = new EncryptionService();
        private static readonly ClientMessageInterpreter _messageInterpreter = new ClientMessageInterpreter(_encryptionService);
        private bool isRunning = false;
        private static string currentActiveConnectionId = "";
        private static byte[] encryptionKey;
        private static string connectionId;
        private string email;
        private static string ConnectionId
        {
            get
            {
                return connectionId;
            }
            set
            {
                if (connectionId == null)
                {
                    connectionId = value;
                }
            }
        }
        private static List<ClientModel> connectedClients = new List<ClientModel>();

        public Form1(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private Task Connect()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            return _clientSocket.ConnectAsync(new IPEndPoint(new IPAddress(SharedConstants.SERVER_IPV4), SharedConstants.SERVER_PORT));
        }

        #region Util Functions
        private void ReceiveAndSendInBackground()
        {
            Task.Run(() =>
            {
                while (isRunning)
                {
                    if (_clientSocket.Available > 0)
                    {
                        byte[] buffer = new byte[Constants.MAX_BUFFER_LEN];
                        int receivedAmount = _clientSocket.Receive(buffer);
                        byte[] alignedBuffer = new byte[receivedAmount];
                        Array.Copy(buffer, alignedBuffer, receivedAmount);
                        _messageInterpreter.InterpretAndDecrypt("EncryptedChatClient", alignedBuffer, encryptionKey);
                    }
                }
            });
        }

        private async Task SendData(byte[] data)
        {
            await _clientSocket.SendAsync(new ArraySegment<byte>(data));
        }

        private async Task SendMsg()
        {
            if (string.IsNullOrWhiteSpace(txtToSend.Text))
                return;

            var data = _encryptionService.EncryptSymmetericlly(
                 JsonConvert.SerializeObject(new MessageObject
                 {
                     ClassName = "Program",
                     MethodName = "ClientSentMessage",
                     MethodParams =
                     {
                        new Parameter
                        {
                            ParamName = "senderConnectionId",
                            ParamValue = connectionId,
                        },
                        new Parameter
                        {
                            ParamName = "message",
                            ParamValue = txtToSend.Text,
                        },
                        new Parameter
                        {
                            ParamName = "receiverConnectionId",
                            ParamValue = currentActiveConnectionId
                        }
                     }
                 })
                 , Form1.encryptionKey);
            btnSend.Enabled = false;
            await SendData(data);
            btnSend.Enabled = true;
            AddMessageToChat(txtToSend.Text, true);
            txtToSend.Text = "";
        }
        #endregion

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
            }
            base.OnSizeChanged(e);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await Connect();
                isRunning = true;
                ReceiveAndSendInBackground();
            }
            catch (Exception)
            {
                // TODO: implement some reconnnection mechanism
            }
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                var encryptedData = _encryptionService.EncryptSymmetericlly(JsonConvert.SerializeObject(
                    new MessageObject
                    {
                        ClassName = "Program",
                        MethodName = "ClientDisconnected",
                        MethodParams =
                        {
                            new Parameter
                            {
                                ParamName = "connectionId",
                                ParamValue = connectionId
                            }
                        }
                    }
                ), encryptionKey);
                await SendData(encryptedData);

                isRunning = false;
                _clientSocket.Close();
            }
            catch { }

            if (Application.OpenForms.Count > 0)
            {
                Application.OpenForms[0].Show();
            }

            base.OnFormClosing(e);
        }

        private void CreateClientPanel(ClientModel client)
        {
            var panel = new FlowLayoutPanel();
            panel.Tag = client.ConnectionId;
            panel.Height = 75;
            panel.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            panel.Width = chatContainer.Width - 10;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Cursor = Cursors.Hand;
            var label = new Label();
            label.ForeColor = Color.Black;
            label.Click += (object sender, EventArgs e) =>
            {
                ChatHead_Click(sender, e, client);
            };
            label.Width = chatContainer.Width;
            label.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label.Height = 35;
            var label2 = new Label();
            label2.ForeColor = Color.Black;
            label2.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            label2.Click += (object sender, EventArgs e) =>
            {
                ChatHead_Click(sender, e, client);
            };
            label2.Width = chatContainer.Width;
            label2.Height = 35;
            label2.ForeColor = Color.DarkGray;
            label2.Text = "Click here to start chatting";
            if (client.ConnectionId == Form1.connectionId)
            {
                label.Text = "Me";
            }
            else
            {
                label.Text = client.Name;
            }
            panel.Controls.Add(label);
            panel.Controls.Add(label2);
            chatContainer.Controls.Add(panel);
        }

        private void DeleteClientPanel(string connectionId)
        {
            foreach (Control control in chatContainer.Controls)
            {
                if (control.Tag.ToString() == connectionId)
                {
                    chatContainer.Controls.Remove(control);
                }
            }
        }

        private void AddMessageToChat(string msg, bool isMe, string otherConnectionId = null)
        {
            var msgPanel = new FlowLayoutPanel();
            msgPanel.Width = chatMsgs.Width;
            msgPanel.BackColor = isMe ? ColorTranslator.FromHtml("#d9fdd3") : Color.White;
            msgPanel.AutoSize = true;
            var msgLabel = new Label();
            msgLabel.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            if (string.IsNullOrWhiteSpace(otherConnectionId))
            {
                msgLabel.Text = isMe ? $"[You]\n{msg}" : $"[User{otherConnectionId.Substring(0, 4)}]\n {msg}";
            }
            else
            {
                msgLabel.Text = isMe ? $"[You]\n{msg}" : $"[{otherConnectionId}]\n {msg}";
            }
            msgLabel.AutoSize = true;
            msgLabel.Margin = new Padding(15);
            msgPanel.Controls.Add(msgLabel);
            chatMsgs.Controls.Add(msgPanel);
            chatMsgs.ScrollControlIntoView(msgLabel);
        }

        private void AddImageToChat(string image, bool isMe, string otherConnectionId = null)
        {
            byte[] rawImage = Convert.FromBase64String(image);
            var msgPanel = new FlowLayoutPanel();
            msgPanel.Width = chatMsgs.Width;
            msgPanel.BackColor = isMe ? ColorTranslator.FromHtml("#d9fdd3") : Color.White;
            msgPanel.AutoSize = true;
            var label = new Label();
            label.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            if (string.IsNullOrWhiteSpace(otherConnectionId))
            {
                label.Text = isMe ? $"[You]" : $"[User{otherConnectionId.Substring(0, 4)}]";
            }
            else
            {
                label.Text = isMe ? $"[You]" : $"[{otherConnectionId}]";
            }

            var pic = new PictureBox();
            pic.Image = Image.FromStream(new MemoryStream(rawImage));
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Margin = new Padding(15);
            msgPanel.Controls.Add(label);
            msgPanel.Controls.Add(pic);
            chatMsgs.Controls.Add(msgPanel);
            chatMsgs.ScrollControlIntoView(pic);
        }

        private void ChatHead_Click(object sender, EventArgs e, ClientModel client)
        {
            currentActiveConnectionId = client.ConnectionId;

            if (client.ConnectionId == connectionId)
                txtPersonName.Text = "Me";
            else
                txtPersonName.Text = client.Name;

            conversationCotnainer.Visible = true;
            chatMsgs.Controls.Clear();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMsg();
        }

        #region Actions
        public async void ReceiveServerPublicKey(string connectionId, string encryptionKey)
        {
            Form1.encryptionKey = Encoding.ASCII.GetBytes(_encryptionService.Decrypt(Convert.FromBase64String(encryptionKey), Constants.PRIVATE_KEY_XML));
            Form1.ConnectionId = connectionId;

            // getting connected clients
            var data = _encryptionService.EncryptSymmetericlly(
                JsonConvert.SerializeObject(new MessageObject
                {
                    ClassName = "Program",
                    MethodName = "GetConnectedClients",
                    MethodParams =
                    {
                        new Parameter
                        {
                            ParamName = "clientConnectionId",
                            ParamValue = connectionId,
                        },
                        new Parameter
                        {
                            ParamName = "email",
                            ParamValue = email
                        }
                    }
                })
                , Form1.encryptionKey);
            await SendData(data);
        }

        public void ReceiveConnectedClients(string clients)
        {
            connectedClients = JsonConvert.DeserializeObject<List<ClientModel>>(clients);

            foreach (var client in connectedClients)
            {
                MethodInvoker methodInvokerDelegate = delegate ()
                {
                    CreateClientPanel(client);
                };
                Invoke(methodInvokerDelegate);
            }
        }

        public void ReceiveMessageFromClient(string senderConnectionId, string name, string message)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                if (senderConnectionId != ConnectionId && currentActiveConnectionId == senderConnectionId)
                {
                    AddMessageToChat(message, false, name);
                    notifyIcon1.Text = $"{name} Sent You a Message";
                    notifyIcon1.Icon = this.Icon;
                    notifyIcon1.ShowBalloonTip(3000, "Enc Chat", $"{name} Sent You a Message", ToolTipIcon.None);
                }
            };
            Invoke(methodInvokerDelegate);
        }

        public void ReceiveClientConnectedMsg(string connectedClientConnectionId, string clientName)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                CreateClientPanel(new ClientModel { ConnectionId = connectedClientConnectionId, Name = clientName });
            };
            Invoke(methodInvokerDelegate);
        }

        public void ReceiveImageFromClient(string senderConnectionId, string name, string image)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                if (senderConnectionId != ConnectionId && currentActiveConnectionId == senderConnectionId)
                {
                    AddImageToChat(image, false, name);
                    notifyIcon1.Text = $"{name} Sent You a Message";
                    notifyIcon1.Icon = this.Icon;
                    notifyIcon1.ShowBalloonTip(3000, "Enc Chat", $"{name} Sent You a Message", ToolTipIcon.None);
                }
            };
            Invoke(methodInvokerDelegate);
        }

        public void ReceiveClientDisconnectedMsg(string disconnectedClientConnectionId)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                DeleteClientPanel(disconnectedClientConnectionId);
            };
            Invoke(methodInvokerDelegate);
        }
        #endregion

        private async void txtToSend_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    await SendMsg();
                    break;
            }
        }

        private async void btnSendImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                var result = ofd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string fileName = ofd.FileName;
                    FileInfo fileInfo = new FileInfo(fileName);
                    if (fileInfo.Exists && fileInfo.Length <= 4096 && fileInfo.Extension == ".png")
                    {
                        // max file size 4 kb
                        string base64 = Convert.ToBase64String(File.ReadAllBytes(fileName));

                        var data = _encryptionService.EncryptSymmetericlly(
                             JsonConvert.SerializeObject(new MessageObject
                             {
                                 ClassName = "Program",
                                 MethodName = "ClientSentImage",
                                 MethodParams =
                                 {
                                    new Parameter
                                    {
                                        ParamName = "senderConnectionId",
                                        ParamValue = connectionId,
                                    },
                                    new Parameter
                                    {
                                        ParamName = "image",
                                        ParamValue = base64,
                                    },
                                    new Parameter
                                    {
                                        ParamName = "receiverConnectionId",
                                        ParamValue = currentActiveConnectionId
                                    }
                                 }
                             })
                 , Form1.encryptionKey);
                        btnSendImage.Enabled = false;
                        await SendData(data);
                        AddImageToChat(base64, true); ;
                        btnSendImage.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid image format");
                    }
                }
            }
        }
    }
}