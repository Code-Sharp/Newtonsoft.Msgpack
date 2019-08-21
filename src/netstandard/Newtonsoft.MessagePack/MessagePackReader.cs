using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using MessagePack;
using MessagePack.Formatters;
using Newtonsoft.Json;

namespace Newtonsoft.MessagePack
{
    /// <remarks>
    /// This class is mostly based on
    /// https://github.com/neuecc/MessagePack-CSharp/blob/v1.7.3.7/src/MessagePack/Formatters/PrimitiveObjectFormatter.cs
    /// and on
    /// https://github.com/JamesNK/Newtonsoft.Json.Bson/blob/master/Src/Newtonsoft.Json.Bson/BsonDataReader.cs
    /// </remarks>>
    public class MessagePackReader : JsonReader
    {
        private readonly List<ContainerContext> mStack = new List<ContainerContext>();

        private MessagePackType mCurrentElementType;
        private ContainerContext mCurrentContext;
        private readonly byte[] mBytes;
        private int mOffset;

        public MessagePackReader(byte[] bytes)
        {
            mBytes = bytes;
        }

        private class ContainerContext
        {
            public readonly MessagePackType Type;
            public int Length;
            public int Position;

            public ContainerContext(MessagePackType type)
            {
                Type = type;
            }
        }

        private void ReadElement()
        {
            mCurrentElementType = MessagePackBinary.GetMessagePackType(mBytes, mOffset);
        }

        public override bool Read()
        {
            if (mOffset > mBytes.Length)
            {
                return false;
            }

            return ReadNormal();
        }

        public override DateTimeOffset? ReadAsDateTimeOffset()
        {
            int readSize;

            DateTimeOffset result = DateTimeOffsetFormatter.Instance.Deserialize(mBytes, mOffset, null, out readSize);
            mOffset += readSize;

            this.SetToken(JsonToken.Date, result);

            return result;
        }

