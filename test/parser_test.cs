using System.Runtime.InteropServices;
using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Test;

public class ParserTest
{
    [Fact]
    public void ValidParseTest()
    {
        Packet[] expectedPackets = [.. Enumerable.Range(0, 100).Select(x => PacketCreator.RandPacket())];

        var bytes = MemoryMarshal.AsBytes(expectedPackets.AsSpan());

        Parser parser = new();

        parser.AppendInputBuf(bytes, bytes.Length);
        parser.Tick();

        Packet[] actualPackets = new Packet[expectedPackets.Length];
        for (int i = 0; i < actualPackets.Length; i++)
        {
            actualPackets[i] = parser.ParsedPacks[i];
        }

        Assert.True(expectedPackets.SequenceEqual(actualPackets),
                $"\n\noriginal: {PacketCreator.DebugPacksAsBytes(expectedPackets)}" +
                $"\n\noutput: {PacketCreator.DebugPacksAsBytes(actualPackets)}");
    }
}
