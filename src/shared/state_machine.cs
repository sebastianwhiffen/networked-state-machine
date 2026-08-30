using System.Runtime.CompilerServices;

namespace NetworkedStateMachine.Shared;

//I'm thinking each state machine should act as 
//a layer between the entity and the operations being performed on said entity
//
//the server will receive an input from a client with a NSM UID.
//that will be routed to a 'duplicate' (from the clients perspective) state machine on the server
//  <side note>: the reason  this is 'duplicated' on the client is for client side prediction.
//
//the server will run the authoritative code and then return the REQUIRED values back to the client(s state machine),
//
//this now puts the burden on the client to operate on the required 
//values referenced by these networked state machines
public abstract class NSM_StateMachine<ReferenceType, InputType, ReconcilableType>
: NSM_StateMachine where ReferenceType : class
{
    public InputType LastInput;
    public ReconcilableType LastReconciliation;

    public readonly ReferenceType ReferenceObj;
    public readonly Dictionary<Type, NSM_State> AvailableStates;

    public NSM_StateMachine(ReferenceType r, List<NSM_State> states, NSM_State initialState) : base(initialState)
    {
        ReferenceObj = r;
        AvailableStates = states.ToDictionary(s => s.GetType(), s => s);
    }

    public void SetInput(InputType input)
    {
        LastInput = input;
    }

    public ReconcilableType GetReconciledValues()
    {
        return LastReconciliation;
    }
}

public abstract class NSM_StateMachine
{
    public NSM_UID NSM_UID { get; internal set; }
    public abstract string Name { get; }
    protected NSM_State CurrentState;

    public NSM_StateMachine(NSM_State initialState)
    {
        CurrentState = initialState;
    }

    public void Tick() => CurrentState.Tick();

    public abstract bool ChangeState<NewStateType>() where NewStateType : NSM_State;

    public static unsafe T UnsafeCast<T, R>(R input) where T : unmanaged where R : unmanaged
    {
        return *(T*)&input;
    }
}

// public readonly record struct NSM_State_UID;

public abstract class NSM_State<ParentStateMachineType> : NSM_State
where ParentStateMachineType : NSM_StateMachine
{
    public ParentStateMachineType ParentStateMachine;

}

public abstract class NSM_State
{
    public short UID;
    public abstract void Tick();
};

//its just a short, they should really add a "NSM_UID : short" syntax
public readonly record struct NSM_UID(short Value)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator NSM_UID(short value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator short(NSM_UID value) => value.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NSM_UID operator ++(NSM_UID value) => new((short)(value.Value + 1));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NSM_UID operator --(NSM_UID value) => new((short)(value.Value - 1));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NSM_UID operator +(NSM_UID left, short right) => new((short)(left.Value + right));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NSM_UID operator -(NSM_UID left, short right) => new((short)(left.Value - right));
}


public static class SM_Consts
{
    public const string DEFAULT_SM_NAME = "no_name_set";
}
