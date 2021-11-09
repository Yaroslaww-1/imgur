namespace MediaLakeUsers.BuildingBlocks.Security
{
    public interface ISecurityService
    {
        string HashPassword(string password, byte[] salt);
        byte[] GetRandomSalt();
        byte[] GetRandomBytes(int length);
        bool ValidatePassword(string password, string hash, string salt);
    }
}
