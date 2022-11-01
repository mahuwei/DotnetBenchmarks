namespace WebApi.Services;

public class SingletonService {
    private static int _count;

    public int AddOne() {
        _count++;
        return _count;
    }
}