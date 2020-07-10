using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSU.ThreeLayer.Entities
{
    public struct Pair<T1, T2>
    {
        public T1 Key { get; set; }
        public T2 Value { get; set; }

        public Pair(T1 Key, T2 Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}
