using System.Runtime.CompilerServices;

namespace NetworkedStateMachine.Shared;

//I'm thinking each state machine should act as 
//a layer between the entity and the operations being perfomed on said entity
//
//the server will receive an input from a client with a NSM UID.
//that will be routed to a 'duplicate' (from the clients perspective) state machine on the server
//  <side note>: the reason  this is 'duplicated' on the client is for client side prediction.
//
//the server will run the authoratative code and then return it to the client,
//who then does then routes this data like the server to the required state machine.
//
//this now puts the burdeon on the client to operate on values referenced by these networked state machines

public abstract class NSM_StateMachine<ReferenceType, InputType>
(List<NSM_State> states) : NSM_StateMachine where ReferenceType : class
{
    internal ReferenceType? ReferenceObj { get; set; }
    private readonly List<NSM_State> _states = states;

    public abstract void ReceiveInput(InputType p);
}

public abstract class NSM_StateMachine
{
    public NSM_UID NSM_UID { get; internal set; }

    public abstract string Name { get; }

    public abstract void Tick();

    internal Action StartCb { get; set; } = () =>
    {
        throw new Exception(
            $"state machine tried to start before being initialized through the server." +
            "please call (Client | Server).RegisterStateMachine");
    };

    public void Start() => StartCb();

    public abstract bool ChangeState(NSM_State newState);

    public static unsafe T UnsafeCast<T, R>(R input) where T : unmanaged where R : unmanaged
    {
        return *(T*)&input;
    }
}

public readonly record struct NSM_State_UID;
public abstract class NSM_State
{
    public short UID;
    public virtual void Tick() { }
};


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
