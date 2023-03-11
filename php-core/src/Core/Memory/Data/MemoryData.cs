using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Memory.Data
{
    public abstract class MemoryData
    {
        public enum DataType
        {
            Boolean,
            Integer,
            Float,
            String,
            Array,
            Object
        }
        public readonly DataType Type;

        protected MemoryData(DataType type) => Type = type;

        public abstract MemoryData Clone();
        public abstract bool Equals(MemoryData data);

        public abstract MemoryBoolean ToMemoryBoolean();
        public abstract MemoryInteger ToMemoryInteger();
        public abstract MemoryFloat ToMemoryFloat();
        public abstract MemoryString ToMemoryString();
        public abstract MemoryArray ToMemoryArray();

        public abstract bool ToBool();
        public abstract short ToShort();
        public abstract ushort ToUShort();
        public abstract int ToInt();
        public abstract uint ToUInt();
        public abstract long ToLong();
        public abstract ulong ToULong();
        public abstract float ToFloat();
        public abstract double ToDouble();
        public abstract decimal ToDecimal();
        public abstract override string ToString();
        public abstract IReadOnlyDictionary<MemoryData, MemoryData> ToDictionary();
    }
}
