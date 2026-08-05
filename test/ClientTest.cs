using System.Runtime.InteropServices;
using NetworkedStateMachine.Client;
using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;
namespace NetworkedStateMachine.Test;


public class ClientTest
{
    [Fact]
    public void SendPacketThroughClient()
    {
        Packet recPack;
        Action<ReadOnlySpan<byte>>[] lsss = [
            bees => {
                bees.ToArray().Chunk(8).Select(row => string.Join(" ", row.Select(bite => bite.ToString("b")))).ToList().ForEach(Console.WriteLine);
                Console.WriteLine("----------------------------------------------------------------------------");
            }
        ];

        var t = new LocalTransporter(lsss);
        var wow = new NSM_Client(t);

        Packet[] hi = [.. Enumerable.Range(1, 1).Select(x => PacketCreator.RandPacket())];

        wow.Send(hi.AsSpan(0, 1));

        ReadOnlySpan<byte> ok = MemoryMarshal.AsBytes(Core.ParsedPacks.AsSpan(0, 1));
        ok.ToArray().Chunk(8).Select(row => string.Join(" ", row.Select(bite => bite.ToString("b")))).ToList().ForEach(Console.WriteLine);

        recPack = MemoryMarshal.Read<Packet>(ok);

        Assert.Equal(recPack, hi[0]);

    }

}
