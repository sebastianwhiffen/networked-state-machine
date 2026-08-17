using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Benchmarks;

using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class ParserPerf
{
    Parser parser = new();
    private byte[] scratchBytes = null!;

    [ParamsSource(nameof(PacketCounts))]
    public int PacketCount { get; set; }
    public static IEnumerable<int> PacketCounts => [1, Parser.BufMaxCount / 2, Parser.BufMaxCount];

    //-----------------------------------------------------------------------

    [IterationSetup(Target = nameof(WriteInputBuff))]
    public unsafe void WriteSetup()
    {
        parser = new();
        scratchBytes = GC.AllocateArray<byte>(PacketCount * Parser.PacketSizeBytes, pinned: true);
        fixed (byte* ptr = scratchBytes) CopyRandomPackets(ptr, PacketCount);
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
        fixed (byte* ptr = scratchBytes) CopyRandomPackets(ptr, PacketCount);

        parser.AppendInputBuf(scratchBytes, byteCount);
    }

    [Benchmark]
    public void ConsumePackets() => parser.ParsePendingPackets();

    //-----------------------------------------------------------------------

    public unsafe static void CopyRandomPackets(byte* ptr, int count)
    {
        int i = 0;
        while (i < count)
        {
            Marshal.StructureToPtr(PacketCreator.RandPacket(), (nint)ptr, true);

            ptr += Marshal.SizeOf<NSM_Packet>();
            i++;
        }
    }
}
