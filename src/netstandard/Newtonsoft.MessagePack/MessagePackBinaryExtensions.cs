using System;
using System.IO;
using System.Numerics;
using Newtonsoft.MessagePack.Formatters;

namespace Newtonsoft.MessagePack
{
    internal static partial class MessagePackBinary
    {
        public static int WriteDecimal(Stream stream, Decimal value)
        {
            var buffer = StreamDecodeMemoryPool.GetBuffer();
            var writeCount = DecimalFormatter.Instance.Serialize(ref buffer, 0, value, null);
            stream.Write(buffer, 0, writeCount);
            return writeCount;
        }

        public static int WriteTimeSpan(Stream stream, TimeSpan value)
        {
            var buffer = StreamDecodeMemoryPool.GetBuffer();
            var writeCount = TimeSpanFormatter.Instance.Serialize(ref buffer, 0, value, null);
            stream.Write(buffer, 0, writeCount);
            return writeCount;
        }

        public static int WriteGuid(Stream stream, Guid value)
        {
            var buffer = StreamDecodeMemoryPool.GetBuffer();
            var writeCount = GuidFormatter.Instance.Serialize(ref buffer, 0, value, null);
            stream.Write(buffer, 0, writeCount);
            return writeCount;
        }

        public static int WriteBigInteger(Stream stream, BigInteger value)
        {
            var buffer = StreamDecodeMemoryPool.GetBuffer();
            var writeCount = BigIntegerFormatter.Instance.Serialize(ref buffer, 0, value, null);
            stream.Write(buffer, 0, writeCount);
            return writeCount;
        }

        public static int WriteDateTimeOffset(Stream stream, DateTimeOffset value)
        {
            var buffer = StreamDecodeMemoryPool.GetBuffer();
            var writeCount = DateTimeOffsetFormatter.Instance.Serialize(ref buffer, 0, value, null);
            stream.Write(buffer, 0, writeCount);
            return writeCount;
        }

        public static int WriteUri(Stream stream, Uri value)
        {
            var buffer = StreamDecodeMemoryPool.GetBuffer();
            var writeCount = UriFormatter.Instance.Serialize(ref buffer, 0, value, null);
            stream.Write(buffer, 0, writeCount);
            return writeCount;
        }
    }
}