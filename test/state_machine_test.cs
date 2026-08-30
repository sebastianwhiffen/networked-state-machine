using System.Diagnostics;
using System.Numerics;
using NetworkedStateMachine.Client;
using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Test;

public class StateMachineTests
{
    TestTicker tt = new();

    [Fact]
    public void RegisterStateMachines()
    {
        CancellationTokenSource cs = new();

        INSM_Client client = new NSM_Client();
        INSM_Server server = new NSM_LocalServer();

        // var myDude = new MyGuy(client);
        // var sm = () => { return new MyStateMachine(myDude, [new IdleState(), new WalkingState()], new IdleState()); };
        //
        // string key = "MyStateMachineHaiii";
        //
        // server.RegisterStateMachine(key, sm);
        // client.RegisterStateMachine(key, sm);
        //
        // client.AddServer(server);

        tt.Tickables.AddRange([client, server]);
        tt.Start(cs.Token);

    }
}


public union UTickable(INSM_Server, INSM_Client)
{
    public void UPhysTick(double d)
    {
        switch (GetType())
        {
            case INSM_Server s: s.PhysTick(d); break;
            case INSM_Client c: c.PhysTick(d); break;
        }
    }

    public void UTick()
    {
        switch (GetType())
        {
            case INSM_Server s: s.Tick(); break;
            case INSM_Client c: c.Tick(); break;
        }
    }
}

public class TestTicker
{
    public List<UTickable> Tickables = [];

    public void Start(CancellationToken ct)
    {
        double time = 0.0;
        Stopwatch stopwatch = Stopwatch.StartNew();
        double currentTime = 0.0;
        double accumulatedFrameTime = 0.0;
        double targetFrameTime = 1;

        while (!ct.IsCancellationRequested)
        {
            double newTime = stopwatch.Elapsed.TotalSeconds;
            double frameTimeDelta = newTime - currentTime;
            currentTime = newTime;

            accumulatedFrameTime += frameTimeDelta;

            while (accumulatedFrameTime >= targetFrameTime)
            {
                accumulatedFrameTime -= targetFrameTime;
                time += targetFrameTime;

                Tickables.ForEach(x => x.UPhysTick(frameTimeDelta));
            }

            Tickables.ForEach(x => x.UTick());
        }
    }
}


public class MyGuy(INSM_Client client)
{
    //the fact that this "can" be null according to roslyn makes my skin crawl 
    private MyStateMachine _theDudesSM;

    public float Position;

    public void Ready()
    {
        _theDudesSM = client.CreateStateMachineFor<MyStateMachine>("MyStateMachineHaiii");
    }

    public void Tick()
    {
        _theDudesSM.SetInput(new MyGuysInputs() { MoveForward = 1.0f });
    }

    public void PhysTick(double delta)
    {
        Position = _theDudesSM.GetReconciledValues().Position;
    }

}

public class MyGuysAuthoritativeValues
{
    public int Position;
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

public class MyStateMachine : NSM_StateMachine<MyGuy, MyGuysInputs, MyGuysAuthoritativeValues>
{
    public override string Name { get; } = "MyStateMachine";

    public MyStateMachine(
        MyGuy myGuy,
        List<NSM_State> states,
        NSM_State initialState
    ) : base(myGuy, states, initialState) { }

    public override bool ChangeState<StateType>()
    {
        CurrentState = AvailableStates[typeof(StateType)];
        return true;
    }
}

public class WalkingState : NSM_State<MyStateMachine>
{
    public override void Tick()
    {
        if (ParentStateMachine.LastInput.MoveForward == 0)
        {
            ParentStateMachine.ChangeState<IdleState>();
        }

        ParentStateMachine.ReferenceObj.Position += ParentStateMachine.LastInput.MoveForward;
    }
}

public class IdleState : NSM_State<MyStateMachine>
{
    //I think this will ignore the input and not apply it. could be a problem. 
    //would I have to tick the new state on each transition?
    public override void Tick()
    {
        if (ParentStateMachine.LastInput.MoveForward > 0)
        {
            ParentStateMachine.ChangeState<WalkingState>();
        }
    }
}

