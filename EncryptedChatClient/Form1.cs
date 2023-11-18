using EcnryptedChatClient.Services;
using EncChatCommonLib.Models;
using EncChatCommonLib.Services;
using EncryptedChatClient.Models;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
        private static string ConnectionId { 
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

        public Form1()
        {
            InitializeComponent();
        }

        private Task Connect()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            return _clientSocket.ConnectAsync(new IPEndPoint(new IPAddress(Constants.SERVER_IPV4), Constants.SERVER_PORT));
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
            }
            catch { }

            isRunning = false;
            _clientSocket.Close();
            base.OnFormClosing(e);
        }

        private void CreateClientPanel(ClientModel client)
        {
            var panel = new FlowLayoutPanel();
            panel.FlowDirection = FlowDirection.TopDown;
            panel.Height = 75;
            panel.Width = chatContainer.Width;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Cursor = Cursors.Hand;
            var label = new Label();
            label.Click += (object sender, EventArgs e) =>
            {
                ChatHead_Click(sender, e, client);
            };
            label.Width = chatContainer.Width;
            label.Height = 35;
            var label2 = new Label();
            label2.Click += (object sender, EventArgs e) =>
            {
                ChatHead_Click(sender, e, client);
            };
            label2.Width = chatContainer.Width;
            label2.Height = 35;
            label2.Font = new Font(this.Font.FontFamily, 14);
            label2.ForeColor = Color.DarkGray;
            label2.Text = "Click here to start chatting";
            if (client.ConnectionId == Form1.connectionId)
            {
                label.Text = "Me";
            }
            else
            {
                label.Text = client.ConnectionId;
            }
            panel.Controls.Add(label);
            panel.Controls.Add(label2);
            chatContainer.Controls.Add(panel);
        }

        private void AddMessageToChat(string msg, bool isMe, string otherConnectionId = null)
        {
            var msgPanel = new FlowLayoutPanel();
            msgPanel.Width = chatMsgs.Width;
            var msgLabel = new Label();
            msgLabel.Text = isMe ? $"You: {msg}" : $"{otherConnectionId}: {msg}";
            msgLabel.AutoSize = true;
            msgLabel.Margin = new Padding(15);
            msgLabel.BorderStyle = BorderStyle.FixedSingle;
            msgPanel.RightToLeft = isMe ? RightToLeft.No : RightToLeft.Yes;
            msgPanel.Controls.Add(msgLabel);
            chatMsgs.Controls.Add(msgPanel);
            chatMsgs.ScrollControlIntoView(msgPanel);
        }

        private void ChatHead_Click(object sender, EventArgs e, ClientModel client)
        {
            currentActiveConnectionId = client.ConnectionId;

            if (client.ConnectionId == connectionId)
                txtPersonName.Text = "Me";
            else
                txtPersonName.Text = client.ConnectionId;

            conversationCotnainer.Visible = true;
            chatMsgs.Controls.Clear();
        }

        private async void btnSend_Click(object sender, EventArgs e)
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

        public void ReceiveMessageFromClient(string senderConnectionId, string message)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                AddMessageToChat(message, false, senderConnectionId);
            };
            Invoke(methodInvokerDelegate);
        }
        #endregion
    }
}