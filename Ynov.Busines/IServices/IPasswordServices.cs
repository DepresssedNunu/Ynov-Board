namespace Ynov.Business.IServices;

public interface IPasswordServices
{
    public string HashPassword(string password);
    public bool VerifyPassword(string password, string hash);
}