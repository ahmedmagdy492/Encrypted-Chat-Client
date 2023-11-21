namespace Enc_Chat_Server
{
    using Enc_Chat_Server.Models;
    using Enc_Chat_Server.Services;
    using EncChatCommonLib.Models;
    using EncChatCommonLib.Services;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class Program
    {
        private static EncryptionService encryptionService = new EncryptionService();
        private static MessageInterpreter messageInterpreter = new MessageInterpreter(encryptionService);
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        private static IClientsService clientService = new ClientsService();
        private static int currentPageNo = 1;

        private static bool isRunning = false;
        private const int PORT = Constants.PORT;
        private static List<Client> clients = new List<Client>();
        private static List<User> users = new List<User>();

        static Program() {
            const int pageSize = 100;
            users = clientService.GetUsers(currentPageNo, pageSize).Result;
            currentPageNo += pageSize;
        }

        public static Task SendData(string connectionId, byte[] dataToSend)
        {
            return Task.Run(() =>
            {
                var client = clients.FirstOrDefault(c => c.ConnectionID == connectionId);

                if (client != null)
                {
                    try
                    {
                        client.ClientSocket.Send(dataToSend);
                    }
                    catch (Exception ex)
                    {
                        LoggerService.LogError($"Exception occured while sending data to client {client.ConnectionID}: {ex.Message}");
                    }
                }
            });
        }

        static void Main(string[] args)
        {
            isRunning = true;

            Console.CancelKeyPress += Console_CancelKeyPress;
            LoggerService.LogWarning("Initializing ...");

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(1000);

            LoggerService.LogSuccess("Server is Initalized Successfully");
            LoggerService.LogSuccess($"Now Listening on port {PORT} ...");

            while (isRunning)
            {
                Socket clientSocket = serverSocket.Accept();
                LoggerService.LogSpecialMessage($"Client has connected: {clientSocket.RemoteEndPoint}");

                var client = new Client(messageInterpreter)
                {
                    ClientSocket = clientSocket,
                    ConnectionID = Guid.NewGuid().ToString("N"),
                };

                LoggerService.LogSpecialMessage($"Client generated client id: {client.ConnectionID}");

                client.Start();
                clients.Add(client);

                // sending connection id and the encryption key encrypted using the private key of the client
                byte[] dataToSend = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(
                    new MessageObject
                    {
                        ClassName = "Form1",
                        MethodName = "ReceiveServerPublicKey",
                        MethodParams = { 
                            new Parameter
                            {
                                ParamName = "connectionId",
                                ParamValue = client.ConnectionID
                            },
                            new Parameter
                            {
                                ParamName = "encryptionKey",
                                ParamValue = encryptionService.Encrypt(Constants.SHARED_KEY, Constants.PUBLIC_KEY_XML)
                            }
                        }
                    }
                ));
                SendData(client.ConnectionID, dataToSend);
            }

            serverSocket.Close();
        }

        private static void OnClientDisconnect(Client client)
        {
            clients.Remove(client);
            LoggerService.LogError($"Client {client.ConnectionID} has disconnected");

            // sending to everyone that someone has disconnected
            var clientConnectedData = encryptionService.EncryptSymmetericlly(JsonConvert.SerializeObject(
                    new MessageObject
                    {
                        ClassName = "Form1",
                        MethodName = "ReceiveClientDisconnectedMsg",
                        MethodParams =
                        {
                            new Parameter
                            {
                                ParamName = "disconnectedClientConnectionId",
                                ParamValue = client.ConnectionID
                            }
                        }
                    }
                ), Constants.SHARED_KEY);

            foreach (var c in clients)
            {
                if (c.ConnectionID != client.ConnectionID)
                {
                    SendData(c.ConnectionID, clientConnectedData);
                }
            }
        }

        #region Actions
        public static void GetConnectedClients(string clientConnectionId, string email)
        {
            var client = clients.FirstOrDefault(c => c.ConnectionID == clientConnectionId);
            client.ClientName = users.FirstOrDefault(c => c.Email == email)?.Name;
            LoggerService.LogSuccess($"Received GetConnectedClients Message from client: {email}");

            if(client != null)
            {
                // sending to everyone else that someone has connected
                var clientConnectedData = encryptionService.EncryptSymmetericlly(JsonConvert.SerializeObject(
                    new MessageObject
                    {
                        ClassName = "Form1",
                        MethodName = "ReceiveClientConnectedMsg",
                        MethodParams =
                        {
                            new Parameter
                            {
                                ParamName = "connectedClientConnectionId",
                                ParamValue = client.ConnectionID
                            },
                            new Parameter
                            {
                                ParamName = "clientName",
                                ParamValue = client.ClientName
                            }
                        }
                    }
                ), Constants.SHARED_KEY);

                foreach (var c in clients)
                {
                    if (c.ConnectionID != client.ConnectionID)
                    {
                        SendData(c.ConnectionID, clientConnectedData);
                    }
                }

                var data = encryptionService.EncryptSymmetericlly(JsonConvert.SerializeObject(
                    new MessageObject
                    {
                        ClassName = "Form1",
                        MethodName = "ReceiveConnectedClients",
                        MethodParams =
                        {
                            new Parameter
                            {
                                ParamName = "clients",
                                ParamValue = JsonConvert.SerializeObject(clients.Select(c => new { ConnectionId = c.ConnectionID, Name = c.ClientName }).ToList())
                            }
                        }
                    }
                ), Constants.SHARED_KEY);
                SendData(clientConnectionId, data);
            }
        }

        public static void ClientSentMessage(string senderConnectionId, string message, string receiverConnectionId)
        {
            var client = clients.FirstOrDefault(c => c.ConnectionID == receiverConnectionId);
            var senderCilent = clients.FirstOrDefault(c => c.ConnectionID == senderConnectionId);
            LoggerService.LogSuccess($"Client ({senderConnectionId}) sent a message to Client ({receiverConnectionId})");

            if (client != null)
            {
                var data = encryptionService.EncryptSymmetericlly(JsonConvert.SerializeObject(
                    new MessageObject
                    {
                        ClassName = "Form1",
                        MethodName = "ReceiveMessageFromClient",
                        MethodParams =
                        {
                            new Parameter
                            {
                                ParamName = "senderConnectionId",
                                ParamValue = senderConnectionId
                            },
                            new Parameter
                            {
                                ParamName = "name",
                                ParamValue = senderCilent.ClientName
                            },
                            new Parameter
                            {
                                ParamName = "message",
                                ParamValue = message
                            }
                        }
                    }
                ), Constants.SHARED_KEY);
                SendData(receiverConnectionId, data);
            }
        }

        public static void ClientSentImage(string senderConnectionId, string image, string receiverConnectionId)
        {
            var client = clients.FirstOrDefault(c => c.ConnectionID == receiverConnectionId);
            var senderCilent = clients.FirstOrDefault(c => c.ConnectionID == senderConnectionId);
            LoggerService.LogSuccess($"Client ({senderConnectionId}) sent an image message to Client ({receiverConnectionId})");

            if (client != null)
            {
                var data = encryptionService.EncryptSymmetericlly(JsonConvert.SerializeObject(
                    new MessageObject
                    {
                        ClassName = "Form1",
                        MethodName = "ReceiveImageFromClient",
                        MethodParams =
                        {
                            new Parameter
                            {
                                ParamName = "senderConnectionId",
                                ParamValue = senderConnectionId
                            },
                            new Parameter
                            {
                                ParamName = "name",
                                ParamValue = senderCilent.ClientName
                            },
                            new Parameter
                            {
                                ParamName = "image",
                                ParamValue = image
                            }
                        }
                    }
                ), Constants.SHARED_KEY);
                SendData(receiverConnectionId, data);
            }
        }

        public static void ClientDisconnected(string connectionId)
        {
            var client = clients.FirstOrDefault(c => c.ConnectionID == connectionId);

            if(client != null)
            {
                OnClientDisconnect(client);
            }
        }
        #endregion

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            LoggerService.LogError("Stopping the server ...");

            foreach (var client in clients)
            {
                try
                {
                    client.Stop();
                }
                catch (Exception ex) { LoggerService.LogError($"Error occured while stopping client sessions: {ex.Message}"); }
            }

            LoggerService.LogSuccess("Server has stopped successfully");

            isRunning = false;

            Environment.Exit(0);
        }
    }
}