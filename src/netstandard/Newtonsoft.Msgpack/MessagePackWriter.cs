﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using MsgPack;
using Newtonsoft.Json;

namespace Newtonsoft.Msgpack
{
    public class MessagePackWriter : JsonWriter
    {
        private readonly Stream mStream;
        private MsgpackToken mDocument;
        private MsgpackToken mState;
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
            WritePrimitive(new Int32MsgpackValue(mState, value));
        }

        public override void WriteValue(uint value)
        {
            base.WriteValue(value);
            WritePrimitive(new UInt32MsgpackValue(mState, value));
        }

        public override void WriteValue(long value)
        {
            base.WriteValue(value);
            WritePrimitive(new Int64MsgpackValue(mState, value));
        }

        public override void WriteValue(ulong value)
        {
            base.WriteValue(value);
            WritePrimitive(new UInt64MsgpackValue(mState, value));
        }

        public override void WriteValue(float value)
        {
            base.WriteValue(value);
            WritePrimitive(new SingleMsgpackValue(mState, value));
        }

        public override void WriteValue(double value)
        {
            base.WriteValue(value);
            WritePrimitive(new DoubleMsgpackValue(mState, value));
        }

        public override void WriteValue(bool value)
        {
            base.WriteValue(value);
            WritePrimitive(new BooleanMsgpackValue(mState, value));
        }

        public override void WriteValue(short value)
        {
            base.WriteValue(value);
            WritePrimitive(new Int16MsgpackValue(mState, value));
        }

        public override void WriteValue(ushort value)
        {
            base.WriteValue(value);
            WritePrimitive(new UInt16MsgpackValue(mState, value));
        }

        public override void WriteValue(char value)
        {
            base.WriteValue(value);
            WritePredefined<char>(value);
        }

        public override void WriteValue(byte value)
        {
            base.WriteValue(value);
            WritePrimitive(new ByteMsgpackValue(mState, value));
        }

        public override void WriteValue(sbyte value)
        {
            base.WriteValue(value);
            WritePrimitive(new SByteMsgpackValue(mState, value));
        }

        public override void WriteValue(decimal value)
        {
            base.WriteValue(value);
            WritePredefined<decimal>(value);
        }

        public override void WriteValue(DateTime value)
        {
            base.WriteValue(value);

            if (this.WriteDateTimeAsString)
            {
                string asString = ConvertDateTimeToString(value);

                WritePredefined<string>(asString);
            }
            else
            {
                WritePredefined<DateTime>(value);
            }
        }

        private string ConvertDateTimeToString(DateTime dateTime)
        {
            string formatted = JsonConvert.ToString(dateTime, this.DateFormatHandling, this.DateTimeZoneHandling);

            return formatted.Substring(1, formatted.Length - 2);
        }

        public bool WriteDateTimeAsString
        {
            get;
            set;
        }

        public override void WriteValue(DateTimeOffset value)
        {
            base.WriteValue(value);
            WritePredefined<DateTimeOffset>(value);
        }

        public override void WriteValue(Guid value)
        {
            base.WriteValue(value);
            WritePrimitive(new GuidMsgpackValue(mState, value));
        }

        public override void WriteValue(TimeSpan value)
        {
            base.WriteValue(value);
            WritePrimitive(new TimeSpanMsgpackValue(mState, value));
        }

        public override void WriteValue(Uri value)
        {
            base.WriteValue(value);
            WritePredefined<Uri>(value);
        }

        public override void WriteValue(object value)
        {
            if (value is BigInteger)
            {
                WriteBigInteger((BigInteger)value);
                SetWriteState(JsonToken.Integer, value);
            }
            else
            {
                base.WriteValue(value);
            }
        }

        private void WriteBigInteger(BigInteger value)
        {
            WritePrimitive(new BigIntegerMsgpackValue(mState, value));
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
            SetState(new MsgpackArray(mState));
        }

        public override void WriteEndObject()
        {
            EndState();
            base.WriteEndObject();
        }

        public override void WriteStartObject()
        {
            base.WriteStartObject();

            SetState(new MsgpackObject(mState));
        }

        private void WritePrimitive(byte[] value)
        {
            WritePrimitive(new BinaryMsgpackValue(mState, value));
        }

        private void WritePrimitive(string value)
        {
            WritePrimitive(new StringMsgpackValue(mState, value));
        }

        private void WritePrimitive<T>(MsgpackValue<T> msgpackValue)
        {
            if (mState != null)
            {
                mState.AddChild(mCurrentPropertyName, msgpackValue);
            }
            else
            {
                SetState(msgpackValue);
                EndState();
            }
        }

        private void WritePredefined<T>(T value)
        {
            WritePrimitive(new MsgpackPredefinedValue<T>(mState, value));
        }