        private bool ReadNormal()
        {
            switch (CurrentState)
            {
                case State.Start:
                {
                    ReadElement();
                    ReadType(mCurrentElementType);
                    return true;
                }

                case State.Complete:
                case State.Closed:
                    return false;
                case State.Property:
                {
                    ContainerContext context = mCurrentContext;
                    ReadElement();
                    ReadType(mCurrentElementType);
                    context.Position++;
                    return true;
                }

                case State.ObjectStart:
                case State.ArrayStart:
                case State.PostValue:
                {
                    ContainerContext context = mCurrentContext;
                    if (context == null)
                    {
                        return false;
                    }

                    if (context.Position <= context.Length - 1)
                    {
                        if (context.Type == MessagePackType.Array)
                        {
                            ReadElement();
                            ReadType(mCurrentElementType);
                            context.Position++;
                        }
                        else
                        {
                            SetToken(JsonToken.PropertyName, ReadString());
                        }
                    }
                    else if (context.Position == context.Length)
                    {
                        PopContext();

                        JsonToken endToken = (context.Type == MessagePackType.Map)
                            ? JsonToken.EndObject
                            : JsonToken.EndArray;

                        SetToken(endToken);
                    }
                    else
                    {
                        throw new JsonReaderException("Read past end of current container context.");
                    }

                    return true;
                }

                case State.ConstructorStart:
                    break;
                case State.Constructor:
                    break;
                case State.Error:
                    break;
                case State.Finished:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        private object ReadString()
        {
            int readSize;
            string str = MessagePackBinary.ReadString(mBytes, mOffset, out readSize);
            mOffset += readSize;
            return str;
        }

        private void PopContext()
        {
            mStack.RemoveAt(mStack.Count - 1);
            if (mStack.Count == 0)
            {
                mCurrentContext = null;
            }
            else
            {
                mCurrentContext = mStack[mStack.Count - 1];
            }
        }

        private void PushContext(ContainerContext newContext)
        {
            mStack.Add(newContext);
            mCurrentContext = newContext;
        }

        private void ReadType(MessagePackType type)
        {
            int readSize = 0;

            switch (type)
            {
                case MessagePackType.Float:
                {
                    double d;

                    if (MessagePackCode.Float32 == mBytes[mOffset])
                    {
                        d = MessagePackBinary.ReadSingle(mBytes, mOffset, out readSize);
                    }
                    else
                    {
                        d = MessagePackBinary.ReadDouble(mBytes, mOffset, out readSize);
                    }

                    if (FloatParseHandling == FloatParseHandling.Decimal)
                    {
                        SetToken(JsonToken.Float, Convert.ToDecimal(d, CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        SetToken(JsonToken.Float, d);
                    }

                    break;
                }

                case MessagePackType.String:
                {
                    string str = MessagePackBinary.ReadString(mBytes, mOffset, out readSize);
                    SetToken(JsonToken.String, str);
                    break;
                }

                case MessagePackType.Map:
                {
                    SetToken(JsonToken.StartObject);

                    ContainerContext newContext = new ContainerContext(MessagePackType.Map);
                    PushContext(newContext);
                    newContext.Length = MessagePackBinary.ReadMapHeader(mBytes, mOffset, out readSize);
                    break;
                }

                case MessagePackType.Array:
                {
                    SetToken(JsonToken.StartArray);

                    ContainerContext newContext = new ContainerContext(MessagePackType.Array);
                    PushContext(newContext);
                    newContext.Length = MessagePackBinary.ReadArrayHeader(mBytes, mOffset, out readSize);
                    break;
                }

                case MessagePackType.Binary:
                    byte[] data = MessagePackBinary.ReadBytes(mBytes, mOffset, out readSize);

                    SetToken(JsonToken.Bytes, data);
                    break;
                case MessagePackType.Unknown:
                    SetToken(JsonToken.Undefined);
                    break;
                case MessagePackType.Boolean:
                    bool b = MessagePackBinary.ReadBoolean(mBytes, mOffset, out readSize);
                    SetToken(JsonToken.Boolean, b);
                    break;
                case MessagePackType.Extension:
                    ExtensionHeader ext = MessagePackBinary.ReadExtensionFormatHeader(mBytes, mOffset, out readSize);

                    if (ext.TypeCode != ReservedMessagePackExtensionTypeCode.DateTime)
                    {
                        throw new InvalidOperationException("Invalid primitive bytes.");
                    }

                    DateTime dateTime = MessagePackBinary.ReadDateTime(mBytes, mOffset, out readSize);
                    SetToken(JsonToken.Date, dateTime);
                    break;
                case MessagePackType.Nil:
                    MessagePackBinary.ReadNil(mBytes, mOffset, out readSize);
                    SetToken(JsonToken.Null);
                    break;
                case MessagePackType.Integer:
                    object integer = ReadInteger(mBytes, mOffset, out readSize);
                    SetToken(JsonToken.Integer, integer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), "Unexpected MessagePackType value: " + type);
            }

            mOffset += readSize;
        }

        private static object ReadInteger(byte[] bytes, int offset, out int readSize)
        {
            var code = bytes[offset];

            if (MessagePackCode.MinNegativeFixInt <= code && code <= MessagePackCode.MaxNegativeFixInt)
                return MessagePackBinary.ReadSByte(bytes, offset, out readSize);
            else if (MessagePackCode.MinFixInt <= code && code <= MessagePackCode.MaxFixInt)
                return MessagePackBinary.ReadByte(bytes, offset, out readSize);
            else if (code == MessagePackCode.Int8)
                return MessagePackBinary.ReadSByte(bytes, offset, out readSize);
            else if (code == MessagePackCode.Int16)
                return MessagePackBinary.ReadInt16(bytes, offset, out readSize);
            else if (code == MessagePackCode.Int32)
                return MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            else if (code == MessagePackCode.Int64)
                return MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            else if (code == MessagePackCode.UInt8)
                return MessagePackBinary.ReadByte(bytes, offset, out readSize);
            else if (code == MessagePackCode.UInt16)
                return MessagePackBinary.ReadUInt16(bytes, offset, out readSize);
            else if (code == MessagePackCode.UInt32)
                return MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
            else if (code == MessagePackCode.UInt64)
                return MessagePackBinary.ReadUInt64(bytes, offset, out readSize);
            throw new InvalidOperationException("Invalid primitive bytes.");
        }
    }
}