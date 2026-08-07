namespace NetworkedStateMachine.Shared;

public interface INSM_Server
{
    public string UID { get; }
    public void GiveBytes(ReadOnlySpan<byte> bytes);
    public void Tick();
}

public interface INSM_Client;
