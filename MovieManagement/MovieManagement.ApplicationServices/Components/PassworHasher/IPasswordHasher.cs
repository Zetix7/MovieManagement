namespace MovieManagement.ApplicationServices.Components.PassworHasher;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string passwordHash, string inputPassword);
}
