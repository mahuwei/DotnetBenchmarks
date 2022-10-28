using BenchmarkDotNet.Running;
using DotnetBenchmarks;

var summary = BenchmarkRunner.Run<FastListIteration>();
Console.WriteLine(summary);