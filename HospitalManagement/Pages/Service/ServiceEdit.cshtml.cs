using HospitalManagement.Pages.Register;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Service
{
    public class ServiceEditModel : PageModel
    {
		public Serviceinfo serviceinfo = new Serviceinfo();
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
					String sqlquery = "select * from service where id=@id";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						cmd.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								
								serviceinfo.id = "" + reader.GetInt32(0);
								serviceinfo.servicename = reader.GetString(1);
								serviceinfo.status = reader.GetString(3);
								serviceinfo.doctor = ""+reader.GetInt32(4);


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
			serviceinfo.id = Request.Form["id"];
			serviceinfo.servicename = Request.Form["servicename"];
			serviceinfo.status = Request.Form["status"];
			serviceinfo.doctor = Request.Form["doctor"];
			if (serviceinfo.id.Length == 0 )


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
					String sqlquery = "UPDATE service SET servicename = @servicename, Status=@status, doctor=@doctor WHERE id=@id;";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						cmd.Parameters.AddWithValue("@id", serviceinfo.id);
						cmd.Parameters.AddWithValue("@servicename", serviceinfo.servicename);
						cmd.Parameters.AddWithValue("@status", serviceinfo.status);
						cmd.Parameters.AddWithValue("@doctor", serviceinfo.doctor);
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
			successMessage = "Service Updated";
			Response.Redirect("/Service/ServiceView");
		}
	}
}
