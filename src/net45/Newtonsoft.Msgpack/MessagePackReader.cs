using System;
using System.IO;
using MsgPack;
using MsgPack.Serialization;
using Newtonsoft.Json;

namespace Newtonsoft.Msgpack
{
    public class MessagePackReader : JsonReader
    {
        private bool mFirstRead = true;
        private State mState;

        public MessagePackReader(Stream stream)
        {
            Unpacker unpacker = Unpacker.Create(stream);
            mState = new State(this, unpacker, null);
        }

        private Unpacker Unpacker
        {
            get
            {
                return mState.Unpacker;
            }
        }

        public override bool Read()
        {
            if (mState == null)
            {
                return false;
            }

            bool result = mState.Read();

            if (mFirstRead)
            {
                mState.ReadValue();
                mFirstRead = false;
            }
            
            return result;
        }

        #region Overriden

        public override int? ReadAsInt32()
        {
            return mState.ReadAsInt32();
        }

        public override string ReadAsString()
        {
            return mState.ReadAsString();
        }

        public override decimal? ReadAsDecimal()
        {
            return mState.ReadAsDecimal();
        }

        public override byte[] ReadAsBytes()
        {
            return mState.ReadAsBytes();
        }

        public override DateTimeOffset? ReadAsDateTimeOffset()
        {
            return mState.ReadAsDateTimeOffset();
        }

        public override DateTime? ReadAsDateTime()
        {
            return mState.ReadAsDateTime();
        }

        #endregion

        private class State
        {
            protected readonly MessagePackReader mReader;

            protected Unpacker mUnpacker;

            protected readonly State mPreviousState;
            private readonly MessagePackSerializer<DateTime?> mDateTimeSerializer;
            private readonly MessagePackSerializer<DateTimeOffset> mDateTimeOffsetSerializer;

            public State(MessagePackReader reader, Unpacker unpacker, State previousState)
            {
                mReader = reader;
                mUnpacker = unpacker;
                mPreviousState = previousState;
                mDateTimeSerializer = SerializationContext.Default.GetSerializer<DateTime?>();
                mDateTimeOffsetSerializer = SerializationContext.Default.GetSerializer<DateTimeOffset>();
            }

            public Unpacker Unpacker
            {
                get { return mUnpacker; }
            }

            public virtual bool Read()
            {
                return mUnpacker.Read();
            }

            public void ReadValue()
            {
                if (mUnpacker.IsMapHeader)
                {
                    mReader.SetToken(JsonToken.StartObject);
                    mReader.mState = new MapState(mReader, mUnpacker.ReadSubtree(), this);
                }
                else if (mUnpacker.IsArrayHeader)
                {
                    mReader.SetToken(JsonToken.StartArray);
                    mReader.mState = new ArrayState(mReader, mUnpacker.ReadSubtree(), this);
                }
                else
                {
                    ReadPrimitive();
                }
            }

            protected void ReadPrimitive()
            {
                MessagePackObject lastReadData = mUnpacker.LastReadData;

                if (lastReadData.IsNil)
                {
                    mReader.SetToken(JsonToken.Null, null);
                }
                else if (lastReadData.UnderlyingType == typeof(byte[]))
                {
                    mReader.SetToken(JsonToken.Bytes, lastReadData.AsBinary());                    
                }
                else if (lastReadData.UnderlyingType == typeof(bool))
                {
                    mReader.SetToken(JsonToken.Boolean, lastReadData.AsBoolean());
                }
                else if (lastReadData.UnderlyingType == typeof(string))
                {
                    mReader.SetToken(JsonToken.String, lastReadData.AsString());
                }
                else if (lastReadData.UnderlyingType == typeof(double) ||
                    lastReadData.UnderlyingType == typeof(float))
                {
                    mReader.SetToken(JsonToken.Float, lastReadData.ToObject());
                }
                else if (lastReadData.IsTypeOf<sbyte>() == true || 
                    lastReadData.IsTypeOf<short>() == true || 
                    lastReadData.IsTypeOf<ushort>() == true || 
                    lastReadData.IsTypeOf<int>() == true || 
                    lastReadData.IsTypeOf<uint>() == true || 
                    lastReadData.IsTypeOf<long>() == true || 
                    lastReadData.IsTypeOf<ulong>() == true)
                {
                    mReader.SetToken(JsonToken.Integer, lastReadData.ToObject());
                }
            }

