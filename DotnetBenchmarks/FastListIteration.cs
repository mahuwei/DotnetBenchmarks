using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace DotnetBenchmarks;
/*
 // * Summary *

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.755)
Intel Core i7-10750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.100-rc.2.22477.23
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2  [AttachedDebugger]
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2


|           Method |  Size |         Mean |      Error |       StdDev | Allocated |
|----------------- |------ |-------------:|-----------:|-------------:|----------:|
|              For |   100 |     55.83 ns |   1.067 ns |     0.998 ns |         - |
|          ForEach |   100 |    108.61 ns |   1.781 ns |     1.666 ns |         - |
|     ForEach_Linq |   100 |    186.54 ns |   3.161 ns |     2.957 ns |         - |
| Parallel_ForEach |   100 |  6,019.83 ns | 110.096 ns |   102.983 ns |    2382 B |
|    Parallel_Linq |   100 | 24,991.18 ns | 286.987 ns |   268.447 ns |    6040 B |
|         For_Span |   100 |     34.05 ns |   0.553 ns |     0.518 ns |         - |
|     Foreach_Span |   100 |     33.92 ns |   0.601 ns |     0.533 ns |         - |
|              For |  1000 |    507.67 ns |   5.060 ns |     4.733 ns |         - |
|          ForEach |  1000 |  1,021.67 ns |  16.481 ns |    15.417 ns |         - |
|     ForEach_Linq |  1000 |  1,803.18 ns |  35.245 ns |    47.051 ns |         - |
| Parallel_ForEach |  1000 | 15,333.94 ns | 322.548 ns |   935.769 ns |    2786 B |
|    Parallel_Linq |  1000 | 29,441.03 ns | 482.616 ns |   592.696 ns |    6040 B |
|         For_Span |  1000 |    285.61 ns |   5.591 ns |     7.269 ns |         - |
|     Foreach_Span |  1000 |    281.35 ns |   5.398 ns |     5.544 ns |         - |
|              For | 10000 |  5,308.17 ns |  77.068 ns |    72.090 ns |         - |
|          ForEach | 10000 | 12,634.72 ns | 239.883 ns |   224.386 ns |         - |
|     ForEach_Linq | 10000 | 18,410.47 ns | 267.638 ns |   223.490 ns |         - |
| Parallel_ForEach | 10000 | 43,829.51 ns | 830.726 ns | 1,560.304 ns |    3727 B |
|    Parallel_Linq | 10000 | 40,999.12 ns | 724.166 ns |   677.386 ns |    6040 B |
|         For_Span | 10000 |  2,713.50 ns |  54.247 ns |    93.573 ns |         - |
|     Foreach_Span | 10000 |  2,675.44 ns |  52.994 ns |    76.003 ns |         - |

 */

[MemoryDiagnoser(false)]
public class FastListIteration {
    private static readonly Random Rng = new Random(80085);
    private List<int> _items;

    [Params(100, 1_000, 10_000)]
    public int Size {get; set;} = 100;

    [GlobalSetup]
    public void Setup() {
        _items = Enumerable.Range(1, Size).Select(x => Rng.Next()).ToList();
    }

    [Benchmark]
    public void For() {
        for(var i = 0; i < _items.Count; i++){
            var item = _items[i];
        }
    }

    [Benchmark]
    public void ForEach() {
        foreach(var item in _items){
        }
    }

    [Benchmark]
    public void ForEach_Linq() {
        _items.ForEach(item => { });
    }

    [Benchmark]
    public void Parallel_ForEach() {
        Parallel.ForEach(_items, i => { });
    }

    [Benchmark]
    public void Parallel_Linq() {
        _items.AsParallel().ForAll(i => { });
    }

    [Benchmark]
    public void For_Span() {
        var spans = CollectionsMarshal.AsSpan(_items);
        for(var i = 0; i < spans.Length; i++){
            var item = spans[i];
        }
    }

    [Benchmark]
    public void Foreach_Span() {
        foreach(var item in CollectionsMarshal.AsSpan(_items)){
        }
    }
}