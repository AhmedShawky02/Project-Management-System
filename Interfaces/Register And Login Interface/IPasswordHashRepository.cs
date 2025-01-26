namespace Project_Management_System.Interfaces
{
    public interface IPasswordHashRepository
    {
        string Hash(string password);
        bool Verified(string passwordHash, string password);
    }
}
