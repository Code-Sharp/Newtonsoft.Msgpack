﻿using System;
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
                
                if (lastReadData.IsNil)
                {
                    JsonToken state = newState ?? JsonToken.Null;
                    mReader.SetToken(state, null);
                }
                else if (lastReadData.UnderlyingType == typeof(byte[]))
                {
                    JsonToken state = newState ?? JsonToken.Bytes;
                    mReader.SetToken(state, lastReadData.AsBinary());                    
                }
                else if (lastReadData.UnderlyingType == typeof(bool))
                {
                    JsonToken state = newState ?? JsonToken.Boolean;
                    mReader.SetToken(state, lastReadData.AsBoolean());
                }
                else if (lastReadData.UnderlyingType == typeof(string))
                {
                    JsonToken state = newState ?? JsonToken.String;
                    mReader.SetToken(state, lastReadData.AsString());
                }
                else if (lastReadData.UnderlyingType == typeof(double) ||
                    lastReadData.UnderlyingType == typeof(float))
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
                DateTime? result = mDateTimeSerializer.UnpackFrom(Unpacker);
                
                mReader.SetToken(JsonToken.Date, result);
                Proceed();

                return result;
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
                if (!base.Read())
                {
                    EndState();
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
