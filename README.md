#Newtonsoft.Msgpack

[![AppVeyor Build][AppVeyorStatus]][AppVeyorLastBuild]
[![NuGet Version][NuGetPackageVersion]][NuGetPackage]

This project brings [msgpack-cli][MsgPackCliLink] to [Json.NET][JsonNetLink].

It actually contains an implementation of JsonReader/JsonWriter using msgpack-cli Unpacker/Packer classes.

##Why?

Both libraries are great libraries, but it seems that [Json.NET][JsonNetLink] is more extendable and customizable for serializing objects - i.e: there are a lot of extension points where you can change serialization behavior.

This allows you to read/write from Msgpack streams with your favorite [Json.NET][JsonNetLink] attributes and custom behaviors (null handling, JsonConverters, ContractResolvers and etc).

##Why not?

This supports only Msgpack Map serialization, I think that array serialization doesn't really fit here.

##Status

Should handle simple scenarios. If it doesn't work for you, please open an issue.

A NuGet package is [available][NuGetPackage].


##Usage

Example based on Newtonsoft.Json BSON support [sample][BsonLink] (Note: this sample works only on the custom-serializers branch)

```csharp
Product product = new Product
    {
        ExpiryDate = DateTime.Parse("2009-04-05T14:45:00Z"),
        Name = "Carlos' Spicy Wieners",
        Price = 9.95m,
        Sizes = new[] {"Small", "Medium", "Large"}
    };

MemoryStream memoryStream = new MemoryStream();
JsonSerializer serializer = new JsonSerializer();

// serialize product to MessagePack
MessagePackWriter writer = new MessagePackWriter(memoryStream);
serializer.Serialize(writer, product);

Console.WriteLine(BitConverter.ToString(memoryStream.ToArray()));

// 84-A4-4E-61-6D-65-B5-43-61-72-6C-6F-73-27-20-53-70-69-
// 63-79-20-57-69-65-6E-65-72-73-A5-50-72-69-63-65-A4-39-
// 2E-39-35-A5-53-69-7A-65-73-93-A5-53-6D-61-6C-6C-A6-4D-
// 65-64-69-75-6D-A5-4C-61-72-67-65-AA-45-78-70-69-72-79-
// 44-61-74-65-D3-00-00-01-20-76-BD-51-E0

memoryStream.Seek(0, SeekOrigin.Begin);

// deserialize product from MessagePack
MessagePackReader reader = new MessagePackReader(memoryStream);
Product deserializedProduct = serializer.Deserialize<Product>(reader);

Console.WriteLine(deserializedProduct.Name);
// Carlos' Spicy Wieners
```

##Want to help?

Contribute in any way! pull requests will be appreciated.

[MsgPackCliLink]:https://github.com/msgpack/msgpack-cli
[JsonNetLink]:https://github.com/JamesNK/Newtonsoft.Json
[BsonLink]:http://james.newtonking.com/archive/2009/12/26/json-net-3-5-release-6-binary-json-bson-support
[NuGetPackage]:https://www.nuget.org/packages/Newtonsoft.Msgpack/0.1.0
[NuGetPackageVersion]:http://img.shields.io/nuget/v/Newtonsoft.Msgpack.svg
[AppVeyorStatus]:http://img.shields.io/appveyor/ci/darkl/newtonsoft-msgpack.svg
[AppVeyorLastBuild]:https://ci.appveyor.com/project/darkl/newtonsoft-msgpack
