using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Server;

public abstract class NSM_Server : INSM_Server
{
    private readonly string _GUID = new Guid().ToString();
    public string GUID => _GUID;

    protected readonly Parser _parser;
    protected NSM_StateMachineManager _smm;

    public NSM_Server(NSM_StateMachineManager smm)
    {
        _parser = new();
        _smm = smm;
    }

    //the server should keep each of its clients up to date with the registered state machines.
    //calling this will register the state machine on each added client.
    //adding a client to a server will also cause the registered state machines to be added 
    //
    /// <summary>
    /// registers a state machine on this server, and all clients attatched to this server
    /// </summary>
    public void RegisterStateMachine(string keyName, Func<NSM_StateMachine> stateMachine)
        => _smm.RegisterStateMachine(keyName, stateMachine);

    public void GiveBytes(ReadOnlySpan<byte> bytes)
    {
        _parser.AppendInputBuf(bytes, bytes.Length);
    }

    public void Quit()
    {
    }

    public void Tick()
    {
        // _parser.Tick();
    }

    public void PhysTick(double delta)
    {
    }
}


public class NSM_LocalServer : NSM_Server
{
    public NSM_LocalServer(NSM_StateMachineManager smm) : base(smm) { }
    public NSM_LocalServer() : base(new NSM_StateMachineManager(new LocalTransporter())) { }

}

// public class NSM_RemoteServer : NSM_Server
// {
// }


