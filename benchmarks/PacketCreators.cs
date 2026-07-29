using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkedStateMachine.Server;

namespace NetworkedStateMachine.Benchmarks;


public static class PacketCreator
{
    static readonly Random rng = new();

    public static Packet RandPacket() => new(
            (short)rng.Next(short.MinValue, short.MaxValue),
            (short)rng.Next(short.MinValue, short.MaxValue),
            (short)rng.Next(short.MinValue, short.MaxValue)
    );

    public unsafe static void CopyRandomPackets(byte* ptr, int count)
    {
        int i = 0;
        while (i < count)
        {
            Marshal.StructureToPtr(RandPacket(), (nint)ptr, true);

            ptr += Core.PacketSizeBytes;
            i++;
        }
    }

}

