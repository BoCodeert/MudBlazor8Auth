namespace Blazor8Auth.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string? hashedpassword);
    }
}