using System.Runtime.InteropServices;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Client;

public class NSM_Client(ITransport t)
{
    private readonly ITransport _transport = t;

    public void Send(ReadOnlySpan<Packet> p) => _transport.Send(MemoryMarshal.AsBytes(p));

}

public class LocalTransporter(Action<ReadOnlySpan<byte>>[] l) : ITransport
{
    private readonly Action<ReadOnlySpan<byte>>[] _listeners = l; 
    public void Send(ReadOnlySpan<byte> p)
    {
        foreach (var l in _listeners) l(p);
    }
}

public class RemoteTransporter : ITransport
{
    public void Send(ReadOnlySpan<byte> packets)
    {

    }
}

public interface ITransport
{
    public void Send(ReadOnlySpan<byte> packets);
}
