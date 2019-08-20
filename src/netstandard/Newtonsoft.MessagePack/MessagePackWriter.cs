using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using MessagePack;

namespace Newtonsoft.Json.MessagePack
{
    /// <remarks>
    /// This class is mostly based on
    /// https://github.com/neuecc/MessagePack-CSharp/blob/v1.7.3.7/src/MessagePack/Formatters/PrimitiveObjectFormatter.cs
    /// and
    /// https://github.com/neuecc/MessagePack-CSharp/blob/v1.7.3.7/src/MessagePack/Formatters/StandardClassLibraryFormatter.cs
    /// </remarks>
    public class MessagePackWriter : JsonWriter
    {
        private readonly Stream mStream;
        private MessagePackToken mDocument;
        private MessagePackToken mState;
        private string mCurrentPropertyName;

        public MessagePackWriter(Stream stream)
        {
            mStream = stream;
        }

        public override void Flush()
        {
            mStream.Flush();
        }

        public override void WriteValue(byte[] value)
        {
            base.WriteValue(value);
            WritePrimitive(value);
        }

        public override void WriteValue(string value)
        {
            base.WriteValue(value);
            WritePrimitive(value);
        }

        public override void WriteValue(int value)
        {
            base.WriteValue(value);
            WritePrimitive(new Int32MessagePackValue(mState, value));
        }

        public override void WriteValue(uint value)
        {
            base.WriteValue(value);
            WritePrimitive(new UInt32MessagePackValue(mState, value));
        }

        public override void WriteValue(long value)
        {
            base.WriteValue(value);
            WritePrimitive(new Int64MessagePackValue(mState, value));
        }

        public override void WriteValue(ulong value)
        {
            base.WriteValue(value);
            WritePrimitive(new UInt64MessagePackValue(mState, value));
        }

        public override void WriteValue(float value)
        {
            base.WriteValue(value);
            WritePrimitive(new SingleMessagePackValue(mState, value));
        }

        public override void WriteValue(double value)
        {
            base.WriteValue(value);
            WritePrimitive(new DoubleMessagePackValue(mState, value));
        }

        public override void WriteValue(bool value)
        {
            base.WriteValue(value);
            WritePrimitive(new BooleanMessagePackValue(mState, value));
        }

        public override void WriteValue(short value)
        {
            base.WriteValue(value);
            WritePrimitive(new Int16MessagePackValue(mState, value));
        }

        public override void WriteValue(ushort value)
        {
            base.WriteValue(value);
            WritePrimitive(new UInt16MessagePackValue(mState, value));
        }

        public override void WriteValue(char value)
        {
            base.WriteValue(value);
            WritePrimitive(new CharMessagePackValue(mState, value));
        }

        public override void WriteValue(byte value)
        {
            base.WriteValue(value);
            WritePrimitive(new ByteMessagePackValue(mState, value));
        }

        public override void WriteValue(sbyte value)
        {
            base.WriteValue(value);
            WritePrimitive(new SByteMessagePackValue(mState, value));
        }

        public override void WriteValue(DateTime value)
        {
            base.WriteValue(value);

            WritePrimitive(new DateTimeMessagePackValue(mState, value));
        }

        public override void WriteValue(Guid value)
        {
            base.WriteValue(value);
            WritePrimitive(new GuidMessagePackValue(mState, value));
        }

        public override void WriteValue(TimeSpan value)
        {
            base.WriteValue(value);
            WritePrimitive(new TimeSpanMessagePackValue(mState, value));
        }

        public override void WriteValue(decimal value)
        {
            base.WriteValue(value);
            WritePrimitive(new DecimalMessagePackValue(mState, value));
        }

        public override void WriteValue(DateTimeOffset value)
        {
            base.WriteValue(value);
            WritePrimitive(new DateTimeOffsetMessagePackValue(mState, value));
        }

        public override void WriteValue(Uri value)
        {
            base.WriteValue(value);
            WritePrimitive(new UriMessagePackValue(mState, value));
        }

        public override void WriteValue(object value)
        {
            if (value is BigInteger)
            {
                WriteBigInteger((BigInteger) value);
                SetWriteState(JsonToken.Integer, value);
            }
            else
            {
                base.WriteValue(value);
            }
        }

        private void WriteBigInteger(BigInteger value)
        {
            WritePrimitive(new BigIntegerMessagePackValue(mState, value));
        }

