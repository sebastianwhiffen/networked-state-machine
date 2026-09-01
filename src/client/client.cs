using System.Runtime.InteropServices;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Client;

public class NetBuffer
{
    private readonly Memory<NSM_Packet> _netBuff;
}

public class NSM_Client : INSM_Client
{
    private readonly NSM_StateMachineManager _smm;

    public NSM_Client()
    {
        _smm = new NSM_StateMachineManager(new LocalTransporter());
    }

    public NSM_Client(NSM_StateMachineManager smm)
    {
        _smm = smm;
    }

    public void Send(ReadOnlySpan<NSM_Packet> ps)
    {
        foreach (NSM_Packet p in ps)
        {
            _smm.RoutePacket(p);
        }
    }

    // this should be checking if the client and the server contain the same state machines, 
    // honestly this is extremely rough.
    // if we let the client download state machines from the server you'll get RCE'd
    //
    // just set up the transports for now.
    public void AddServer(INSM_Server s)
    {
    }

    public T CreateStateMachineFor<T>(string keyName)
    where T : NSM_StateMachine
    {
        return _smm.InstantiateRegisteredSM<T>(keyName);
    }

    public void RegisterStateMachine(string keyName, Func<NSM_StateMachine> stateMachine)
    {
        _smm.RegisterStateMachine(keyName, stateMachine);
    }

    public void Tick()
    {
        // _smm.Tick();
    }

    public void PhysTick(double delta)
    {
    }

    public void RegisterStateMachine<T>(string keyName, Func<T> stateMachine) where T : NSM_StateMachine
    {
        _smm.RegisterStateMachine(keyName, stateMachine);
    }
}


