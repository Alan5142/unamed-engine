
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Test;

[StructLayout(LayoutKind.Sequential)]
public struct TestStruct
{
    public int a;
    public int b;
}

public class Class1
{
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static void Func()
    {
        Console.WriteLine("Hello from .NET!");
    }

    [UnmanagedCallersOnly]
    public static unsafe void SayName(char* name, int size)
    {
        Console.WriteLine("We're now in C# :D");
        Console.WriteLine($"Size of the string passed: {size}");
        string nameStr = Marshal.PtrToStringAnsi((IntPtr)name, size);
        Console.WriteLine($"Hi there {nameStr}, look, we're interpolating with a parameter!");
    }

    [UnmanagedCallersOnly]
    public static int SumTest(int a, int b)
    {
        return a + b;
    }

    [UnmanagedCallersOnly]
    public static unsafe void MultipleArgs(int a, char b, char* c)
    {
        string testStr = new(c);
        Console.WriteLine($"Multiple args: {a}, {b}, {testStr}");
    }

    // Receive a native char* and copy the .NET version string into it.
    [UnmanagedCallersOnly]
    public static unsafe void GetDotNetVersion(byte* buffer, int bufferLength)
    {
        string versionString = RuntimeInformation.FrameworkDescription;
        int length = Math.Min(versionString.Length, bufferLength - 1);
        fixed (char* pVersionString = versionString)
        {
            for (int i = 0; i < length; i++)
            {
                buffer[i] = (byte)pVersionString[i];
            }
        }
        buffer[length] = 0;
    }

    [UnmanagedCallersOnly]
    public static unsafe void GetStruct(TestStruct* testStruct)
    {
        // Convert to a ref struct to access the fields.
        ref TestStruct testStructRef = ref Unsafe.AsRef<TestStruct>(testStruct);
        HandleStruct(ref testStructRef);
    }

    private static void HandleStruct(ref TestStruct testStruct)
    {
        testStruct.a = 42;
        testStruct.b = 43;
    }
}