using System.Diagnostics;
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
        // CancellationTokenSource cs = new();
        //
        // INSM_Client client = new NSM_Client();
        // INSM_Server server = new NSM_LocalServer();
        //
        // var sm = () => { return new MyStateMachine([new IdleState(), new WalkingState()], new IdleState()); };
        //
        // client.RegisterStateMachine("myStateMachine", sm);
        //
        // var myDude = new MyGuy(client);
        // myDude.Ready();
        //
        // var clientThread = new Thread(() => FixedStepTicker.Start([client, myDude]));
        // var serverThread = new Thread(() => FixedStepTicker.Start([server]));
        //
        // clientThread.Start();
        // serverThread.Start();
        //
        // clientThread.Join();

    }
}

public class MyGuy(INSM_Client client)
{
    private MyStateMachine _theDudesSM;

    public float Position;

    public void Ready()
    {
        _theDudesSM = client.CreateStateMachineFor<MyStateMachine>("myStateMachine");
    }

    public void Tick()
    {
        _theDudesSM.PushInput(new MyGuysInputs() { MoveForward = 1.0f });
    }

    public void PhysTick(double delta)
    {
        Position = _theDudesSM.GetReconciledValues()?.Position ?? 1;
        Console.WriteLine(Position);
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
        List<NSM_State> states,
        NSM_State initialState
    ) : base(states, initialState) { }

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
    }

    public override void PhysTick(double delta) { }
}

public class IdleState : NSM_State<MyStateMachine>
{
    //I think this will ignore the input and not apply it. could be a problem. 
    //would I have to tick the new state on each transition?
    public override void Tick()
    {

    }

    public override void PhysTick(double delta) {
        
    }
}

public union UTickable(INSM_Server, INSM_Client, MyGuy)
{
    public void UPhysTick(double d)
    {
        switch (this)
        {
            case INSM_Server s: s.PhysTick(d); break;
            case INSM_Client c: c.PhysTick(d); break;
            case MyGuy g: g.PhysTick(d); break;
        }
    }

    public void UTick()
    {
        switch (this)
        {
            case INSM_Server s: s.Tick(); break;
            case INSM_Client c: c.Tick(); break;
            case MyGuy g: g.Tick(); break;
        }
    }
}

//https://gafferongames.com/post/fix_your_timestep/
public static class FixedStepTicker
{
    public static void Start(List<UTickable> tickable)
    {
        double time = 0.0;
        Stopwatch stopwatch = Stopwatch.StartNew();
        double currentTime = 0.0;
        double accumulatedFrameTime = 0.0;
        double targetFrameTime = 1;

        while (true)
        {
            double newTime = stopwatch.Elapsed.TotalSeconds;
            double frameTimeDelta = newTime - currentTime;
            currentTime = newTime;

            accumulatedFrameTime += frameTimeDelta;

            while (accumulatedFrameTime >= targetFrameTime)
            {
                accumulatedFrameTime -= targetFrameTime;
                time += targetFrameTime;

                tickable.ForEach(x => x.UPhysTick(frameTimeDelta));
            }

            tickable.ForEach(x => x.UTick());
        }
    }
}
