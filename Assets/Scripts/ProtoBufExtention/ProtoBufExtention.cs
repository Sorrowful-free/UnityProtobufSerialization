using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProtoBuf;

namespace Assets.Scripts.ProtoBufExtention
{
    public static class ProtoBufExtention
    {
        public static byte[] ToProtoBufBytes<TType>(this TType instance)
        {
            var memory = new MemoryStream();
            Serializer.Serialize(memory,instance);
            var buffer = memory.GetBuffer().Take((int)memory.Length).ToArray();
            memory.Close();
            return buffer;
        }

        public static TType FromProtoBufBytes<TType>(this byte[] bytes)
        {
            var instance = default(TType);
            var memory = new MemoryStream(bytes);
            instance = Serializer.Deserialize<TType>(memory);
            memory.Close();
            return instance;
        }

        public static byte[] ToSafeProtoBufBytes<TType>(this TType instance)
        {
            var protobufBytes = instance.ToProtoBufBytes();
            var buffer = new List<byte>();
            var byteLengs = BitConverter.GetBytes(protobufBytes.Length);
            buffer.AddRange(byteLengs);
            buffer.AddRange(protobufBytes);
            return buffer.ToArray();
        }

        public static TType FromSafeProtoBufBytes<TType>(this byte[] bytes)
        {
            var length = BitConverter.ToInt32(bytes.Take(4).ToArray(), 0);
            var protobufBytes = bytes.Skip(4).Take(length).ToArray();
            var instance = protobufBytes.FromProtoBufBytes<TType>();
            return instance;
        }
    }
}
