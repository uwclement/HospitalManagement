using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Account
{
    public class signupModel : PageModel
    {
		String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
		public string message = "";
		public Patient patient = new Patient();
		public void OnGet()
		{
		}
		public void OnPost()
		{
			patient.fullName = Request.Form["fullName"];
			patient.email = Request.Form["email"];
			patient.phone = Request.Form["phone"];
			patient.address = Request.Form["address"];
			string password = Request.Form["password"];
			string roles = "patient";
			if (patient.phone.Length == 0 || patient.fullName.Length == 0
				|| patient.email.Length == 0 || patient.address.Length == 0 || password.Length == 0)
			{
				message = "Provide All Info";
				return;
			}
			try
			{
				using (SqlConnection conn = new SqlConnection(conString))
				{
					conn.Open();
					using (SqlCommand cmd = new SqlCommand("create_patient", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@FullName", patient.fullName);
						cmd.Parameters.AddWithValue("Email", patient.email);
						cmd.Parameters.AddWithValue("@Phone", patient.phone);
						cmd.Parameters.AddWithValue("@Address", patient.address);
						cmd.Parameters.AddWithValue("@password", password);
						cmd.Parameters.AddWithValue("@Role", roles);
						int rowAffected = cmd.ExecuteNonQuery();
						if (rowAffected > 0)
						{
							message = "Patient Created";

						}
						else
						{
							message = "Patient not Created";
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Violation of PRIMARY KEY "))
				{
					message = "There's a problem: Patient already exists";

				}
				else
				{
					message = "There's a problem: " + ex.Message;

				}

			}
			Patient pat = new Patient();
			password = "";

		}
	}
}

