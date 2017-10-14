# Eris packets

[![](https://img.shields.io/nuget/v/Eris.Packets.svg)](https://www.nuget.org/packages/Eris.Packets)
[![](https://joris.visualstudio.com/_apis/public/build/definitions/b5bf31cd-d10a-4ddb-afc6-e9746c2c9c31/12/badge)](https://github.com/wazowsk1/eris-packets)

A PacketWriter and PacketReader to easily create and read data packets (byte[])

## PacketWriter 

Basic usage:
```c#
using (var writer = new PacketWriter())
{
    writer.WriteUInt8(1);

    var result = writer.GetBytes();
    
    // results in:
    // 01 
}
```

More functions:
```c#
writer.WriteUInt8(1);  // 01
writer.WriteUInt16(1); // 01 00
writer.WriteUInt32(1); // 01 00 00 00
writer.WriteUInt64(1); // 01 00 00 00 00 00 00 00

writer.WriteAscii("text"); // 04 00 74 65 78 74 (2 bytes length + 4 bytes text)
```
see /test for more.

## PacketReader

Basic usage:
```c#
using (var reader = new PacketReader(new byte[] { 1 }))
{
    var result = reader.ReadUInt8();
    
    // results in:
    // 01    
}
```


More functions:
```c#
var result = reader.ReadUInt8();  // 01 => byte 1
var result = reader.ReadUInt16(); // 01 00 => ushort 1
var result = reader.ReadUInt32(); // 01 00 00 00 => uint 1
var result = reader.ReadUInt64(); // 01 00 00 00 00 00 00 00=> ulong 1

var result = writer.ReadAscii(); // 04 00 74 65 78 74 (2 bytes length + 4 bytes text) => "text"
```

see /test for more.


The reader also has the capability to collect the read actions (opt-in). With this the data read can be easily visualized and traced back, especially handy when there is a more complex reading logic required.

The PacketReadActions contains the data used (byte[]) and a list of actions, Each action contains the amount of bytes read and a custom message


```c#
using (var reader = new PacketReader(new byte[] { 0x01, 0x04 0x00 0x74 0x65 0x78 0x74 }, collectReadActions: true))
{
    var result = reader.ReadUInt8("This is a message");
    var result = reader.ReadAscii("Reading a ascii string");
    
    var packetReadActions = reader.GetPacketReadActions();

    // the actions will contain a list of the following data
    // count, message
    // 1, "UInt8: this is a message"
    // 2, "Ascii length (4): Reading a ascii string"
    // 4, "Ascii ("text"): Reading a ascii string"    
}
```


When a read action is executed that can not be executed, like trying to read more data then the input that is given. A PacketReadException will be thrown with the message what caused it.

```c#
using (var reader = new PacketReader(new byte[] { 0x01, 0x04 0x00 }, collectReadActions: true))
{
    try
    {
        var result = reader.ReadUInt8("This is a message");

        // will cause a PacketReadException because it will try to read past the end of the data
        var result = reader.ReadAscii("Reading a ascii string");
    }
    catch(PacketReadException e)
    {
        // collect information
        var message = e.Message;    
        var packetReadActions = reader.GetPacketReadActions();
    }   
}
```
