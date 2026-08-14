using System.Numerics;
using NetworkedStateMachine.Client;
using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Test;

public class StateMachineTests
{
    [Fact]
    public void RegisterStateMachines()
    {
        var t = new LocalTransporter();
        var client = new NSM_Client(t);
        var server = new NSM_Server();

        var myDude = new MyGuy();
        var sm = new MyStateMachine([new MyState1(), new MyState2()], myDude);

        server.RegisterStateMachine(sm);

        client.AddServer(server);
    }
}

public class MyGuy
{
    public string DudesState { get; set; } = "none";
}

public class MyGuysInputs
{
    public int ClientTick;
    public Vector2 MouseDelta = Vector2.Zero;
    public float MoveForward;
    public float MoveRight;
    public bool JumpPressed;
    public bool JumpHeld;
}

public class MyStateMachine : NSM_StateMachine<MyGuy, MyGuysInputs>
{
    public MyStateMachine(List<NSM_State> states, MyGuy reference_obj) : base(states, reference_obj)
    {
    }

    public override bool ChangeState(NSM_State new_state)
    {
        throw new NotImplementedException();
    }

    public override void ReceiveInput(MyGuysInputs p)
    {

    }

    public override void Tick()
    {
        throw new NotImplementedException();
    }
}

public class MyState1 : NSM_State
{
}
public class MyState2 : NSM_State
{
}

