using System.Runtime.InteropServices;

namespace NetworkedStateMachine.Shared;

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

            ptr += Marshal.SizeOf<Packet>();
            i++;
        }
    }

    public static string DebugPacksAsBytes(Packet[] ps)
    {
        ReadOnlySpan<byte> raw = MemoryMarshal.AsBytes(ps.AsSpan());
        var formattedRows = raw.ToArray().Chunk(8).Select(row => string.Join(" ", row.Select(bite => bite.ToString("b8")))).ToList();
        return string.Join("\n", formattedRows);

    }

}

