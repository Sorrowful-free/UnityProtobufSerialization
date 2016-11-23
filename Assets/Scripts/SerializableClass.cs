
using ProtoBuf;

namespace Assets.Scripts
{
    [ProtoContract]
    public class SerializableClass
    {
        [ProtoMember(1)]
        public string SerializableString;

        [ProtoMember(2)]
        public int SerializableInt;

        [ProtoMember(3)]
        public float SerializableFloat;

        [ProtoMember(4)]
        public bool SerializableBool;

        [ProtoMember(5)]
        public double SerializableDouble;


        public override string ToString()
        {
            return string.Format("str:{0} int:{1} float:{2} bool:{3} double:{4}", SerializableString, SerializableInt,
                SerializableFloat, SerializableBool, SerializableDouble);
        }
    }
}
