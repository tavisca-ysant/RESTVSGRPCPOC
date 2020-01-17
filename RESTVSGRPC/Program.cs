using BenchmarkDotNet.Running;
using System;

namespace RESTVSGRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkHarness>();
            Console.ReadKey(true);
        }
    }
}
