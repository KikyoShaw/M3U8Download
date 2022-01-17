using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M3u8Puller.Kit
{
    class SeqKit
    {
        static long current = 0;

        public static long Next()
        {
            long seq = 0;
            lock (typeof(SeqKit))
            {
                current++;
                seq = current;
                if (current > int.MaxValue)
                {
                    current = 0;
                }
            }
            return seq;
        }
    }
}
