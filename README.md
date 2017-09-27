# Eris packets

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

    var data = packetReadActions.Data;
    // The data will contain the original input data
    // 0x01, 0x04 0x00 0x74 0x65 0x78 0x74

    var actions = packetReadActions.Actions;

    // the actions will contain a list of the following data
    // bytes, message
    // 1, "UInt8: this is a message"
    // 2, "Ascii length (4): Reading a ascii string"
    // 4, "Ascii (\"text\"): Reading a ascii string"
    
}
```

When a read action is executed that can not be executed, like trying to read more data then the input that is given. A PacketException will be thrown which will include the error message and the PacketReadActions if the collectReadActions is turned on.
