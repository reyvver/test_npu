using FlatBuffers;
using UnityEngine;

namespace PlayerData
{
    public class ServerDataManager
    {
        private readonly Sender _sender;
        private readonly FlatBufferBuilder _builder;
        
        public ServerDataManager()
        {
            _sender = new Sender();
            _builder = new FlatBufferBuilder(1024);
        }
        
        public void SendDataToServer<T>(T data)
        {
            BuildNetworkPacket(data);
            _sender.SendToServer(GetCurrentBuffer());
        }

        public void Stop()
        {
            _sender.GetResult();
        }

        private void BuildNetworkPacket<T>(T data)
        {
            _builder.Clear();
            
            switch (data)
            {
                case string stringData:
                {
                    Debug.Log(stringData);
                    StringPacket(stringData);
                    break;
                }
                /* etc different types */
            }
        }

        private void StringPacket(string message)
        {
            var packetMessage = _builder.CreateString(message);

            ServerData.StartServerData(_builder);
            ServerData.AddMessage(_builder, packetMessage);
            
            var packet = ServerData.EndServerData(_builder);
            
            _builder.Finish(packet.Value);
            ServerData.FinishServerDataBuffer(_builder, packet);
        }

        private byte[] GetCurrentBuffer()
        {
            return _builder.SizedByteArray();
        }
    }
}