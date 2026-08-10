using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Server;

public class NSM_StateMachineManager()
{
    private readonly List<NSM_StateMachine> _stateMachines;

    public short AddStateMachine(NSM_StateMachine sm)
    {
        _stateMachines.Add(sm);
        return sm.UID;
    }

    private void RoutePacket(Packet p){
       _stateMachines.First(sm => sm.UID == p.NSM_uid);
    }

}

