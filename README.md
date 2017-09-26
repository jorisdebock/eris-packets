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
writer.WriteUInt64(1); // 01 00 00 00 00 00

writer.WriteAscii("text"); // 04 00 74 65 78 74 (2 bytes length + 4 bytes text)
```

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
var result = reader.ReadUInt64(); // 01 00 00 00 00 00 => ulong 1

var result = writer.ReadAscii(); // 04 00 74 65 78 74 (2 bytes length + 4 bytes text) => "text"
```


see /test for more
