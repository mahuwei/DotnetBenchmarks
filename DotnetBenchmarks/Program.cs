using BenchmarkDotNet.Running;
using DotnetBenchmarks;

//var summary = BenchmarkRunner.Run<FastListIteration>();
BenchmarkRunner.Run<ContextPooling>();
Console.WriteLine("按任意键退出程序...");
Console.ReadKey();
