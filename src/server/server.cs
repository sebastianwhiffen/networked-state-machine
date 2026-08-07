using NetworkedStateMachine.Shared;

namespace NetworkedStateMachine.Server;

public class NSM_Server(Parser? p = null) : INSM_Server
{
    private readonly string _UID = new Guid().ToString();
    public string UID => _UID;

    public readonly List<string> _registeredStateMachines = [];

    private readonly Parser _parser = p ?? new();

    public void GiveBytes(ReadOnlySpan<byte> bytes)
    {
        _parser.AppendInputBuf(bytes, bytes.Length);
    }

    public void Tick()
    {
        _parser.Tick();
    }
}


