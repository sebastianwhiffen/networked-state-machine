namespace NetworkedStateMachine.Shared;

public interface INSM_Server
{
    public string GUID { get; }
    public void GiveBytes(ReadOnlySpan<byte> bytes);
    public void PhysTick(double delta);
    public void Tick();

    public void RegisterStateMachine(string keyName, Func<NSM_StateMachine> stateMachine);
}

public interface INSM_Client
{
    public void AddServer(INSM_Server server);

    public void PhysTick(double delta);
    public void Tick();

    //I probably want this to be like the websdks DI container later (singleton vs scoped)
    public void RegisterStateMachine<T>(string keyName, Func<T> stateMachine) where T : NSM_StateMachine;

    /// <summary>
    /// Uses the internal factory to instantiate a new state machine for the object you wish to pair it with.
    /// T (the return type) must be a NSM_StateMachine
    /// ReferenceInput is the type of inputs this stateMachine expects
    /// and ReconciledType is the reconcilable values
    /// </summary>
    public T CreateStateMachineFor<T>(string keyName) where T : NSM_StateMachine;

};

