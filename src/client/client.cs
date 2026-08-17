using System.Runtime.InteropServices;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Client;

public class NSM_Client : INSM_Client
{
    private ITransport _transport = new NoOpTransporter();
    public ITransport Transport { get => _transport; private set => SetTransport(value); }

    private readonly NSM_StateMachineManager _smm;

    public NSM_Client()
    {
        _smm = new(_transport);
    }

    //will be filled with shit later no doubt, please put your shit here <3
    private void SetTransport(ITransport transport)
    {
        _transport = transport;
    }

    public void Send(ReadOnlySpan<NSM_Packet> p) => _transport.Send(MemoryMarshal.AsBytes(p));

    public void AddServer(INSM_Server s)
    {
        s.GetManifest();
        // SetTransport(s.GetTransport());
        // _transport.AddListener(s.GUID, s.GiveBytes);
    }

    public T InstantiateStateMachine<T, RI, R>(string keyName, R ref_obj)
    where T : NSM_StateMachine<R, RI>
    where R : class
    {
        return _smm.InstantiateRegisteredSM<T, RI, R>(keyName, ref_obj);
    }

    public void RegisterStateMachine(string keyName, Func<NSM_StateMachine> stateMachine)
    {
        _smm.RegisterStateMachine(keyName, stateMachine);
    }
}


