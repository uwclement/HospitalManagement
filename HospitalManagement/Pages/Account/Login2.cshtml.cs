using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Account
{
    public class Login2Model : PageModel
    {
        String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
        public string message = "";
		private readonly IHttpContextAccessor _httpContextAccessor;
		public  Login2Model([FromServices] IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
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
                                if (passwords.Equals(Password) )
                                {
                                    if (role.Equals("admin"))
                                    {
                                        Response.Redirect("/Admin/AdminView");
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
                                        Response.Redirect("/Appointment/AppointmentView");
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
        }
    }

}

