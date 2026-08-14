using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Server;

public class NSM_Server
(Parser? p = null, StateMachineManager? smm = null) : INSM_Server
{
    private readonly string _UID = new Guid().ToString();
    public string UID => _UID;

    private readonly Parser _parser = p ?? new();
    private readonly StateMachineManager _smm = smm ?? new();

    public void RegisterStateMachine(NSM_StateMachine stateMachine) => _smm.AddStateMachine(stateMachine);

    public void GiveBytes(ReadOnlySpan<byte> bytes)
    {
        _parser.AppendInputBuf(bytes, bytes.Length);
    }

    public void Tick()
    {
        _parser.Tick();
    }
}

public class StateMachineManager
{
    public readonly Dictionary<NSM_UID, NSM_StateMachine> _registeredStateMachines = [];

    public NSM_UID current_id;

    //TODO: figure out if this is required by writing some tests?? 
    //its an edge case I'm pre-worrying about rn
    private readonly Lock _idLock = new();

    public NSM_UID AddStateMachine(NSM_StateMachine sm)
    {
        lock (_idLock)
        {
            NSM_UID id = current_id;
            current_id = current_id++;

            _registeredStateMachines[id] = sm;

            return id;
        }
    }
}
