using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Benchmarks;

using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class ParserPerf
{
    private byte[] scratchBytes = null!;

    public static IEnumerable<int> PacketCounts =>
    [
        1,
        Core.BufMaxCount / 2,
        Core.BufMaxCount
    ];

    [ParamsSource(nameof(PacketCounts))]
    public int PacketCount { get; set; }

    [IterationCleanup(Targets = new[] { nameof(WriteInputBuff), nameof(ConsumePackets) })]
    public void Flush() => Core.Flush();

    //-----------------------------------------------------------------------

    [IterationSetup(Target = nameof(WriteInputBuff))]
    public unsafe void WriteSetup()
    {
        scratchBytes = GC.AllocateArray<byte>(PacketCount * Core.PacketSizeBytes, pinned: true);
        fixed (byte* ptr = scratchBytes) PacketCreator.CopyRandomPackets(ptr, PacketCount);
    }

    [Benchmark]
    public void WriteInputBuff() => Core.AppendInputBuf(scratchBytes, scratchBytes.Length);

    //-----------------------------------------------------------------------

    [IterationSetup(Targets = [nameof(ConsumePackets)])]
    public unsafe void PacketSetup()
    {
        Core.Flush();
        int byteCount = PacketCount * Core.PacketSizeBytes;

        scratchBytes = GC.AllocateArray<byte>(byteCount, pinned: true);
        fixed (byte* ptr = scratchBytes) PacketCreator.CopyRandomPackets(ptr, PacketCount);

        Core.AppendInputBuf(scratchBytes, byteCount);
    }

    [Benchmark]
    public void ConsumePackets() => Core.ParsePendingPackets();

    //-----------------------------------------------------------------------
}
