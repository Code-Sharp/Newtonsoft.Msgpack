using System;
using System.IO;
using MsgPack;
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

            public State(MessagePackReader reader, Unpacker unpacker, State previousState)
            {
                mReader = reader;
                mUnpacker = unpacker;
                mPreviousState = previousState;
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
                    ReadPrimitive(false);
                }
            }

            protected void ReadPrimitive(bool isProperty)
            {
                MessagePackObject lastReadData = mUnpacker.LastReadData;
                JsonToken? newState = null;

                if (isProperty)
                {
                    newState = JsonToken.PropertyName;
                }
                else if (lastReadData.IsNil)
                {
                    JsonToken state = newState ?? JsonToken.Null;
                    mReader.SetToken(state, null);
                }
                else if (lastReadData.IsTypeOf<byte[]>() == true)
                {
                    JsonToken state = newState ?? JsonToken.Bytes;
                    mReader.SetToken(state, lastReadData.AsBinary());                    
                }
                else if (lastReadData.IsTypeOf<bool>() == true)
                {
                    JsonToken state = newState ?? JsonToken.Boolean;
                    mReader.SetToken(state, lastReadData.AsBoolean());
                }
                if (lastReadData.IsTypeOf<string>() == true)
                {
                    JsonToken state = newState ?? JsonToken.String;
                    mReader.SetToken(state, lastReadData.AsString());
                }
                else if (lastReadData.IsTypeOf<double>() == true || 
                    lastReadData.IsTypeOf<float>() == true)
                {
                    JsonToken state = newState ?? JsonToken.Float;
                    mReader.SetToken(state, lastReadData.ToObject());
                }
                else if (lastReadData.IsTypeOf<sbyte>() == true || 
                    lastReadData.IsTypeOf<short>() == true || 
                    lastReadData.IsTypeOf<ushort>() == true || 
                    lastReadData.IsTypeOf<int>() == true || 
                    lastReadData.IsTypeOf<uint>() == true || 
                    lastReadData.IsTypeOf<long>() == true || 
                    lastReadData.IsTypeOf<ulong>() == true)
                {
                    JsonToken state = newState ?? JsonToken.Integer;
                    mReader.SetToken(state, lastReadData.ToObject());
                }
            }

            protected bool EndState(JsonToken state)
            {
                mReader.SetToken(state);
                mReader.mState = this.mPreviousState;
                mUnpacker.Dispose();
                return true;
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

                return null;
            }

            public string ReadAsString()
            {
                string result;

                if (Unpacker.ReadString(out result))
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

                if (Unpacker.ReadBinary(out result))
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
                double? doubleResult = null;
                float? floatResult = null;
                ulong? ulongResult = null;
                long? longResult = null;
                uint? uintResult = null;
                int? intResult = null;
                sbyte? sbyteResult = null;
                byte? byteResult = null;
                ushort? ushortResult = null;
                short? shortResult = null;

                if (Unpacker.ReadNullableDouble(out doubleResult))
                {
                    hasResult = true;

                    if (doubleResult != null)
                    {
                        result = new decimal(doubleResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableSingle(out floatResult))
                {
                    hasResult = true;

                    if (floatResult != null)
                    {
                        result = new decimal(floatResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableUInt64(out ulongResult))
                {
                    hasResult = true;

                    if (ulongResult != null)
                    {
                        result = new decimal(ulongResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableInt64(out longResult))
                {
                    hasResult = true;

                    if (longResult != null)
                    {
                        result = new decimal(longResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableUInt32(out uintResult))
                {
                    hasResult = true;

                    if (uintResult != null)
                    {
                        result = new decimal(uintResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableInt32(out intResult))
                {
                    hasResult = true;

                    if (intResult != null)
                    {
                        result = new decimal(intResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableSByte(out sbyteResult))
                {
                    hasResult = true;

                    if (sbyteResult != null)
                    {
                        result = new decimal(sbyteResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableByte(out byteResult))
                {
                    hasResult = true;
                    
                    if (byteResult != null)
                    {
                        result = new decimal(byteResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableUInt16(out ushortResult))
                {
                    hasResult = true;
                    
                    if (ushortResult != null)
                    {
                        result = new decimal(ushortResult.Value);
                    }
                }
                else if (Unpacker.ReadNullableInt16(out shortResult))
                {
                    hasResult = true;
                    
                    if (shortResult != null)
                    {
                        result = new decimal(shortResult.Value);
                    }
                }

                if (hasResult)
                {
                    mReader.SetToken(JsonToken.Float, result);
                    Proceed();
                }

                return result;
            }

            // Not supported by message pack.
            public DateTime? ReadAsDateTime()
            {
                return null;
            }

            public DateTimeOffset? ReadAsDateTimeOffset()
            {
                return null;
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
                    return EndState(JsonToken.EndArray);
                }
                else
                {
                    ReadValue();
                    return true;
                }
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
                if (!base.Read())
                {
                    EndState(JsonToken.EndObject);
                    return true;
                }
                else
                {
                    if (mKeyValueType == KeyValueType.Key)
                    {
                        ReadPrimitive(true);
                        mKeyValueType = KeyValueType.Value;
                    }
                    else
                    {
                        ReadValue();
                        mKeyValueType = KeyValueType.Key;
                    }

                    return true;
                }
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
