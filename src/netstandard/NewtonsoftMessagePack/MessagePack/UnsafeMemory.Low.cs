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
    using System.Runtime.CompilerServices;

    namespace MessagePack.Internal
    {
        // for string key property name write optimization.

        internal static class UnsafeMemory
        {
            public static readonly bool Is32Bit = (IntPtr.Size == 4);
        }

        internal static partial class UnsafeMemory32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw1(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(byte*) pDst = *(byte*) pSrc;
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw2(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(short*) pDst = *(short*) pSrc;
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw3(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(byte*) pDst = *(byte*) pSrc;
                    *(short*) (pDst + 1) = *(short*) (pSrc + 1);
                }

                return src.Length;
            }
        }

        internal static partial class UnsafeMemory64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw1(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(byte*) pDst = *(byte*) pSrc;
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw2(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(short*) pDst = *(short*) pSrc;
                }

                return src.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe int WriteRaw3(ref byte[] dst, int dstOffset, byte[] src)
            {
                MessagePackBinary.EnsureCapacity(ref dst, dstOffset, src.Length);

                fixed (byte* pSrc = &src[0])
                fixed (byte* pDst = &dst[dstOffset])
                {
                    *(byte*) pDst = *(byte*) pSrc;
                    *(short*) (pDst + 1) = *(short*) (pSrc + 1);
                }

                return src.Length;
            }

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
        }
    }
}