using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Shared;

public class NSM_StateMachineManager
{
    //something to think about
    //https://youtu.be/kowKK6tEwd0?si=rzrJ1QLwehhGZzsn
    public readonly Dictionary<NSM_UID, NSM_StateMachine> _smInstances = [];

    private readonly Dictionary<string, Func<NSM_StateMachine>> _registeredStateMachines = [];

    private ITransport _transport;

    public NSM_StateMachineManager(ITransport transport)
    {
        _transport = transport;
    }

    public void AddListener()
    {
    }

    public void Tick()
    {
        foreach (var sm in _smInstances.Values)
        {
            sm.Tick();
        }
    }

    public void RegisterStateMachine(string keyName, Func<NSM_StateMachine> sm)
    {
        _registeredStateMachines.Add(keyName, sm);
    }

    //TODO: figure out if this is required by writing some tests?? 
    //its an edge case I'm pre-worrying about rn
    private readonly Lock _idLock = new();
    private NSM_UID current_id;

    public T InstantiateRegisteredSM<T>(string keyName)
        where T : NSM_StateMachine
    {
        var sm = (T)_registeredStateMachines.FirstOrDefault(x => x.Key == keyName).Value();

        lock (_idLock)
        {
            NSM_UID id = current_id;
            current_id = current_id++;

            sm.NSM_UID = id;

            _smInstances[id] = sm;
        }

        return sm;
    }

    public List<string> GetManifest()
    {
        return _registeredStateMachines.Select(kv => kv.Key).ToList();
    }

    public void RoutePacket(NSM_Packet p)
    {
        var sm = _smInstances[(NSM_UID)p.NSM_UID]
            ?? throw new Exception($"no state machine registered with UID: {p.NSM_UID}");

        // sm.ReceiveInput(p);
    }
}
