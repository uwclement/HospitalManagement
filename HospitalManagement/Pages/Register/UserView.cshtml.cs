using HospitalManagement.Pages.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Mail;
using HospitalManagement.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagement.Pages.Register
{
	
	public class UserViewModel : PageModel
	{
		public List<Userinfo> listPatients = new List<Userinfo>();
		public void OnGet()
		{
			listPatients.Clear();
			try
			{
				String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(conString))
				{
					con.Open();
					String sqlquery = "select * from users";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Userinfo userinfo = new Userinfo();
								userinfo.id = "" + reader.GetInt32(0);
								userinfo.name = reader.GetString(1);
								userinfo.email = reader.GetString(2);
								userinfo.phone = reader.GetString(3);
								userinfo.role = reader.GetString(4);
								userinfo.password = reader.GetString(5);
								userinfo.createdat = " " + reader.GetDateTime(6);

								listPatients.Add(userinfo);
							}
						}
					}
				}

			}
			catch (Exception ex)
			{

				Console.WriteLine("Exception:" + ex.Message);
			}
		}

        [BindProperty]
        public email sendmail { get; set; }

        public async Task OnPostAsync()
        {
            try
            {
                string to = sendmail.To;
                string subject = sendmail.Subject;
                string body = sendmail.Body;

                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = false;
                mm.From = new MailAddress("rozeyclempercy@gmail.com");

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("rozeyclempercy@gmail.com", "cleme11111");
                    await smtp.SendMailAsync(mm);
                }

                ViewData["Message"] = "The mail is sent to " + to;
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "An error occurred: " + ex.Message;
                Console.WriteLine("Exception details: " + ex.ToString());
            }
        }
    }
	public class Userinfo
	{
		public string id;
		public string name;
		public string email;
		public string phone;
		public string role;
		public string password;
		public string createdat;
	}

	
}
