using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Globalization;

namespace HospitalManagement.Pages.Service
{
    public class SessionCreateModel : PageModel
    {
        public SessionInfo sessioninfo = new SessionInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String serviceName = Request.Query["servicename"];
        }

        public void OnPost()
        {
            
            sessioninfo.doctor = Request.Form["doctor"];
            sessioninfo.date = Request.Form["date"];
            sessioninfo.session= Request.Form["session"];
            sessioninfo.status = Request.Form["status"];
            if (sessioninfo.doctor.Length == 0 || sessioninfo.session.Length == 0
            )

            {
                errorMessage = "All Fields are Required";
                return;
            }
			//save data
			String serviceName = Request.Query["servicename"];
			String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlquery = $"insert into {serviceName} (doctor,date,session,status) values(@doctor,@date,@session,@status)";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@doctor", sessioninfo.doctor);
                        cmd.Parameters.AddWithValue("@date", sessioninfo.date);
                        cmd.Parameters.AddWithValue("@session", sessioninfo.session);
                        cmd.Parameters.AddWithValue("@status", sessioninfo.status);

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }



            successMessage = "Session Added";
            Response.Redirect("/Service/AddSession");
        }
    }


    public class SessionInfo {

        public string doctor;
        public string session;
        public string date;
        public string status;
    }
}