        private void SetState(MsgpackToken token)
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
                Packer packer = Packer.Create(mStream, PackerCompatibilityOptions.None);
                mDocument.Pack(packer);
            }
        }

        private abstract class MsgpackToken
        {
            private readonly MsgpackToken mParent;

            public MsgpackToken() : this(null) 
            {
            }

            public MsgpackToken(MsgpackToken parent)
            {
                mParent = parent;
            }

            public MsgpackToken Parent
            {
                get
                {
                    return mParent;
                }
            }

            public abstract void AddChild(string propertyName, MsgpackToken child);
            public abstract void Pack(Packer packer);
        }

        private sealed class MsgpackObject : MsgpackToken
        {
            private readonly List<MsgpackProperty> mChildren = new List<MsgpackProperty>();

            public MsgpackObject()
            {
            }

            public MsgpackObject(MsgpackToken parent) : base(parent)
            {
            }

            public ICollection<MsgpackProperty> Children
            {
                get
                {
                    return mChildren;
                }
            }

            public override void AddChild(string propertyName, MsgpackToken child)
            {
                mChildren.Add(new MsgpackProperty()
                    {
                        Name = propertyName,
                        Value = child
                    });
            }

            public override void Pack(Packer packer)
            {
                Packer objectPacker = 
                    packer.PackMapHeader(mChildren.Count);

                foreach (MsgpackProperty child in mChildren)
                {
                    objectPacker.PackString(child.Name);
                    child.Value.Pack(objectPacker);
                }
            }
        }

        private sealed class MsgpackProperty
        {
            public string Name { get; set; }
            public MsgpackToken Value { get; set; }
        }

        private sealed class MsgpackArray : MsgpackToken
        {
            private readonly List<MsgpackToken> mChildren = new List<MsgpackToken>();

            public MsgpackArray()
            {
            }

            public MsgpackArray(MsgpackToken parent) : base(parent)
            {
            }

            public ICollection<MsgpackToken> Children
            {
                get
                {
                    return mChildren;
                }
            }

            public override void AddChild(string propertyName, MsgpackToken child)
            {
                mChildren.Add(child);
            }

            public override void Pack(Packer packer)
            {
                Packer arrayPacker = packer.PackArrayHeader(mChildren.Count);

                foreach (MsgpackToken child in mChildren)
                {
                    child.Pack(arrayPacker);
                }
            }
        }

        private abstract class MsgpackValue<T> : MsgpackToken
        {
            private readonly T mValue;

            public MsgpackValue(MsgpackToken parent, T value) :
                base(parent)
            {
                mValue = value;
            }

            public T Value
            {
                get
                {
                    return mValue;
                }
            }

            public override void AddChild(string propertyName, MsgpackToken child)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class MsgpackPredefinedValue<T> : MsgpackValue<T>
        {
            public MsgpackPredefinedValue(MsgpackToken parent, T value) :
                base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class SingleMsgpackValue : MsgpackValue<float>
        {
            public SingleMsgpackValue(MsgpackToken parent, float value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class DoubleMsgpackValue : MsgpackValue<double>
        {
            public DoubleMsgpackValue(MsgpackToken parent, double value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class UInt64MsgpackValue : MsgpackValue<ulong>
        {
            public UInt64MsgpackValue(MsgpackToken parent, ulong value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class Int64MsgpackValue : MsgpackValue<long>
        {
            public Int64MsgpackValue(MsgpackToken parent, long value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class UInt32MsgpackValue : MsgpackValue<uint>
        {
            public UInt32MsgpackValue(MsgpackToken parent, uint value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class Int32MsgpackValue : MsgpackValue<int>
        {
            public Int32MsgpackValue(MsgpackToken parent, int value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class UInt16MsgpackValue : MsgpackValue<ushort>
        {
            public UInt16MsgpackValue(MsgpackToken parent, ushort value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class Int16MsgpackValue : MsgpackValue<short>
        {
            public Int16MsgpackValue(MsgpackToken parent, short value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class SByteMsgpackValue : MsgpackValue<sbyte>
        {
            public SByteMsgpackValue(MsgpackToken parent, sbyte value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class ByteMsgpackValue : MsgpackValue<byte>
        {
            public ByteMsgpackValue(MsgpackToken parent, byte value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class BooleanMsgpackValue : MsgpackValue<bool>
        {
            public BooleanMsgpackValue(MsgpackToken parent, bool value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.Pack(this.Value);
            }
        }

        private sealed class NullMsgValue : MsgpackValue<object>
        {
            public NullMsgValue(MsgpackToken parent) : base(parent, null)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.PackNull();
            }
        }

        private sealed class BinaryMsgpackValue : MsgpackValue<byte[]>
        {
            public BinaryMsgpackValue(MsgpackToken parent, byte[] value) : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.PackBinary(this.Value);
            }
        }

        private sealed class StringMsgpackValue : MsgpackValue<string>
        {
            public StringMsgpackValue(MsgpackToken parent, string value) : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.PackString(this.Value);
            }
        }

        private sealed class BigIntegerMsgpackValue : MsgpackValue<BigInteger>
        {
            public BigIntegerMsgpackValue(MsgpackToken parent, BigInteger value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.PackBinary(this.Value.ToByteArray());
            }
        }

        private sealed class GuidMsgpackValue : MsgpackValue<Guid>
        {
            public GuidMsgpackValue(MsgpackToken parent, Guid value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.PackBinary(this.Value.ToByteArray());
            }
        }

        private sealed class TimeSpanMsgpackValue : MsgpackValue<TimeSpan>
        {
            public TimeSpanMsgpackValue(MsgpackToken parent, TimeSpan value)
                : base(parent, value)
            {
            }

            public override void Pack(Packer packer)
            {
                packer.PackString(this.Value.ToString());
            }
        }
    }
}