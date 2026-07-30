using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Server;

public static class Core
{
    public const int BufMaxCount = 1024;

    public static readonly int PacketSizeBytes = 0;

    public static readonly Packet[] ParsedPacks = new Packet[BufMaxCount];
    static int ParsedPackWriteHead = 0;
    static int ParsedPackReadHead = 0;

    readonly static byte[] InputBuf = null;
    static int InputBufBytesToRead = 0;
    static int InputBufWriteHead = 0;
    static int InputBufReadHead = 0;

    static Core()
    {
        PacketSizeBytes = Marshal.SizeOf<Packet>();
        InputBuf = GC.AllocateArray<byte>(PacketSizeBytes * BufMaxCount, pinned: true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Flush()
    {
        ParsedPackWriteHead = 0;
        ParsedPackReadHead = 0;
        InputBufBytesToRead = 0;
        InputBufWriteHead = 0; InputBufReadHead = 0;
    }

    public static void Tick()
    {
        ParsePendingPackets();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public void AppendInputBuf(byte[] bytes, int byteCount)
    {
        //We should always be consuming/flushing this buffer faster than we can fill it up.
        //if this fills up too often then double the array size.
        if (byteCount + InputBufWriteHead > BufMaxCount * PacketSizeBytes)
        {
            throw new IndexOutOfRangeException($"Input buffer Overflow: inputs not being processed in time between frames. byteCount :{byteCount}, writeHead{InputBufWriteHead}, greater than {BufMaxCount * PacketSizeBytes}");
        }

        Array.Copy(bytes, 0, InputBuf, InputBufWriteHead, byteCount);

        InputBufWriteHead += byteCount;
        InputBufBytesToRead += byteCount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ParsePendingPackets()
    {
        ReadOnlySpan<byte> slice = InputBuf.AsSpan(InputBufReadHead, InputBufBytesToRead);
        ReadOnlySpan<Packet> packs = MemoryMarshal.Cast<byte, Packet>(slice);

        packs.CopyTo(ParsedPacks.AsSpan(ParsedPackWriteHead, packs.Length));
        ParsedPackWriteHead += packs.Length;
    }
}





