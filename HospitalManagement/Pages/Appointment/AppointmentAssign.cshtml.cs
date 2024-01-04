using HospitalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using static HospitalManagement.Pages.Appointment.AppointmentViewModel;
using HospitalManagement.Pages.Service;
using HospitalManagement.Pages.Doctor;
using System.Data;

namespace HospitalManagement.Pages.Appointment
{
	public class AppointmentAssignModel : PageModel
	{

		public List<Serviceinfos> servicelists = new List<Serviceinfos>();
		public Appointmentinfo appointmentinfo = new Appointmentinfo();
		public SessionInfo sessioninfo = new SessionInfo();
		public List<SessionInfo> sessionlist = new List<SessionInfo>();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
			String id = Request.Query["id"];
			String serviceName = Request.Query["service"];
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

					
						String sqlqueryy = $"SELECT * FROM {serviceName}";
						using (SqlCommand cmdd = new SqlCommand(sqlqueryy, con))
						{
							using (SqlDataReader reader = cmdd.ExecuteReader())
							{
								while (reader.Read())
								{
									Serviceinfos serviceinfos = new Serviceinfos();
									serviceinfos.id = "" + reader.GetInt32(0);
									serviceinfos.doctor = "" + reader.GetInt32(1);
									serviceinfos.date = reader.GetDateTime(2).ToString("yyyy-MM-dd");
									serviceinfos.session = reader.GetTimeSpan(3).ToString();
									serviceinfos.status = "" + reader.GetString(4);

									servicelists.Add(serviceinfos);
								}
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
			emailMessage.Subject = "Appoved";
			emailMessage.Body = email.Body+"  \n"+email.Appointment;

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
					String sqlquery = "UPDATE appointment SET respond ='Appoved' WHERE id=@id;";
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
	}

	public class SessionInfo
	{
		public string date;
		public string session;
	}

	public class Serviceinfos
	{
		public string id;
		public string servicename;
		public string department;
		public string status;
		public string doctor;
		public string session;
		public string date;

	}
}