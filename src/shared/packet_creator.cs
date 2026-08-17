using System.Runtime.InteropServices;

namespace NetworkedStateMachine.Shared;

public static class PacketCreator
{
    static readonly Random rng = new();

    public static NSM_Packet RandPacket(short? nsm_id = null) => new(
            nsm_id ?? (short)rng.Next(short.MinValue, short.MaxValue),
            (short)rng.Next(short.MinValue, short.MaxValue),
            (short)rng.Next(short.MinValue, short.MaxValue),
            (short)rng.Next(short.MinValue, short.MaxValue)
    );

    public static string DebugPacksAsBytes(NSM_Packet[] ps)
    {
        ReadOnlySpan<byte> raw = MemoryMarshal.AsBytes(ps.AsSpan());
        var formattedRows = raw.ToArray().Chunk(8).Select(row => string.Join(" ", row.Select(bite => bite.ToString("b8")))).ToList();
        return string.Join("\n", formattedRows);

    }

}

