namespace AsyncTest;

public delegate void CommandDelegate(string value);

public sealed class Command : IDisposable {
    public void Dispose() {
    }

    public event CommandDelegate? OnCommandDelegate;

    public void GetValueEventAsync() {
        var tk = new CancellationTokenSource(new TimeSpan(0, 0, 0, 3));
        ValueEvent(tk.Token);
    }

    private void ValueEvent(CancellationToken cancellationToken, int sleepSecond = 2) {
        Console.WriteLine("  start call ValueEventAsync.");
        Task.Run(async () => {
            await Task.Delay(sleepSecond * 1000, cancellationToken);
            OnCommandDelegate?.Invoke($"GetValueEventAsync:{DateTime.Now}");
        }, cancellationToken);
        Console.WriteLine("  call ValueEventAsync end.");
    }

    public async Task<string?> GetValueTaskAsync() {
        var tk = new CancellationTokenSource(new TimeSpan(0, 0, 0, 5));
        string? resultValue = null;
        var isComplete = false;
        // 定义一个事件处理的内部方法，以便于取消事件订阅。
        void OnOnCommandDelegateHandler(string value) {
            Console.WriteLine($"  OnCommandDelegate事件返回结果。{value}");
            resultValue = value;
            isComplete = true;
        }

        OnCommandDelegate += OnOnCommandDelegateHandler;

        try{
            var task = Task.Run(() => {
                do{
                    if(isComplete == false){
                        Thread.Sleep(100);
                        continue;
                    }

                    break;
                } while(true);

                return resultValue;
            }, tk.Token);
            ValueEvent(tk.Token, 6);
            var waitAsync = await task.WaitAsync(tk.Token);
            task.Dispose();
            return waitAsync;
        }
        catch(Exception ex){
            Console.WriteLine(ex);
            throw;
        }
        finally{
            OnCommandDelegate -= OnOnCommandDelegateHandler;
        }
    }
}