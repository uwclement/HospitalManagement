using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Account
{
    public class Login3Model : PageModel
    {
        String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
        public string message = "";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Login3Model([FromServices] IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Patient patient = new Patient();
        public void onGet()
        {


        }

        public void OnPost()
        {
            String emails = Request.Form["email"];
            String passwords = Request.Form["password"];

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string query = "SELECT fullnames,email,Upassword,roles FROM accounts where email=@Email";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", emails);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fullname = reader.GetString(0);
                                string email = reader.GetString(1);
                                string Password = reader.GetString(2);
                                string role = reader.GetString(3);
                                if (passwords.Equals(Password))
                                {
                                    if (role.Equals("admin"))
                                    {
                                        Response.Redirect("/Doctor/DoctorView");
                                    }
                                    else if (role.Equals("patient"))
                                    {

                                        string Email = emails;
                                        _httpContextAccessor.HttpContext.Session.SetString("email", Email);
                                        _httpContextAccessor.HttpContext.Session.SetString("fullname", fullname);
                                        Response.Redirect("/Appointment/AppointmentPage");
                                    }
                                    else if (role.Equals("doctor"))
                                    {
                                        Response.Redirect("/Doctor/DoctorView");
                                    }
                                }
                                else
                                {
                                    message = "Invalid Credentials";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }



            patient.fullName = Request.Form["fullName"];
            patient.email = Request.Form["email"];
            patient.phone = Request.Form["phone"];
            patient.address = Request.Form["address"];
            string password = Request.Form["password"];
            string roles = "patient";
            if (patient.email.Length ==0)
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

