using UnityEngine;
using FlatBuffers;

namespace PlayerData
{
    public class SaveAndLoad
    {
        public struct SaveData
        {
            public Vector3 position;
        }

        private readonly FlatBufferBuilder _builder;
        private readonly BinaryFile _binaryFile;

        public SaveAndLoad()
        {
            _binaryFile = new BinaryFile();
            _builder = new FlatBufferBuilder(1024);
        }
        
        public void SaveUserData(SaveData dataToSave)
        {
            var position = Vec3.CreateVec3(_builder, 
                dataToSave.position.x, 
                dataToSave.position.y, 
                dataToSave.position.z);
            
            Data.StartData(_builder);
            Data.AddPosition(_builder, position);
            var playerData = Data.EndData(_builder);
            
            _builder.Finish(playerData.Value);
            Data.FinishDataBuffer(_builder, playerData);

            _binaryFile.WriteToFile(_builder);
        }

        public SaveData LoadUserData()
        {
            var data = _binaryFile.ReadFromFile();
            SaveData savedData = new SaveData();

            var position = data.Position.Value;
            savedData.position = new Vector3(position.X, position.Y, position.Z);
            
            return savedData;
        }
    }
}