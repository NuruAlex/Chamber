using Events;
using Events.Args;

namespace Chamber.Recievers.CallBack
{
    [Serializable]
    public class CallBackPacket
    {
        public int Number { get; set; } = -1;
        public long ChatId { get; set; } = -1;
        public CallBackCode Code { get; set; } = CallBackCode.Ingnore;
        public string SendData { get; set; }

        private readonly string? _callBack;

        public CallBackPacket(string callBackData)
        {
            _callBack = callBackData;
            SendData = "";
        }
        public CallBackPacket(long chatId, CallBackCode code, int number)
        {
            ChatId = chatId;
            Code = code;
            Number = number;
            SendData = "";
        }
        public CallBackPacket(long chatId, CallBackCode code, int number, string sendData)
        {
            ChatId = chatId;
            Code = code;
            Number = number;
            SendData = sendData;
        }
        public CallBackPacket(CallBackCode code)
        {
            Code = code;
            SendData = "";
        }

        public CallBackPacket(long chatId, CallBackCode code)
        {
            ChatId = chatId;
            Code = code;
            SendData = "";
        }

        public CallBackPacket(string textData, CallBackCode code)
        {
            SendData = textData;
            Code = code;
        }
        /// <summary>
        /// Метод задает формат для call back пакета 
        /// </summary>
        /// <returns>чат.код.номер.текстовые данные</returns>
        public string Pack()
        {
            return $"{ChatId}.{(int)Code}.{Number}.{SendData}";
        }


        public void Unpack()
        {
            string[]? split = _callBack?.Split('.');

            if (split == null)
            {
                PriorityEventHandler.Invoke(new ErrorArgs(new Exception("call back unpack error")));
                return;
            }

            ChatId = long.Parse(split[0]);
            Code = (CallBackCode)Enum.Parse(typeof(CallBackCode), split[1]);
            Number = int.Parse(split[2]);
            SendData = split[3];
        }
    }
}
