using System.Diagnostics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Running;
using NetworkedStateMachine.Server;

namespace NetworkedStateMachine.Benchmarks;

public static class Program
{
    public unsafe static void Main(string[] args)
    {
        BenchmarkRunner.Run<ParserPerf>();
    }
}



