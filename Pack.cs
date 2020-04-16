using System;
using System.IO;

namespace bafro
{
    class Pack
    {
        SystemTime incomeTime;
        SystemTime startUp;
        SystemTime st;
        UInt32 time;
        public Pack()
        {

        }
        public Pack(SystemTime incomeTime, SystemTime startUp, SystemTime st, UInt32 time)
        {
            set(incomeTime, startUp, st, time);
        }
        public void set(SystemTime incomeTime, SystemTime startUp, SystemTime st, UInt32 time)
        {
            this.incomeTime = incomeTime;
            this.startUp = startUp;
            this.st = st;
            this.time = time;
        }

        public BinaryWriter toBytes(BinaryWriter br)
        {
            br = incomeTime.toBytes(br);
            br = startUp.toBytes(br);
            br = st.toBytes(br);
            br.Write(time);
            return br;
        }
    }
}