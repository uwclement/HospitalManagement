using HospitalManagement.Pages.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Appointment
{
    public class AppointmentRequestModel : PageModel
    {
	
			public Serviceinfo serviceinfo = new Serviceinfo();
			public String errorMessage = "";
			public String successMessage = "";
		public string userEmail;
		public string userName;
			public void OnGet()
			{
			userEmail = HttpContext.Session.GetString("email");
			userName = HttpContext.Session.GetString("fullname");
            String id = Request.Query["id"];
				try
				{
					String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";

					using (SqlConnection con = new SqlConnection(conString))
					{
						con.Open();
						String sqlquery = "select * from service where id=@id";
						using (SqlCommand cmd = new SqlCommand(sqlquery, con))
						{
							cmd.Parameters.AddWithValue("@id", id);
							using (SqlDataReader reader = cmd.ExecuteReader())
							{
								if (reader.Read())
								{
									;
									serviceinfo.id = "" + reader.GetInt32(0);
									serviceinfo.servicename = reader.GetString(1);
									serviceinfo.status = reader.GetString(3);
									//serviceinfo.doctor = reader.GetString(4);


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
			public void OnPost()
			{
				//serviceinfo.id = Request.Form["id"];
			    serviceinfo.name = Request.Form["name"];
			    serviceinfo.email = Request.Form["email"];
			    serviceinfo.servicename = Request.Form["servicename"];
			    serviceinfo.history = Request.Form["history"];
			    
				if (  serviceinfo.servicename.Length ==0
				 || serviceinfo.history.Length == 0 || serviceinfo.email.Length == 0)


				{
					errorMessage = "All Fields are Required";
					return;
				}
				try
				{
					String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
					using (SqlConnection con = new SqlConnection(conString))
					{
						con.Open();
						String sqlquery = "insert into appointment (name,email,service,history) values(@name,@email,@service,@history) ";
						using (SqlCommand cmd = new SqlCommand(sqlquery, con))
						{
						//cmd.Parameters.AddWithValue("@id", serviceinfo.id);
						cmd.Parameters.AddWithValue("@name",serviceinfo.name);
						cmd.Parameters.AddWithValue("@email",serviceinfo.email);
							cmd.Parameters.AddWithValue("@service", serviceinfo.servicename);
							//cmd.Parameters.AddWithValue("@status", serviceinfo.status);
							//cmd.Parameters.AddWithValue("@location", serviceinfo.location);
							cmd.Parameters.AddWithValue("@history", serviceinfo.history);
							cmd.ExecuteNonQuery();
						}
					}
				}
				catch (Exception ex)
				{
					errorMessage = ex.Message;
					return;
				}
				successMessage = "Appointment Requested";
				Response.Redirect("/Appointment/AppointmentPage");
			}
		}
	}
