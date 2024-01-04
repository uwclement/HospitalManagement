using HospitalManagement.Pages.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Register
{
    public class UserEditModel : PageModel
    {
		public Userinfo userinfo = new Userinfo();
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
					String sqlquery = "select * from users where id=@id";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						cmd.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
							;
								userinfo.id = "" + reader.GetInt32(0);
								userinfo.name = reader.GetString(1);
								userinfo.email = reader.GetString(2);
								userinfo.phone = reader.GetString(3);
								userinfo.role = reader.GetString(4);
								userinfo.password = reader.GetString(5);
								userinfo.createdat = " " + reader.GetDateTime(6);

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
			userinfo.id = Request.Form["id"];
			userinfo.name = Request.Form["name"];
			userinfo.email = Request.Form["email"];
			userinfo.phone = Request.Form["phone"];
			userinfo.role = Request.Form["role"];
			userinfo.password = Request.Form["password"];
			userinfo.createdat = Request.Form["createdat"];
			if ( userinfo.role.Length == 0 )
			

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
					String sqlquery = "UPDATE users SET role = @role WHERE id=@id;";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						cmd.Parameters.AddWithValue("@id", userinfo.id);
						cmd.Parameters.AddWithValue("@role", userinfo.role);
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
			successMessage = "Petient Updated";
			Response.Redirect("/Register/UserView");
		}
	}
}

