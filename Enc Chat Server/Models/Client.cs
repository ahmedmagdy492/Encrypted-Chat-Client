using Enc_Chat_Server.Services;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;

namespace Enc_Chat_Server.Models
{
    internal class Client
    {
        private Task _clientSession;
        private readonly MessageInterpreter _messageInterpreter;

        private bool isRunning = false;

        public string ConnectionID { get; set; }
        public string ClientName { get; set; }
        public Socket ClientSocket { get; set; }

        public Client(MessageInterpreter messageInterpreter)
        {
            _messageInterpreter = messageInterpreter;
        }

        public void Start()
        {
            _clientSession = new Task(ClientSessionHandler);
            isRunning = true;
            _clientSession.Start();
        }

        private void ClientSessionHandler()
        {
            while (isRunning)
            {
                if (ClientSocket.Available > 0)
                {
                    byte[] messageBuffer = new byte[Constants.MAX_MSG_SIZE_IN_BYTES];
                    int receivedAmount = ClientSocket.Receive(messageBuffer);
                    byte[] alignedBuffer = new byte[receivedAmount];
                    Array.Copy(messageBuffer, alignedBuffer, receivedAmount);
                    _messageInterpreter.InterpretAndDecrypt("Enc_Chat_Server", alignedBuffer);
                }
            }
        }

        public void Stop()
        {
            isRunning = false;
        }
    }
}
