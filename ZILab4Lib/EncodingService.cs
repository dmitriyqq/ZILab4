using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZILab3Lib
{
    public class EncodingService
    {
        public EncodingService()
        {
            //uint[] key = new uint[4] { 99999, 88888, 77777, 66666 };
            //uint[] test = new uint[4] { 945779817, 5325131, 0, 0 };

            //EncodeBlock(test, key);
            //uint[] encoded = new uint[] { test[0], test[1], test[2], test[3] };
            //var t = string.Join(",", test);
            //DecodeBlock(test, key);
            //var t2 = string.Join(",", test);
        }

        private uint CreateInt(byte[] data, int i)
        {
            uint code = 0;
            for (int j = 3; j >= 0; j--)
            {
                if (i + j < data.Length) {
                    code <<= 8;
                    code |= data[i + j];
                }
            }
            return code;
        }

        private byte[] WriteBytes(uint[] ints) {
            var bytes = new List<byte>();

            for (int i = 0; i < ints.Length; i++)
            {
                    bytes.Add((byte)ints[i]);
                    bytes.Add((byte)(ints[i] >> 8));
                    bytes.Add((byte)(ints[i] >> 16));
                    bytes.Add((byte)(ints[i] >> 24));
            }

            return bytes.ToArray();
        }


        public byte[] Encode(byte[] data, uint[] key)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var intData = new List<uint>();
            for (int i = 0; i < data.Length; i+=4)
            {
                intData.Add(CreateInt(data, i));
            }

            while (intData.Count % 4 != 0)
            {
                intData.Add(0);
            }

            var intDataArray = intData.ToArray();

            uint[] MAC = null;

            for(int i = 0; i < intDataArray.Length; i+= 4)
            {
                uint[] block2;

                if (MAC == null)
                {
                    block2 = new uint[4] { intDataArray[i], intDataArray[i + 1], intDataArray[i + 2], intDataArray[i + 3] };
                } else
                {
                    block2 = new uint[4] { intDataArray[i] ^ MAC[0], intDataArray[i + 1] ^ MAC[1], intDataArray[i + 2] ^ MAC[2], intDataArray[i + 3] ^ MAC[3] };
                }

                EncodeBlock(block2, key);

                MAC = block2;

                intDataArray[i] = block2[0];
                intDataArray[i + 1] = block2[1];
                intDataArray[i + 2] = block2[2];
                intDataArray[i + 3] = block2[3];
            }

            return WriteBytes(MAC);
        }

        private void printBlock(string code, int stage, uint[] block)
        {
            //var t = string.Join(",", block);
            //Console.WriteLine("{0}-{1}: {2}", code, stage, t);
        }

        private void EncodeBlock(uint[] block, uint[] key)
        {
            //printBlock("e", 0, block);
            for (int i = 0; i < 4; i++)
            {
                block[i] ^= key[(i) % 4];
            }
            //printBlock("e", 1, block);
            for (int j = 1; j <= 6; j++)
            {
                f(block);
                //printBlock("ef", j+1, block);
                for (int i = 0; i < 4; i++)
                {
                    block[i] ^= key[(i + j) % 4];
                }
                //printBlock("e", j+1, block);
            }
        }

        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        private void f(uint[] block)
        {
            const uint C = 0x2aaaaaaa;
            const ulong C0 = 0x025f1cdb;
            const ulong C1 = C0 << 1;
            const ulong C2 = C0 << 3;
            const ulong C3 = (C0 << 7) % uint.MaxValue;


            ulong[] c = new ulong[4] { C0, C1, C2, C3 };
            
            //printBlock("fstart", 0, block);

            for (int i = 0; i < 4; i++)
            {
                ulong num = (ulong)(c[i]) * (ulong)block[i];
                block[i] = block[i] == uint.MaxValue ? block[i] : (uint)((num) % uint.MaxValue);
            }

            //printBlock("fmul", 1, block);

            if ((block[0] & 1) == 1)
            {
                block[0] ^= C;
            }

            if ((block[2] & 1) == 0)
            {
                block[2] ^= C;
            }

            //printBlock("fc", 2, block);

            for (int i = 0; i < 4; i++)
            {
                block[i] = block[mod(i - 1, 4)] ^ (block[i]) ^ block[(i + 1) % 4]; // ^ (block[(i + 1) % 4]);
            }

            //printBlock("fxor", 3, block);
        }
    }
}

