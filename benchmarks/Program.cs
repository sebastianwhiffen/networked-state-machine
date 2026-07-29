using System.Diagnostics;
using BenchmarkDotNet.Running;
using NetworkedStateMachine.Server;

namespace NetworkedStateMachine.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ParserPerf>();
    }
}



