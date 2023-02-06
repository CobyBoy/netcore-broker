using System.Runtime.InteropServices;

namespace BrokerApi.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail([Optional]string emailAdress, [Optional] string verificationToken);
    }
}
