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
    using System.Runtime.CompilerServices;

    namespace MessagePack.Internal
    {
        internal static partial class UnsafeMemory32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw4(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw5(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 1) = *(int*) (pSrc + 1);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw6(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 2) = *(int*) (pSrc + 2);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw7(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 3) = *(int*) (pSrc + 3);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw8(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw9(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 5) = *(int*) (pSrc + 5);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw10(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 6) = *(int*) (pSrc + 6);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw11(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 7) = *(int*) (pSrc + 7);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw12(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw13(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 9) = *(int*) (pSrc + 9);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw14(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 10) = *(int*) (pSrc + 10);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw15(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 11) = *(int*) (pSrc + 11);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw16(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw17(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 13) = *(int*) (pSrc + 13);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw18(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 14) = *(int*) (pSrc + 14);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw19(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 15) = *(int*) (pSrc + 15);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw20(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw21(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 17) = *(int*) (pSrc + 17);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw22(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 18) = *(int*) (pSrc + 18);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw23(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 19) = *(int*) (pSrc + 19);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw24(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 20) = *(int*) (pSrc + 20);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw25(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 20) = *(int*) (pSrc + 20);
                    *(int*) (pDst + 21) = *(int*) (pSrc + 21);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw26(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 20) = *(int*) (pSrc + 20);
                    *(int*) (pDst + 22) = *(int*) (pSrc + 22);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw27(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 20) = *(int*) (pSrc + 20);
                    *(int*) (pDst + 23) = *(int*) (pSrc + 23);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw28(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 20) = *(int*) (pSrc + 20);
                    *(int*) (pDst + 24) = *(int*) (pSrc + 24);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw29(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 20) = *(int*) (pSrc + 20);
                    *(int*) (pDst + 24) = *(int*) (pSrc + 24);
                    *(int*) (pDst + 25) = *(int*) (pSrc + 25);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw30(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 20) = *(int*) (pSrc + 20);
                    *(int*) (pDst + 24) = *(int*) (pSrc + 24);
                    *(int*) (pDst + 26) = *(int*) (pSrc + 26);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw31(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(int*) (pDst + 0) = *(int*) (pSrc + 0);
                    *(int*) (pDst + 4) = *(int*) (pSrc + 4);
                    *(int*) (pDst + 8) = *(int*) (pSrc + 8);
                    *(int*) (pDst + 12) = *(int*) (pSrc + 12);
                    *(int*) (pDst + 16) = *(int*) (pSrc + 16);
                    *(int*) (pDst + 20) = *(int*) (pSrc + 20);
                    *(int*) (pDst + 24) = *(int*) (pSrc + 24);
                    *(int*) (pDst + 27) = *(int*) (pSrc + 27);
                }

                return src.Length;
            }

        }

        internal static partial class UnsafeMemory64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw8(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw9(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 1) = *(long*) (pSrc + 1);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw10(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 2) = *(long*) (pSrc + 2);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw11(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 3) = *(long*) (pSrc + 3);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw12(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 4) = *(long*) (pSrc + 4);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw13(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 5) = *(long*) (pSrc + 5);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw14(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 6) = *(long*) (pSrc + 6);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw15(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 7) = *(long*) (pSrc + 7);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw16(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw17(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 9) = *(long*) (pSrc + 9);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw18(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 10) = *(long*) (pSrc + 10);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw19(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 11) = *(long*) (pSrc + 11);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw20(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 12) = *(long*) (pSrc + 12);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw21(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 13) = *(long*) (pSrc + 13);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw22(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 14) = *(long*) (pSrc + 14);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw23(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 15) = *(long*) (pSrc + 15);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw24(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 16) = *(long*) (pSrc + 16);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw25(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 16) = *(long*) (pSrc + 16);
                    *(long*) (pDst + 17) = *(long*) (pSrc + 17);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw26(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 16) = *(long*) (pSrc + 16);
                    *(long*) (pDst + 18) = *(long*) (pSrc + 18);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw27(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 16) = *(long*) (pSrc + 16);
                    *(long*) (pDst + 19) = *(long*) (pSrc + 19);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw28(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 16) = *(long*) (pSrc + 16);
                    *(long*) (pDst + 20) = *(long*) (pSrc + 20);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw29(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 16) = *(long*) (pSrc + 16);
                    *(long*) (pDst + 21) = *(long*) (pSrc + 21);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw30(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 16) = *(long*) (pSrc + 16);
                    *(long*) (pDst + 22) = *(long*) (pSrc + 22);
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw31(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(long*) (pDst + 0) = *(long*) (pSrc + 0);
                    *(long*) (pDst + 8) = *(long*) (pSrc + 8);
                    *(long*) (pDst + 16) = *(long*) (pSrc + 16);
                    *(long*) (pDst + 23) = *(long*) (pSrc + 23);
                }

                return src.Length;
            }

        }
    }
}