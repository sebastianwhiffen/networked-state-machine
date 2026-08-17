namespace NetworkedStateMachine.Shared;

public interface INSM_Server : INSM_Registrar
{
    public string GUID { get; }
    public void GiveBytes(ReadOnlySpan<byte> bytes);
    public void Tick();
    public ITransport GetTransport();
    public List<string> GetManifest();
}

public interface INSM_Client : INSM_Registrar
{
    public void AddServer(INSM_Server server);

    /// <summary>
    /// Uses the internal factory to instantiate a new state machine for the object you wish to pair it with.
    /// T (the return type) must be a NSM_StateMachine
    /// RI (the type of inputs for the refrerence object)
    /// and R (the type you're networking states with) should be... erm. the type whos props you're networking.. 
    /// </summary>
    public T InstantiateStateMachine<T, RI, R>(string keyName, R ref_obj)
        where T : NSM_StateMachine<R, RI>
        where R : class;


};

public interface INSM_Registrar
{
    public void RegisterStateMachine(string keyName, Func<NSM_StateMachine> stateMachine);

}
