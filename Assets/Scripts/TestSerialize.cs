using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Assets.Scripts.ProtoBufExtention;
using ProtoBuf;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class TestSerialize : MonoBehaviour
    {
        private SerializableClass _bytesInstance;
        private void Awake()
        {
            _bytesInstance = DeSerialize();
        }

        private void OnDestroy()
        {
            Serialize();
        }

        private SerializableClass DeSerialize()
        {
            string _filePath = Application.dataPath + "/bytes.bytes";
            if (File.Exists(_filePath))
            {
                var stream = File.Open(_filePath, FileMode.OpenOrCreate);
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                var result = buffer.FromSafeProtoBufBytes<SerializableClass>();
                Debug.Log(string.Format("load result:{0},({1}bytes)", result, buffer.Length));
                stream.Close();
                return result;
            }
            return null;
        }

        private void Serialize()
        {
            string _filePath = Application.dataPath + "/bytes.bytes";
            if (_bytesInstance == null)
            {
                _bytesInstance = new SerializableClass();
            }
            _bytesInstance.SerializableBool = true;
            _bytesInstance.SerializableDouble = Random.Range(-100.0f, 100.0f);
            _bytesInstance.SerializableFloat = Random.Range(-100.0f, 100.0f);
            _bytesInstance.SerializableInt = Random.Range(-100, 100);
            _bytesInstance.SerializableString = Guid.NewGuid().ToString();
            
            var stream = File.Open(_filePath, FileMode.OpenOrCreate);
            stream.SetLength(0);
            var bytes = _bytesInstance.ToSafeProtoBufBytes();
            stream.Write(bytes,0,bytes.Length);
            stream.Close();
            Debug.Log(string.Format("save result:{0},({1}bytes)",_bytesInstance,bytes.Length));
            
        }

    }
}