        public override void WriteNull()
        {
            base.WriteNull();
            WritePrimitive(new NullMsgValue(mState));
        }

        public override void WritePropertyName(string name)
        {
            base.WritePropertyName(name);
            mCurrentPropertyName = name;
        }

        public override void WriteEndArray()
        {
            base.WriteEndArray();
            EndState();
        }

        public override void WriteStartArray()
        {
            base.WriteStartArray();
            SetState(new MessagePackArray(mState));
        }

        public override void WriteEndObject()
        {
            EndState();
            base.WriteEndObject();
        }

        public override void WriteStartObject()
        {
            base.WriteStartObject();

            SetState(new MessagePackObject(mState));
        }

        private void WritePrimitive(byte[] value)
        {
            WritePrimitive(new BinaryMessagePackValue(mState, value));
        }

        private void WritePrimitive(string value)
        {
            WritePrimitive(new StringMessagePackValue(mState, value));
        }

        private void WritePrimitive(MessagePackToken messagePackValue)
        {
            if (mState != null)
            {
                mState.AddChild(mCurrentPropertyName, messagePackValue);
            }
            else
            {
                SetState(messagePackValue);
                EndState();
            }
        }

        private void SetState(MessagePackToken token)
        {
            if (mState == null)
            {
                mDocument = token;
            }
            else
            {
                mState.AddChild(mCurrentPropertyName, token);
            }

            mState = token;
        }

        private void EndState()
        {
            mState = mState.Parent;

            if (mState == null)
            {
                mDocument.Write(mStream);
            }
        }

        private abstract class MessagePackToken
        {
            private readonly MessagePackToken mParent;

            public MessagePackToken() : this(null)
            {
            }

            public MessagePackToken(MessagePackToken parent)
            {
                mParent = parent;
            }

            public MessagePackToken Parent
            {
                get { return mParent; }
            }

            public abstract void AddChild(string propertyName, MessagePackToken child);

            public abstract void Write(Stream stream);
        }

        private sealed class MessagePackObject : MessagePackToken
        {
            private readonly List<MessagePackProperty> mChildren = new List<MessagePackProperty>();

            public MessagePackObject()
            {
            }

            public MessagePackObject(MessagePackToken parent) : base(parent)
            {
            }

            public ICollection<MessagePackProperty> Children
            {
                get { return mChildren; }
            }

            public override void AddChild(string propertyName, MessagePackToken child)
            {
                mChildren.Add(new MessagePackProperty()
                              {
                                  Name = propertyName,
                                  Value = child
                              });
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteMapHeader(stream, mChildren.Count);

                foreach (MessagePackProperty child in mChildren)
                {
                    MessagePackBinary.WriteString(stream, child.Name);
                    child.Value.Write(stream);
                }
            }
        }

        private sealed class MessagePackProperty
        {
            public string Name { get; set; }
            public MessagePackToken Value { get; set; }
        }

        private sealed class MessagePackArray : MessagePackToken
        {
            private readonly List<MessagePackToken> mChildren = new List<MessagePackToken>();

            public MessagePackArray(MessagePackToken parent) : base(parent)
            {
            }

            public ICollection<MessagePackToken> Children
            {
                get { return mChildren; }
            }

            public override void AddChild(string propertyName, MessagePackToken child)
            {
                mChildren.Add(child);
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteArrayHeader(stream, mChildren.Count);

                foreach (MessagePackToken child in mChildren)
                {
                    child.Write(stream);
                }
            }
        }

        private abstract class MessagePackValue<T> : MessagePackToken
        {
            private readonly T mValue;

            public MessagePackValue(MessagePackToken parent, T value) :
                base(parent)
            {
                mValue = value;
            }

            public T Value
            {
                get { return mValue; }
            }

            public override void AddChild(string propertyName, MessagePackToken child)
            {
                throw new NotSupportedException();
            }
        }

        private sealed class SingleMessagePackValue : MessagePackValue<float>
        {
            public SingleMessagePackValue(MessagePackToken parent, float value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteSingle(stream, this.Value);
            }
        }

        private sealed class DoubleMessagePackValue : MessagePackValue<double>
        {
            public DoubleMessagePackValue(MessagePackToken parent, double value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteDouble(stream, this.Value);
            }
        }

        private sealed class UInt64MessagePackValue : MessagePackValue<ulong>
        {
            public UInt64MessagePackValue(MessagePackToken parent, ulong value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteUInt64(stream, this.Value);
            }
        }

