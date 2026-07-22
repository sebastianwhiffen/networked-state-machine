using System.Runtime.CompilerServices;

unsafe
{
    byte[] buffer = GC.AllocateArray<byte>(16, pinned: true);
    byte* bufferPtr = (byte*)Unsafe.AsPointer(ref buffer[0]);

    byte[] wow = System.Text.Encoding.ASCII.GetBytes("christ");

    Array.Copy(wow, 0, buffer, 0, wow.Length);

    var x = string.Join(
        Environment.NewLine,
        buffer.Chunk(8).Select(group => string.Join(" ", group.Select(b => b.ToString("X2"))))
    );

    Console.WriteLine(x);
    Console.WriteLine((long)bufferPtr);
}
