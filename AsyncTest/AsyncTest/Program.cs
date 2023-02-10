using AsyncTest;

Console.Title = "异步转同步";
Console.WriteLine("按任意键退出程序...");
Console.WriteLine();
var command = new Command();
try{
    Console.WriteLine("开始调用：GetValueEventAsync");
    command.OnCommandDelegate += OnHandler;
    command.GetValueEventAsync();
}
catch(Exception ex){
    Console.WriteLine($"  错误信息:{ex.Message}");
}
finally{
    await Task.Delay(5000);
    command.OnCommandDelegate -= OnHandler;
    command.Dispose();
}

Console.WriteLine();
command = new Command();
try{
    Console.WriteLine("开始调用：GetValueTaskAsync");
    var valueTaskAsync = await command.GetValueTaskAsync();
    Console.WriteLine($"  调用 GetValueTaskAsync 后，返回结果：{valueTaskAsync}");
}
catch(Exception ex){
    Console.WriteLine($"  错误信息:{ex.Message}");
}

await Task.Delay(5000);
command.GetValueEventAsync();
command.OnCommandDelegate += OnHandler;

await Task.Delay(5000);
command.OnCommandDelegate -= OnHandler;
command.Dispose();

Console.ReadKey();

void OnHandler(string value) {
    Console.WriteLine($"  调用 GetValueEventAsync 后，通过事件返回结果。{value}");
}