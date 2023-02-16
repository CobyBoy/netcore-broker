using BrokerApi.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace BrokerApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail([Optional] string emailAdress, string verificationToken)
        {
            //
            var mesage = new MimeMessage();
            string link = "https://localhost:7149/api/Auth/confirm-email?verificationToken="+verificationToken;
            mesage.From.Add(MailboxAddress.Parse("aboutacoby@gmail.com"));
            mesage.To.Add(MailboxAddress.Parse("aboutacoby@gmail.com"));
            mesage.Subject = "Confirma con el botón, a ver si funciona";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = string.Format(@"

           
 <div class=""adM"">
  </div><table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
    <tbody>
      <tr>
        <td style=""background-image:url(https://ci5.googleusercontent.com/proxy/6GjVE_g4EFBaHUruQ2zSQML7BEARKf-lv4T36FSh8hhy7lVSH-xg0oWD7Dqwt64HrPkEJAvEogKqXWhFy_FmCURAViOXkhe9cJA-bw24QoIptxvrzuW_MC8tvRu4cn2ppHtmsv9IDr7WBx0dDcHwlXjOZPQtmGuNyRaJdKK4mf22v0-f6yJ9HfTKTycTWN1C5cdUgzJPJ52b_DwPXdy3LVkQq8uSG6ccZyK4_7Vsr_5vOlEj0AlYmLN4nQWHNeqRFTWvzY_vD6xkDm02aTlziO2x4b4=s0-d-e1-ft#https://marketing-image-production.s3.amazonaws.com/uploads/33688fdecb99e8c4bdba03c1ca353c3ed0ed98c185407bf08a5ed8487819a7f4453be086f265b1771b81a0a623fc11331f2c852907f114700e5a789a3894d833.jpg)"" bgcolor=""#3C5676"" align=""center"">
          <table style=""max-width:600px"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
            <tbody>
              <tr>
                <td style=""text-align:center;padding:15px 0"" valign=""top"" align=""center"">
               
                <h1 style=""font-family:'Open',sans-serif;color:#fff;margin-bottom:0"">Welcome to your first Ponzi scheme!</h1>
                <p style=""color:#fff;font-size:24px;margin-top:15px;margin-bottom:10px"">We’re revolutionizing fraud and scams</p>      
              </td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
      <tr>
        <td bgcolor=""#0D182D"" align=""center"">
          <table style=""max-width:600px"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
            <tbody>
              <tr>
                <td style=""padding:50px 50px 0 50px;border-radius:4px 4px 0 0"" valign=""top"" bgcolor=""#ffffff"" align=""left"">
                  <h2 style=""font-family:'Montserrat',sans-serif;color:#1d2635"">
                    Please Confirm Your Email
                  </h2>
                </td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
      <tr>
        <td style=""padding:0 10px 0 10px"" bgcolor=""#0D182D"" align=""center"">
          <table style=""max-width:600px"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
            <tbody>
              <tr>
                <td style=""padding:0 30px 30px 50px;border-left:1px solid #dde3eb;border-right:1px solid #dde3eb;line-height:24px"" bgcolor=""#fff"" align=""left"">
                  <p>
                    We're excited to have you get started. First, you need to confirm your account. Just press the buttom below.
                  </p>
                </td>
              </tr>
              <tr>
                <td style=""padding:0 30px 50px 30px;border-left:1px solid #dde3eb;border-right:1px solid #dde3eb"" bgcolor=""#fff"" align=""center"">
                  <table cellspacing=""0"" cellpadding=""0"" border=""0"">
                    <tbody>
                      <tr>
                        <td style=""border-radius:4px"" bgcolor=""#386FF9"" align=""center"">
                          <a href=""{0}"" style=""font-size:20px;font-family:'Montserrat',sans-serif;color:#ffffff;font-weight:bold;text-decoration:none;
color:#ffffff;text-decoration:none;padding:15px 50px;border-radius:4px;display:inline-block""
target=""_blank"">Confirm Email</a>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
      <tr>
        <td style=""padding:0 10px 0 10px"" bgcolor=""#0D182D"" align=""center"">
          <table style=""max-width:600px"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
            <tbody>
              <tr>
                <td style=""padding:20px;border:1px solid #dde3eb;border-radius:0 0 4px 4px"" bgcolor=""#fff"" align=""center"">
                  <p style=""margin:0"">Having trouble? <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ/"" style=""color:#0d182d"" target=""_blank"" >Contact Support</a></p>
                </td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
      <tr>
        <td style=""padding:15px 10px"" bgcolor=""#0D182D"" align=""center"">
          <table style=""max-width:600px"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
            <tbody>
              <tr>
                <td><p style=""font-size:16px;color:#fff"">© 2023 Coby</p></td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
    </tbody>
  </table>

            ", link);
            //email.Body = new TextPart(TextFormat.Html) { Text = "tEST bODY <button><a href=\"https://localhost:7149/api/Auth/verify-email?token=212\"> un boton</a></button>",  };
            
            mesage.Body = bodyBuilder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailSettings")["Host"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailSettings")["Username"], _configuration.GetSection("EmailSettings")["Password"]);
            smtp.Send(mesage);
            smtp.Disconnect(true);
        }
    }
}
