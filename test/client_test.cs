using System.Runtime.InteropServices;
using NetworkedStateMachine.Client;
using NetworkedStateMachine.Server;
using NetworkedStateMachine.Shared;
namespace NetworkedStateMachine.Test;

public class ClientTests
{
    // [Fact]
    // public void SendPacketThroughClient()
    // {
    //     NSM_Packet refPack = new();
    //     NSM_Packet testPack = PacketCreator.RandPacket();
    //
    //     var t = new LocalTransporter();
    //     t.AddListener("testListener", (beeees) => { refPack = MemoryMarshal.Read<NSM_Packet>(beeees); });
    //
    //     var client = new NSM_Client();
    //
    //     client.Send([testPack]);
    //
    //     Assert.True(testPack == refPack,
    //         $"\n\noriginal: {PacketCreator.DebugPacksAsBytes([testPack])}" +
    //         $"\n\noutput: {PacketCreator.DebugPacksAsBytes([refPack])}");
    //
    // }
    //
    // [Fact]
    // public void SendPacketThroughClientReceiveOnServer()
    // {
    //     NSM_Packet testPack = PacketCreator.RandPacket();
    //
    //     var t = new LocalTransporter();
    //
    //     var client_smm = new NSM_StateMachineManager(t);
    //     var server_smm = new NSM_StateMachineManager(t);
    //
    //     var client = new NSM_Client(client_smm);
    //     var server = new NSM_LocalServer(server_smm);
    //
    //     client.AddServer(server);
    //
    //     client.Send([testPack]);
    //
    //     server.Tick();
    //
    //     Assert.True(testPack == p.ParsedPacks[0],
    //         $"\n\noriginal: {PacketCreator.DebugPacksAsBytes([testPack])}" +
    //         $"\n\noutput: {PacketCreator.DebugPacksAsBytes([p.ParsedPacks[0]])}");
    //
    // }

}
