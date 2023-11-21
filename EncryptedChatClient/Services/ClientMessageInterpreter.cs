using EncChatCommonLib.Models;
using EncChatCommonLib.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EcnryptedChatClient.Services
{
    public class ClientMessageInterpreter
    {
        private readonly EncryptionService _encryptionService;

        public ClientMessageInterpreter(EncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        private bool IsDataEncrypted(byte[] data, byte[] key)
        {
            try
            {
                _encryptionService.DecryptSymmetericlly(data, key);
                return true;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        public void InterpretAndDecrypt(string projectNamespace, byte[] message, byte[] key)
        {
            string msgStr;

            if(key != null && IsDataEncrypted(message, key))
                msgStr = _encryptionService.DecryptSymmetericlly(message, key);
            else
                msgStr = Encoding.ASCII.GetString(message);

            var msgObj = JsonConvert.DeserializeObject<MessageObject>(msgStr);

            var classObj = Type.GetType($"{projectNamespace}.{msgObj.ClassName}");
            object[] methodParams = new object[msgObj.MethodParams.Count];
            int counter = 0;

            foreach(var parameter in msgObj.MethodParams)
            {
                methodParams[counter++] = parameter.ParamValue;
            }

            classObj.GetMethod(msgObj.MethodName).Invoke(Application.OpenForms[1], methodParams.Length > 0 ? methodParams : null);
        }
    }
}
