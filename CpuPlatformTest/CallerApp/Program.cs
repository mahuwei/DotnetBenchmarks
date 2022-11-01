// See https://aka.ms/new-console-template for more information

using AnyCpuLib;
using X86Lib;

Console.WriteLine("Cpu 多平台调用测试...");

Console.WriteLine("Any cpu lib 测试(按任意键开始)...");
Console.ReadKey();
var anyCpu = new AnyCpuSample();
Console.WriteLine($"AnyCpu.Add:{anyCpu.Add(3, 3)}");

Console.WriteLine("x86 cpu lib 测试(按任意键开始)...");
Console.ReadKey();

var x86Cpu = new X86Sample();
Console.WriteLine($"x86Cpu.Add:{x86Cpu.Add(3, 2)}");

Console.WriteLine();
Console.WriteLine("测试结果：\n  x86应用可以调用anyCpu和x86类库。\n  原则：不同平台不能运行。");