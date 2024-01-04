using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Patient
{
    public class PatientEditModel : PageModel
    {

		public PatientInfo patientinfo = new PatientInfo();
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
					String sqlquery = "select * from patient where id=@id";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						cmd.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								patientinfo.id = "" + reader.GetInt32(0);
								patientinfo.fname = reader.GetString(1);
								patientinfo.lname = reader.GetString(2);
								patientinfo.DOB = " " + reader.GetDateTime(3);
								patientinfo.gender = reader.GetString(4);
								patientinfo.contact = reader.GetString(5);
								patientinfo.address = reader.GetString(6);
								patientinfo.history = reader.GetString(7);

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
			patientinfo.id = Request.Form["id"];
			patientinfo.fname = Request.Form["fname"];
			patientinfo.lname = Request.Form["lname"];
			patientinfo.contact = Request.Form["contact"];
			patientinfo.gender = Request.Form["gender"];
			patientinfo.DOB = Request.Form["dob"];
			patientinfo.address = Request.Form["address"];
			patientinfo.history = Request.Form["history"];
			patientinfo.createdat = Request.Form["createdat"];
			if (patientinfo.fname.Length == 0 || patientinfo.lname.Length == 0 || patientinfo.contact.Length == 0
				|| patientinfo.DOB.Length == 0 || patientinfo.address.Length == 0 || patientinfo.history.Length == 0
				)
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
					String sqlquery = "UPDATE patient SET first_name = @first_name, last_name = @last_name, date_of_birth = @date_of_birth, gender = @gender, contact_number = @contact_number, address = @address, medical_history = @medical_history WHERE id = @id;";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						cmd.Parameters.AddWithValue("@id", patientinfo.id);
						cmd.Parameters.AddWithValue("@first_name", patientinfo.fname);
						cmd.Parameters.AddWithValue("@last_name", patientinfo.lname);
						cmd.Parameters.AddWithValue("@date_of_birth", patientinfo.DOB);
						cmd.Parameters.AddWithValue("@gender", patientinfo.gender);
						cmd.Parameters.AddWithValue("@contact_number", patientinfo.contact);
						cmd.Parameters.AddWithValue("@address", patientinfo.address);
						cmd.Parameters.AddWithValue("@medical_history", patientinfo.history);
						

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
			Response.Redirect("/Patient/PatientView");
		}
	}
}
