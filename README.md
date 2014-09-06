Newonsoft.Msgpack
=================

This project brings [msgpack-cli][MsgPackCliLink] to [Json.NET][JsonNetLink].

It actually contains an implementation of JsonReader/JsonWriter using msgpack-cli Unpacker/Packer classes.

Why?
=================

Both libraries are great libraries, but it seems that [Json.NET][JsonNetLink] is more extendable and customizable for serializing objects - i.e: there are a lot of extension points where you can change serialization behavior.

This allows you to read/write from Msgpack streams with your favorite [Json.NET][JsonNetLink] attributes and custom behaviors (null handling, JsonConverters, ContractResolvers and etc).

Why not?
=================

This supports only Msgpack Map serialization, I think that array serialization doesn't really fit here.


Usage
=================

Currently only JsonReader is implemented, but JsonWriter will be implemented soon.


```csharp
using (MemoryStream stream = new MemoryStream(bytes))
{
    using (MessagePackReader reader = new MessagePackReader(stream))
    {
        JsonSerializer jsonSerializer = new JsonSerializer();

        Person person =
            jsonSerializer.Deserialize<Person>(reader);
    }                
}
```

Want to help?
=================

Contribute in any way! pull requests will be appriciated.

[MsgPackCliLink]:https://github.com/msgpack/msgpack-cli
[JsonNetLink]:https://github.com/JamesNK/Newtonsoft.Json
