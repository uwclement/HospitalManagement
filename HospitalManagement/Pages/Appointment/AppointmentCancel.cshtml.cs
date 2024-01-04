using HospitalManagement.Pages.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;
using static HospitalManagement.Pages.Appointment.AppointmentViewModel;
using System.Data.SqlClient;
using HospitalManagement.Pages.Service;
using HospitalManagement.Models;

namespace HospitalManagement.Pages.Appointment
{
	public class AppointmentCancelModel : PageModel
	{
		public Appointmentinfo appointmentinfo = new Appointmentinfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
			String id = Request.Query["id"];
			try
			{
				String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";

				using (SqlConnection con = new SqlConnection(conString))
				{
					con.Open();
					String sqlquery = "select * from appointment where id=@id";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						cmd.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{

								appointmentinfo.id = "" + reader.GetInt32(0);
								appointmentinfo.name = reader.GetString(1);
								appointmentinfo.email = reader.GetString(2);
								appointmentinfo.service = reader.GetString(3);
								appointmentinfo.history = reader.GetString(4);
								appointmentinfo.respond = reader.GetString(6);
								//appointmentinfo.reason = reader.GetString(7);


							}
						}

					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
		}
		/// <summary>
		/// to send cancel email
		/// </summary>
		/// <returns></returns>



		[BindProperty]
		public SendRespond email { get; set; }
		public async Task OnPostSendEmail()
		{
			string fromMail = "aucahospital@gmail.com";
			string fromPassword = "jsji rnkl rpyh jprn";

			appointmentinfo.email = Request.Form["email"];
			var emailMessage = new MailMessage();
			emailMessage.From = new MailAddress(fromMail);
			emailMessage.To.Add(email.To);
			emailMessage.Subject = "Cancel";
			emailMessage.Body = email.Body;

			emailMessage.IsBodyHtml = true;

			var smtpClient = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(fromMail, fromPassword),
				EnableSsl = true,
			};
			//save to database first
			appointmentinfo.id = Request.Form["id"];
			if (appointmentinfo.id.Length == 0)


			{
				errorMessage = "Provide the Reason";
				return;
			}
			try
			{
				String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(conString))
				{
					con.Open();
					String sqlquery = "UPDATE appointment SET respond ='Cancel'  WHERE id=@id;";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						cmd.Parameters.AddWithValue("@id", appointmentinfo.id);

						//cmd.Parameters.AddWithValue("@reason", appointmentinfo.reason);

						cmd.ExecuteNonQuery();
						smtpClient.Send(emailMessage);
						successMessage = " Appointment Cancled";

					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
			successMessage = "Appointment Cancled";
			Response.Redirect("/Appointment/AppointmentView");
			
			successMessage = "Appointment Cancled";
			Response.Redirect("/Appointment/AppointmentView");
		}


		/// <summary>
		/// to save cancel to databasse 
		/// </summary>
		



		}

	}



