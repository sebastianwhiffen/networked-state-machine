using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace NetworkedStateMachine.Benchmarks;

public static class Program
{
    const int MAX_OP_SIZE_BYTES = 8;
    const int TEST_TIME_SECONDS = 1 * 1000;
    const int NUM_OP_IN_BATCH = 1;
    const int NUM_BATCHES = 5;

    static readonly Stopwatch sw = Stopwatch.StartNew();
    static readonly Random rng = new();
    static readonly OpType[] availableOpTypes = [OpType.HI, OpType.BYE];
    static readonly int[] opTypeAppearancePercentages = [50, 100];
    static readonly Dictionary<OpType, byte[]> operations = new()
    {
        { OpType.HI,  "HI"u8.ToArray() },
        { OpType.BYE, "BYE"u8.ToArray() },
    };

    //eatswa cuz...
    // operations.Select(op => (
    //     key: op.Key,
    //     bytes: string.Join(
    //         Environment.NewLine,
    //         op.Value.Chunk(8).Select(row => string.Join(" ", row.Select(bite => bite.ToString("X2"))))
    //     )
    // )).ToList().ForEach(kv => Console.WriteLine($"Op name: {kv.key}, Op Bytes: {kv.bytes}"));

    static int loops = 0;

    public unsafe static void Main(string[] args)
    {
        byte[] batch = GC.AllocateArray<byte>(MAX_OP_SIZE_BYTES * NUM_OP_IN_BATCH, pinned: true);
        byte* batchPtr = (byte*)Unsafe.AsPointer(ref batch[0]);

        operations.Select(op => (
               key: op.Key,
               bytes: string.Join(
                   Environment.NewLine,
                   op.Value.Chunk(8).Select(row => string.Join(" ", row.Select(bite => bite.ToString("X2"))))
               )
           )).ToList().ForEach(kv => Console.WriteLine($"Op name: {kv.key}, Op Bytes: {kv.bytes}"));

        int batchBufferLength = 0;
        for (int i = 0; i < NUM_OP_IN_BATCH; i++)
        {
            int percent = rng.Next(100);
            OpType op = SelectOpType(percent);

            Array.Copy(operations[op], 0, batch, batchBufferLength, operations[op].Length);
            batchBufferLength += operations[op].Length;
        }

        batch.Chunk(8).Select(
            row => string.Join(" ", row.Select(bite => bite.ToString("X2")
        ))).ToList().ForEach(Console.WriteLine);

        while (true)
        {
            loops++;
            if (sw.ElapsedMilliseconds > TEST_TIME_SECONDS) break;
        }

        double throughput = loops * NUM_OP_IN_BATCH / (sw.ElapsedMilliseconds);

        Console.WriteLine($"Throughput per m/s: {throughput}");
    }
    static OpType SelectOpType(int percent)
    {
        for (int i = 0; i < opTypeAppearancePercentages.Length; i++)
            if (percent <= opTypeAppearancePercentages[i])
                return availableOpTypes[i];
        throw new Exception($"Invalid input percentage {percent}");
    }

    struct MyStruct
    {
        public int i;
    }


    public enum OpType : byte
    {
        HI,
        BYE = 255
    }
}