        private sealed class Int64MessagePackValue : MessagePackValue<long>
        {
            public Int64MessagePackValue(MessagePackToken parent, long value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteInt64(stream, this.Value);
            }
        }

        private sealed class UInt32MessagePackValue : MessagePackValue<uint>
        {
            public UInt32MessagePackValue(MessagePackToken parent, uint value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteUInt32(stream, this.Value);
            }
        }

        private sealed class Int32MessagePackValue : MessagePackValue<int>
        {
            public Int32MessagePackValue(MessagePackToken parent, int value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteInt32(stream, this.Value);
            }
        }

        private sealed class UInt16MessagePackValue : MessagePackValue<ushort>
        {
            public UInt16MessagePackValue(MessagePackToken parent, ushort value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteUInt16(stream, this.Value);
            }
        }

        private sealed class Int16MessagePackValue : MessagePackValue<short>
        {
            public Int16MessagePackValue(MessagePackToken parent, short value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteInt16(stream, this.Value);
            }
        }

        private sealed class SByteMessagePackValue : MessagePackValue<sbyte>
        {
            public SByteMessagePackValue(MessagePackToken parent, sbyte value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteSByte(stream, this.Value);
            }
        }

        private sealed class ByteMessagePackValue : MessagePackValue<byte>
        {
            public ByteMessagePackValue(MessagePackToken parent, byte value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteByte(stream, this.Value);
            }
        }

        private sealed class BooleanMessagePackValue : MessagePackValue<bool>
        {
            public BooleanMessagePackValue(MessagePackToken parent, bool value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteBoolean(stream, this.Value);
            }
        }

        private sealed class NullMsgValue : MessagePackValue<object>
        {
            public NullMsgValue(MessagePackToken parent) : base(parent, null)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteNil(stream);
            }
        }

        private sealed class BinaryMessagePackValue : MessagePackValue<byte[]>
        {
            public BinaryMessagePackValue(MessagePackToken parent, byte[] value) : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteBytes(stream, Value);
            }
        }

        private sealed class StringMessagePackValue : MessagePackValue<string>
        {
            public StringMessagePackValue(MessagePackToken parent, string value) : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteString(stream, Value);
            }
        }

        private sealed class BigIntegerMessagePackValue : MessagePackValue<BigInteger>
        {
            public BigIntegerMessagePackValue(MessagePackToken parent, BigInteger value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteBytes(stream, this.Value.ToByteArray());
            }
        }

        private sealed class GuidMessagePackValue : MessagePackValue<Guid>
        {
            public GuidMessagePackValue(MessagePackToken parent, Guid value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteBytes(stream, this.Value.ToByteArray());
            }
        }

        private sealed class CharMessagePackValue : MessagePackValue<char>
        {
            public CharMessagePackValue(MessagePackToken parent, char value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteChar(stream, this.Value);
            }
        }

        private sealed class DateTimeMessagePackValue : MessagePackValue<DateTime>
        {
            public DateTimeMessagePackValue(MessagePackToken parent, DateTime value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteDateTime(stream, this.Value);
            }
        }

        private sealed class TimeSpanMessagePackValue : MessagePackValue<TimeSpan>
        {
            public TimeSpanMessagePackValue(MessagePackToken parent, TimeSpan value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteInt64(stream, this.Value.Ticks);
            }
        }

        private sealed class DecimalMessagePackValue : MessagePackValue<decimal>
        {
            public DecimalMessagePackValue(MessagePackToken parent, decimal value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteString(stream, this.Value.ToString(CultureInfo.InvariantCulture));
            }
        }

        private sealed class DateTimeOffsetMessagePackValue : MessagePackValue<DateTimeOffset>
        {
            public DateTimeOffsetMessagePackValue(MessagePackToken parent, DateTimeOffset value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteArrayHeader(stream, 2);
                MessagePackBinary.WriteDateTime(stream, new DateTime(this.Value.Ticks, DateTimeKind.Utc));
                MessagePackBinary.WriteInt16(stream, (short) this.Value.Offset.TotalMinutes);
            }
        }

        private sealed class UriMessagePackValue : MessagePackValue<Uri>
        {
            public UriMessagePackValue(MessagePackToken parent, Uri value)
                : base(parent, value)
            {
            }

            public override void Write(Stream stream)
            {
                MessagePackBinary.WriteString(stream, this.Value.ToString());
            }
        }
    }
}