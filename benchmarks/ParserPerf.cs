using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Benchmarks;

using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class ParserPerf
{
    Parser parser = new(); 
    private byte[] scratchBytes = null!;

    [ParamsSource(nameof(PacketCounts))]
    public int PacketCount { get; set; }
    public static IEnumerable<int> PacketCounts => [ 1, Parser.BufMaxCount / 2, Parser.BufMaxCount ];

    //-----------------------------------------------------------------------

    [IterationSetup(Target = nameof(WriteInputBuff))]
    public unsafe void WriteSetup()
    {
        parser = new();
        scratchBytes = GC.AllocateArray<byte>(PacketCount * Parser.PacketSizeBytes, pinned: true);
        fixed (byte* ptr = scratchBytes) PacketCreator.CopyRandomPackets(ptr, PacketCount);
    }

    [Benchmark]
    public void WriteInputBuff() => parser.AppendInputBuf(scratchBytes, scratchBytes.Length);

    //-----------------------------------------------------------------------

    [IterationSetup(Targets = [nameof(ConsumePackets)])]
    public unsafe void PacketSetup()
    {
        parser = new();
        int byteCount = PacketCount * Parser.PacketSizeBytes;

        scratchBytes = GC.AllocateArray<byte>(byteCount, pinned: true);
        fixed (byte* ptr = scratchBytes) PacketCreator.CopyRandomPackets(ptr, PacketCount);

        parser.AppendInputBuf(scratchBytes, byteCount);
    }

    [Benchmark]
    public void ConsumePackets() => parser.ParsePendingPackets();

    //-----------------------------------------------------------------------
}