            protected void EndState(JsonToken state)
            {
                mReader.SetToken(state);
                mReader.mState = this.mPreviousState;
                mUnpacker.Dispose();
            }

            public int? ReadAsInt32()
            {
                int? result;

                if (Unpacker.ReadNullableInt32(out result))
                {
                    Proceed();
                    mReader.SetToken(JsonToken.Integer, result);
                    return result;
                }
                else
                {
                    EndState();
                }

                return null;
            }

            protected virtual void EndState()
            {
            }

            public string ReadAsString()
            {
                string result;

                if (!Unpacker.ReadString(out result))
                {
                    EndState();
                }
                else
                {
                    Proceed();
                    mReader.SetToken(JsonToken.String, result);
                    return result;
                }

                return null;
            }

            public byte[] ReadAsBytes()
            {
                byte[] result;

                if (!Unpacker.ReadBinary(out result))
                {
                    EndState();
                }
                else
                {
                    Proceed();
                    mReader.SetToken(JsonToken.Bytes, result);
                    return result;
                }

                return null;
            }

            public decimal? ReadAsDecimal()
            {
                bool hasResult = false;
                decimal? result = null;

                if (!Unpacker.Read())
                {
                    EndState();
                }
                else
                {
                    try
                    {
                        result =
                            SerializationContext.Default.GetSerializer<decimal?>()
                                                .UnpackFrom(mUnpacker);

                        hasResult = true;
                    }
                    catch (Exception)
                    {
                    }
                }

                if (hasResult)
                {
                    mReader.SetToken(JsonToken.Float, result);
                    Proceed();
                }

                return result;
            }

            public DateTime? ReadAsDateTime()
            {
                Unpacker.Read();

                DateTime? result;

                if (Unpacker.LastReadData.IsTypeOf<string>() == true)
                {
                    result = ParseDateTimeString(Unpacker.LastReadData.AsString());
                }
                else
                {
                    result = Unpacker.Unpack<DateTime?>();
                }

                mReader.SetToken(JsonToken.Date, result);
                Proceed();

                return result;
            }

            private DateTime? ParseDateTimeString(string readDateTime)
            {
                return JsonConvert.DeserializeObject<DateTime>(@"""" + readDateTime + @"""");
            }

            public DateTimeOffset? ReadAsDateTimeOffset()
            {
                Unpacker.Read();
                DateTimeOffset? result = mDateTimeOffsetSerializer.UnpackFrom(Unpacker);

                mReader.SetToken(JsonToken.Date, result);
                Proceed();

                return result;
            }

            protected virtual void Proceed()
            {
            }
        }

        private class ArrayState : State
        {
            public ArrayState(MessagePackReader reader, Unpacker unpacker, State previousState) :
                base(reader, unpacker, previousState)
            {
            }

            public override bool Read()
            {
                if (!base.Read())
                {
                    EndState();
                }
                else
                {
                    ReadValue();
                }

                return true;
            }

            protected override void EndState()
            {
                EndState(JsonToken.EndArray);
            }
        }

        private class MapState : State
        {
            private KeyValueType mKeyValueType = KeyValueType.Key;

            public MapState(MessagePackReader reader, Unpacker unpacker, State state)
                : base(reader, unpacker, state)
            {
            }

            public override bool Read()
            {
                if (mKeyValueType == KeyValueType.Key)
                {
                    bool readKey = ReadKey();
                    return true;
                }

                if (!base.Read())
                {
                    EndState();
                    return true;
                }
                else
                {
                    ReadValue();
                    mKeyValueType = KeyValueType.Key;
                }

                return true;
            }

            private bool ReadKey()
            {
                bool result = TryReadPropertyName();

                if (result)
                {
                    mKeyValueType = KeyValueType.Value;
                }
                else
                {
                    EndState();
                }

                return result;
            }

            private bool TryReadPropertyName()
            {
                string value;
                bool readSuccess = mUnpacker.ReadString(out value);

                if (readSuccess)
                {
                    mReader.SetToken(JsonToken.PropertyName, value);                    
                }

                return readSuccess;
            }

            protected override void EndState()
            {
                EndState(JsonToken.EndObject);
            }

            protected override void Proceed()
            {
                base.Proceed();

                if (mKeyValueType == KeyValueType.Key)
                {
                    mKeyValueType = KeyValueType.Value;
                }
                else
                {
                    mKeyValueType = KeyValueType.Key;
                }
            }

            private enum KeyValueType
            {
                Key,
                Value
            }
        }
    }
}
