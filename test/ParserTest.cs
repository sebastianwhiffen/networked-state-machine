using System.Runtime.InteropServices;
using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Test;

public class ParserTest
{
    [Fact]
    public void ValidParseTest()
    {
        Packet[] expectedPackets = [.. Enumerable.Range(1, 100).Select(x => PacketCreator.RandPacket())];

        byte[] bytes = MemoryMarshal.AsBytes(expectedPackets.AsSpan()).ToArray();

        Core.AppendInputBuf(bytes, bytes.Length);
        Core.ParsePendingPackets();

        Packet[] actualPackets = new Packet[expectedPackets.Length];
        for (int i = 0; i < actualPackets.Length; i++)
        {
            actualPackets[i] = Core.ParsedPacks[i];
        }

        Assert.Equal(expectedPackets, actualPackets);
    }
}
