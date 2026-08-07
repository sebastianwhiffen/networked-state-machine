using System.Runtime.InteropServices;

namespace NetworkedStateMachine.Shared;

[StructLayout(LayoutKind.Sequential)]
public readonly record struct Packet(short mdx, short mdy, short a)
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
