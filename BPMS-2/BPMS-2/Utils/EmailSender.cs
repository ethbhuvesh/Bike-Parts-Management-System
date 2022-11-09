using System;
using System.Net.Mail;

namespace BPMS_2.Utils
{
    public class EmailSender
    {
       
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            SmtpClient client = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("email", "pass", "outlook.com"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.From = new MailAddress("email");
            mailMessage.Subject = "Confirm your email";
            mailMessage.Body = confirmationLink;
            mailMessage.IsBodyHtml = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;

        }

    }
}
