using practika;

public class Program
{
    static void Main()
    {
        GridIniter initer = new();
        Solution _solution;

        _solution = new(initer.Init(0, 1, 0));
        _solution.Run();
    }
}