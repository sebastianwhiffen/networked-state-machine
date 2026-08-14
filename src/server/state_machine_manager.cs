using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Server;

public class NSM_StateMachineManager()
{
    private readonly List<NSM_StateMachine> _stateMachines;

    public NSM_UID AddStateMachine(NSM_StateMachine sm)
    {
        _stateMachines.Add(sm);
        return sm.NSM_UID;
    }

    private void RoutePacket(NSM_Packet p)
    {
        _stateMachines.First(sm => sm.NSM_UID == (NSM_UID)p.NSM_UID);
    }

}

