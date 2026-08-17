using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Shared;

public class NSM_StateMachineManager
{
    //something to think about
    //https://youtu.be/kowKK6tEwd0?si=rzrJ1QLwehhGZzsn
    public readonly Dictionary<NSM_UID, NSM_StateMachine> _smInstances = [];

    private readonly Dictionary<string, Func<NSM_StateMachine>> _registeredStateMachines = [];

    private readonly ITransport _transport;

    public NSM_StateMachineManager(ITransport transport)
    {
        _transport = transport;
    }

    //TODO: figure out if this is required by writing some tests?? 
    //its an edge case I'm pre-worrying about rn
    private readonly Lock _idLock = new();
    private NSM_UID current_id;

    public void RegisterStateMachine(string keyName, Func<NSM_StateMachine> sm)
    {
        _registeredStateMachines.Add(keyName, sm);
    }

    public T InstantiateRegisteredSM<T, RI, R>(string keyName, R refObj)
        where R : class
        where T : NSM_StateMachine<R, RI>
    {
        var sm = (NSM_StateMachine<R, RI>)_registeredStateMachines.FirstOrDefault(x => x.Key == keyName).Value();

        lock (_idLock)
        {
            NSM_UID id = current_id;
            current_id = current_id++;

            sm.NSM_UID = id;
            sm.StartCb = StartStateMachine;
            sm.ReferenceObj = refObj;

            _smInstances[id] = sm;
        }
        return (T)sm;
    }


    public void StartStateMachine()
    {
        // _transport.Send();
    }

    public List<string> GetManifest()
    {
        return _registeredStateMachines.Select(kv => kv.Key).ToList();
    }


    private void RoutePacket(NSM_Packet p)
    {
        // _stateMachines.First(sm => sm.NSM_UID == (NSM_UID)p.NSM_UID);
    }
}
