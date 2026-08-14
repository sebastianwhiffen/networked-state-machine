using System.Runtime.InteropServices;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Client;

public class NSM_Client(ITransport t)
{
    private readonly ITransport _transport = t;

    public void Send(ReadOnlySpan<NSM_Packet> p) => _transport.Send(MemoryMarshal.AsBytes(p));

    public void AddServer(INSM_Server s) => _transport.AddListener(s.UID, s.GiveBytes);

}

public class LocalTransporter() : ITransport
{
    private readonly Dictionary<string, Action<ReadOnlySpan<byte>>> _listeners = [];

    public void AddListener(string name, Action<ReadOnlySpan<byte>> l) => _listeners.Add(name, l);

    public void Send(ReadOnlySpan<byte> p)
    {
        foreach (var l in _listeners) l.Value(p);
    }
}

public class RemoteTransporter : ITransport
{
    public void AddListener(string name, Action<ReadOnlySpan<byte>> l)
    {
        throw new NotImplementedException();
    }

    public void Send(ReadOnlySpan<byte> packets)
    {
        throw new NotImplementedException();
    }
}

public interface ITransport
{
    public void Send(ReadOnlySpan<byte> packets);

    public void AddListener(string name, Action<ReadOnlySpan<byte>> l);
}
