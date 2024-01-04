using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;

namespace HospitalManagement.Pages.Email
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

		

		[BindProperty]
		public EmaillMessage email { get; set; }



		public async Task OnPostSendEmail()
		{
            string fromMail = "aucahospital@gmail.com";
            string fromPassword = "jsji rnkl rpyh jprn";

            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(fromMail );
            emailMessage.To.Add(email.To);
            emailMessage.Subject = email.Subject;
            emailMessage.Body = email.Body;

            emailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(emailMessage);
        }
    }
	}


