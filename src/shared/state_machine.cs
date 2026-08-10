namespace NetworkedStateMachine.Shared;

//I'm thinking each state machine should act as 
//a layer between the entity and the operations being perfomed on said entity
public abstract class NSM_StateMachine<T>
(List<NSM_State> states, T reference_obj) : NSM_StateMachine where T : class
{
    private readonly T _reference_obj;
    private readonly List<NSM_State> _states = states;

    public virtual void ApplyPacket() { }

    public virtual void Reconcile() { }
}

public abstract class NSM_StateMachine
{
    public short UID;
    public virtual void Tick() { }

    public virtual bool ChangeState(NSM_State new_state) => true;
}

public abstract class NSM_State
{
    public short UID;
    public virtual void Tick() { }
};
