using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Server;

public class NSM_LocalServer : INSM_Server
{
    private readonly string _GUID = new Guid().ToString();
    public string GUID => _GUID;

    private readonly Parser _parser = new();
    private readonly ITransport _transport = new LocalTransporter();
    private readonly NSM_StateMachineManager _smm;

    public NSM_LocalServer() { }
    public NSM_LocalServer(ITransport transport)
    {
        _transport = transport;
        _smm = new(_transport);
    }

    /// <summary>
    /// registers a state machine on this server, and all clients attatched to this server
    /// </summary>

    //the server should keep each of its clients up to date with the registered state machines.
    //calling this will register the state machine on each added client.
    //adding a client to a server will also cause the registered state machines to be added 
    public void RegisterStateMachine(string keyName, Func<NSM_StateMachine> stateMachine) => _smm.RegisterStateMachine(keyName, stateMachine);

    public void GiveBytes(ReadOnlySpan<byte> bytes)
    {
        _parser.AppendInputBuf(bytes, bytes.Length);
    }
    public void Quit()
    {
    }

    public void Tick()
    {
        _parser.Tick();
    }

    public List<string> GetManifest()
    { 
        return _smm.
    }

    public ITransport GetTransport()
    {
        throw new NotImplementedException();
    }
}


