using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Server;

public class Parser
{
    public const int BufMaxCount = 1024;

    public static readonly int PacketSizeBytes = Unsafe.SizeOf<NSM_Packet>();

    public readonly NSM_Packet[] ParsedPacks = new NSM_Packet[BufMaxCount];
    int ParsedPackWriteHead = 0;

    readonly byte[] InputBuf = null;
    int InputBufWriteHead = 0;
    int InputBufReadHead = 0;

    public Parser()
    {
        InputBuf = GC.AllocateArray<byte>(PacketSizeBytes * BufMaxCount, pinned: true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Flush()
    {
        ParsedPackWriteHead = 0;

        InputBufWriteHead = 0;
        InputBufReadHead = 0;
    }

    public void Tick()
    {
        ParsePendingPackets();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendInputBuf(ReadOnlySpan<byte> bytes, int byteCount)
    {
        //We should always be consuming/flushing this buffer faster than we can fill it up.
        //if this fills up too often then double the array size.
        //otherwise it should be considered tantamount to ddos. root cause typeshit
        if (byteCount + InputBufWriteHead > BufMaxCount * PacketSizeBytes)
        {
            throw new IndexOutOfRangeException(
                    $"Input buffer Overflow:\n" +
                    $"inputs not being processed in time between frames. plix flix \n" +
                    $"byteCount :{byteCount}, writeHead{InputBufWriteHead}, greater than {BufMaxCount * PacketSizeBytes}\n"
            );
        }

        bytes[..byteCount].CopyTo(InputBuf.AsSpan(InputBufWriteHead, byteCount));

        InputBufWriteHead += byteCount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParsePendingPackets()
    {
        ReadOnlySpan<byte> slice = InputBuf.AsSpan(InputBufReadHead, InputBufWriteHead);
        ReadOnlySpan<NSM_Packet> packs = MemoryMarshal.Cast<byte, NSM_Packet>(slice);

        packs.CopyTo(ParsedPacks.AsSpan(ParsedPackWriteHead, packs.Length));

        ParsedPackWriteHead += packs.Length;
        InputBufReadHead += packs.Length;
    }
}





