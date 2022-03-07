using System.Collections.Concurrent;
using UnityEngine;

namespace PlayerData
{
    public class Sender
    {
        private readonly ConcurrentQueue<byte[]> _buffers;

        public Sender()
        {
            _buffers = new ConcurrentQueue<byte[]>();
        }

        public void SendToServer(byte[] buffer)
        {
            _buffers.Enqueue(buffer);
            /* send to server code */
        }

        public void GetResult()
        {
            Debug.Log($"Total send {_buffers.Count}");
        }
    }
}