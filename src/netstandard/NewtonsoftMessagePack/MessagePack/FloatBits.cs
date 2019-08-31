namespace Newtonsoft
{
    // MessagePack for C#
    // 
    // MIT License
    // 
    // Copyright (c) 2017 Yoshifumi Kawai
    // 
    // Permission is hereby granted, free of charge, to any person obtaining a copy
    // of this software and associated documentation files (the "Software"), to deal
    // in the Software without restriction, including without limitation the rights
    // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    // copies of the Software, and to permit persons to whom the Software is
    // furnished to do so, subject to the following conditions:
    // 
    // The above copyright notice and this permission notice shall be included in all
    // copies or substantial portions of the Software.
    // 
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    // SOFTWARE.
    // 
    // ---
    // 
    // lz4net
    // 
    // Copyright (c) 2013-2017, Milosz Krajewski
    // 
    // All rights reserved.
    // 
    // Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
    // 
    // Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    // 
    // Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
    // 
    // THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    using System;
    using System.Runtime.InteropServices;

    namespace MessagePack
    {
        // safe accessor of Single/Double's underlying byte.
        // This code is borrowed from MsgPack-Cli https://github.com/msgpack/msgpack-cli

        [StructLayout(LayoutKind.Explicit)]
        internal struct Float32Bits
        {
            [FieldOffset(0)] public readonly float Value;

            [FieldOffset(0)] public readonly Byte Byte0;

            [FieldOffset(1)] public readonly Byte Byte1;

            [FieldOffset(2)] public readonly Byte Byte2;

            [FieldOffset(3)] public readonly Byte Byte3;

            public Float32Bits(float value)
            {
                this = default(Float32Bits);
                this.Value = value;
            }

            public Float32Bits(byte[] bigEndianBytes, int offset)
            {
                this = default(Float32Bits);

                if (BitConverter.IsLittleEndian)
                {
                    this.Byte0 = bigEndianBytes[offset + 3];
                    this.Byte1 = bigEndianBytes[offset + 2];
                    this.Byte2 = bigEndianBytes[offset + 1];
                    this.Byte3 = bigEndianBytes[offset];
                }
                else
                {
                    this.Byte0 = bigEndianBytes[offset];
                    this.Byte1 = bigEndianBytes[offset + 1];
                    this.Byte2 = bigEndianBytes[offset + 2];
                    this.Byte3 = bigEndianBytes[offset + 3];
                }
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Float64Bits
        {
            [FieldOffset(0)] public readonly double Value;

            [FieldOffset(0)] public readonly Byte Byte0;

            [FieldOffset(1)] public readonly Byte Byte1;

            [FieldOffset(2)] public readonly Byte Byte2;

            [FieldOffset(3)] public readonly Byte Byte3;

            [FieldOffset(4)] public readonly Byte Byte4;

            [FieldOffset(5)] public readonly Byte Byte5;

            [FieldOffset(6)] public readonly Byte Byte6;

            [FieldOffset(7)] public readonly Byte Byte7;

            public Float64Bits(double value)
            {
                this = default(Float64Bits);
                this.Value = value;
            }

            public Float64Bits(byte[] bigEndianBytes, int offset)
            {
                this = default(Float64Bits);

                if (BitConverter.IsLittleEndian)
                {
                    this.Byte0 = bigEndianBytes[offset + 7];
                    this.Byte1 = bigEndianBytes[offset + 6];
                    this.Byte2 = bigEndianBytes[offset + 5];
                    this.Byte3 = bigEndianBytes[offset + 4];
                    this.Byte4 = bigEndianBytes[offset + 3];
                    this.Byte5 = bigEndianBytes[offset + 2];
                    this.Byte6 = bigEndianBytes[offset + 1];
                    this.Byte7 = bigEndianBytes[offset];
                }
                else
                {
                    this.Byte0 = bigEndianBytes[offset];
                    this.Byte1 = bigEndianBytes[offset + 1];
                    this.Byte2 = bigEndianBytes[offset + 2];
                    this.Byte3 = bigEndianBytes[offset + 3];
                    this.Byte4 = bigEndianBytes[offset + 4];
                    this.Byte5 = bigEndianBytes[offset + 5];
                    this.Byte6 = bigEndianBytes[offset + 6];
                    this.Byte7 = bigEndianBytes[offset + 7];
                }
            }
        }
    }
}