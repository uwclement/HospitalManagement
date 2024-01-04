using HospitalManagement.Models;
using HospitalManagement.Pages.Register.Doctor;
using HospitalManagement.Pages.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Doctor
{
	public class DoctorCreateModel : PageModel
	{
		public Doctorinfo doctorinfo = new Doctorinfo();
		public String errorMessage = "";
		public String successMessage = "";

		String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
		public string message = "";
		public Doctor doctor = new Doctor();
		public void OnGet()
		{
		}
		public void OnPost()
		{
			doctor.fullNames = Request.Form["fullName"];
			doctor.email = Request.Form["email"];
			doctor.phone = Request.Form["phone"];
			doctor.specialization = Request.Form["specialization"];
			string password = Request.Form["password"];
			string emails = Request.Form["email"];
			string role = "doctor";
			if (doctor.email.Length == 0 || doctor.fullNames.Length == 0 || doctor.phone.Length == 0 ||
				doctor.specialization.Length == 0)
			{
				message = "Provide all Information";
				return;
			}
			try
			{
				using (SqlConnection con = new SqlConnection(conString))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("create_doctor", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@FullName", doctor.fullNames);
						cmd.Parameters.AddWithValue("Email", doctor.email);
						cmd.Parameters.AddWithValue("@Phone", doctor.phone);
						cmd.Parameters.AddWithValue("@Specialization", doctor.specialization);
						cmd.Parameters.AddWithValue("@password", password);
						cmd.Parameters.AddWithValue("@Role", role);
						int rowsAffected = cmd.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							message = "Doctor Created";
							doctor.fullNames = "";
							doctor.email = "";
							doctor.phone = "";
							doctor.specialization = "";
							password = "";
							role = "";
							Response.Redirect("/Doctor/DoctorView");


						}
						else
						{
							message = "Doctor not created";
						}

					}
					con.Close();
				}



			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Violation of PRIMARY KEY "))
				{
					message = "There's a problem: Doctor already exists";

				}
				else
				{
					message = "There's a problem: " + ex.Message;

				}
			}

			password = "";




		}
	}
}

