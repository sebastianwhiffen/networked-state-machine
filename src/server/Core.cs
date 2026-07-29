using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetworkedStateMachine.Server;

public static class Core
{
    const int BufMaxCount = 1024;

    static readonly int PacketSizeBytes = 0;

    static readonly Packet[] ParsedPacks = new Packet[BufMaxCount];
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

    public static void Flush()
    {
        ParsedPackWriteHead = 0;
        ParsedPackReadHead = 0;
        InputBufBytesToRead = 0;
        InputBufWriteHead = 0;
        InputBufReadHead = 0;
    }

    public static void Tick()
    {
        ParsePendingPackets();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public void AppendInputBuf(byte[] bytes, int count)
    {
        //We should always be consuming/flushing this buffer faster than we can fill it up.
        //if this fills up too often then double the array size.
        int overflowByteCount = (count + InputBufWriteHead * PacketSizeBytes) - BufMaxCount;
        if (overflowByteCount > 0) throw new IndexOutOfRangeException(
        "Input buffer Overflow: inputs not being processed in time between frames.");

        Array.Copy(bytes, 0, InputBuf, InputBufWriteHead, count);

        InputBufWriteHead += count;
        InputBufBytesToRead += count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ParsePendingPackets()
    {
        int i = 0;
        while (i < InputBufBytesToRead)
        {
            ReadOnlySpan<byte> slice = InputBuf.AsSpan(InputBufReadHead, PacketSizeBytes);
            var pack = MemoryMarshal.Read<Packet>(slice);
            ParsedPacks[ParsedPackWriteHead] = pack;

            i++;
            InputBufReadHead += PacketSizeBytes;
            ParsedPackWriteHead++;
        }
        InputBufReadHead = 0;
    }
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct Packet(byte hi)
{
    readonly public byte Hi = hi;
}



