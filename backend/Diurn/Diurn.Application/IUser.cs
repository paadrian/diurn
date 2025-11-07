namespace Diurn.Application;

public interface IUser
{
    string Username { get; }
}

public class User : IUser
{
    public string Username { get; } = "Me";
}