
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using NetworkedStateMachine.Server;

namespace NetworkedStateMachine.Benchmarks;

[MemoryDiagnoser]
public class ParserPerf
{

    [IterationCleanup]
    public void Setup()
    {
        Core.Flush();
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestByteArrays))]
    public void WriteInputBuff(byte[] bytes) =>
        Core.AppendInputBuf(bytes, bytes.Length);

    public static byte[][] TestByteArrays => [
        "ok"u8.ToArray(),
        "bigger"u8.ToArray(),
        "really_big_hiiiiiiiiiiiii_wow_this_is_big!"u8.ToArray()
    ];
    
    //set up with more realistic data
    [IterationSetup(Targets = new[] { nameof(ConsumePackets) })]
    public unsafe void PacketSetup()
    {
        int len = Marshal.SizeOf<Packet>();
        byte[] arr = new byte[len];

        var ptr = (byte*)Unsafe.AsPointer(ref arr[0]);
        Marshal.StructureToPtr(new Packet(255), (nint)ptr, true);
        Core.AppendInputBuf(arr, arr.Length);
    }

    [Benchmark]
    public void ConsumePackets() => Core.ParsePendingPackets();
}
