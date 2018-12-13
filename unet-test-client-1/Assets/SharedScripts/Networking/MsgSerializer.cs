using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MsgSerializer {

    public static byte[] SerializeObject(object obj, int byteSize)
    {
        byte[] buffer = new byte[byteSize];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream(buffer);
        formatter.Serialize(memStream, obj);

        return buffer;
    }

    public static NetMsg DeserializeNetMsg(byte[] netMsgBuffer)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream(netMsgBuffer);
        return (NetMsg)formatter.Deserialize(memStream);
    }
}
