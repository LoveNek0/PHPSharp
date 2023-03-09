using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Memory.Data
{
    public class MemoryArray : MemoryData
    {
        private Dictionary<MemoryData, MemoryData> values = new Dictionary<MemoryData, MemoryData>();
        public MemoryArray(params MemoryData[] values) : base(DataType.Array)
        {
            foreach (var value in values)
                Add(value);
        }
        public MemoryArray(params KeyValuePair<MemoryData, MemoryData>[] values) : base(DataType.Array)
        {
            foreach(var value in values)
                Set(value.Key, value.Value);
        }

        public MemoryData Get(MemoryData key)
        {
            return null;
        }
        public void Set(MemoryData key, MemoryData value)
        {

        }
        public void Add(MemoryData value) => values.Add(new MemoryInteger(values.Count()), value.Clone());

        public override MemoryBoolean ToMemoryBoolean() => new MemoryBoolean(ToBool());
        public override MemoryInteger ToMemoryInteger() => new MemoryInteger(ToLong());
        public override MemoryString ToMemoryString() => new MemoryString(ToString());
        public override MemoryArray ToMemoryArray() => this;

        public override bool ToBool() => values.Count > 0;
        public override short ToShort() => values.Count == 0 ? (short)0 : values.First().Value.ToShort();
        public override ushort ToUShort() => values.Count == 0 ? (ushort)0 : values.First().Value.ToUShort();
        public override int ToInt() => values.Count == 0 ? 0 : values.First().Value.ToInt();
        public override uint ToUInt() => values.Count == 0 ? 0 : values.First().Value.ToUInt();
        public override long ToLong() => values.Count == 0 ? 0 : values.First().Value.ToLong();
        public override ulong ToULong()=> values.Count == 0 ? 0 : values.First().Value.ToULong();
        public override string ToString() => values.Count == 0 ? "" : values.First().Value.ToString();
        public override IReadOnlyDictionary<MemoryData, MemoryData> ToDictionary() => values;

    }
}
