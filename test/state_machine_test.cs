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
        INSM_Client client = new NSM_Client();
        INSM_Server server = new NSM_LocalServer();

        var myDude = new MyGuy(client);
        var sm = () => { return new MyStateMachine([new MyState1(), new MyState2()]); };

        server.RegisterStateMachine("MyStateMachineHaiii", sm);
        client.RegisterStateMachine("MyStateMachineHaiii", sm);

        client.AddServer(server);
    }
}

//most c# game engines do not support instantiating a script like this. 
//use a global static instance of the client to call .CreateStateMachine
//this is only test code
public class MyGuy(INSM_Client client)
{
    //the fact that this "can" be null according to roslyn makes my skin crawl 
    private MyStateMachine _theDudesSM;

    public int Velocity;
    public int Position;

    //or "start" if you're a chud who uses unity
    public void Ready()
    {
        _theDudesSM = client.InstantiateStateMachine<MyStateMachine, MyGuysInputs, MyGuy>("MyStateMachineHaiii", this);
        _theDudesSM.Start();
    }

    public void Tick()
    {
        _theDudesSM.Tick();
    }

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
    public override string Name { get; } = "MyStateMachine";

    public MyStateMachine(List<NSM_State> states) : base(states)
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

