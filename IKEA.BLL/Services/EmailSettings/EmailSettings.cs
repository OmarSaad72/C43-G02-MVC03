using IKEA.DAL.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.NewFolder
{
    public class EmailSettings : IEmailSettings
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
           /*Sender*/ client.Credentials = new NetworkCredential("osaad4322@gmail.com", "yycxffztriacakje");
            client.Send("osaad4322@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
