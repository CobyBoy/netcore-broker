using System.Runtime.InteropServices;

namespace BrokerApi.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail([Optional] string emailAdress, string verificationToken);
        public void ReSendEmail(string emailAdress, string reVerificationToken);
    }
}
