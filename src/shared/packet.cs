using System.Runtime.InteropServices;

namespace NetworkedStateMachine.Shared;

[StructLayout(LayoutKind.Sequential)]
public readonly record struct NSM_Packet(short id, short mdx, short mdy, short a)
{
    public readonly short NSM_UID = id;
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

public class NSM_Manifest
{
    public List<string> UIDs;
}


public class LocalTransporter : ITransport
{
    public NSM_Manifest Manifest { get; set; } = new();

    private readonly Dictionary<string, Action<ReadOnlySpan<byte>>> _listeners = [];
    public void AddListener(string name, Action<ReadOnlySpan<byte>> l) => _listeners.Add(name, l);

    public void Send(ReadOnlySpan<byte> p)
    {
        foreach (var l in _listeners) l.Value(p);
    }
}

public class RemoteTransporter : ITransport
{
    public NSM_Manifest Manifest { get; set; } = new();

    public void AddListener(string name, Action<ReadOnlySpan<byte>> l)
    {
        throw new NotImplementedException();
    }

    public void Send(ReadOnlySpan<byte> packets)
    {
        throw new NotImplementedException();
    }
}

public class NoOpTransporter : ITransport
{
    public NSM_Manifest Manifest { get; set; } = new();

    public void AddListener(string name, Action<ReadOnlySpan<byte>> l) { }

    public void Send(ReadOnlySpan<byte> packets) { }
}

public interface ITransport
{
    public void Send(ReadOnlySpan<byte> packets);

    public void AddListener(string name, Action<ReadOnlySpan<byte>> l);

    public NSM_Manifest Manifest { get; set; }
}
