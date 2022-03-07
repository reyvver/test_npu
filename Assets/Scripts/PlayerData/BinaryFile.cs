using System.IO;
using FlatBuffers;
using UnityEngine;

namespace PlayerData
{
    public class BinaryFile
    {
        private static readonly string FilePath = Path.Combine(Application.persistentDataPath, "Saves/save.bin");
        private static readonly string DefaultPath = Path.Combine(Application.dataPath, "Data/defaultdata.bin");

        public BinaryFile()
        {
            string directory = Path.GetDirectoryName(FilePath);
            
            if (!Directory.Exists(directory) && directory != null)
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void WriteToFile(FlatBufferBuilder builder)
        {
            byte[] data = builder.SizedByteArray();
            
            BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create, FileAccess.Write));
            writer.Write(data);
            writer.Close();
        }

        public Data ReadFromFile()
        {
            byte[] data = File.Exists(FilePath) ? File.ReadAllBytes(FilePath) : File.ReadAllBytes(DefaultPath);
            
            var buf = new ByteBuffer(data);
            return Data.GetRootAsData(buf);
        }
    }
}