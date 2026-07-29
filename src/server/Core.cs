using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetworkedStateMachine.Server;

public static class Core
{
    public const int BufMaxCount = 1024;

    public static readonly int PacketSizeBytes = 0;

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
        while (InputBufReadHead < InputBufBytesToRead)
        {
            ReadOnlySpan<byte> slice =
                InputBuf.AsSpan(InputBufReadHead, PacketSizeBytes);

            Packet pack = MemoryMarshal.Read<Packet>(slice);

            ParsedPacks[ParsedPackWriteHead] = pack;

            ParsedPackWriteHead++;
            InputBufReadHead += PacketSizeBytes;
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct Packet(short mdx, short mdy, short a)
{
    public readonly short MouseDeltaX = mdx;
    public readonly short MouseDeltaY = mdy;
    public readonly short Actions = a;

}

[Flags]
public enum InputAction : ushort
{
    None = 0,
    Forward = 1 << 0,
    Backward = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    Jump = 1 << 4,
    Attack = 1 << 5,
}



