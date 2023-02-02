namespace BrokerApi.Interfaces
{
    public interface IHashingService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public string CreateRandomVerificationToken();
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
