using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Service
{
    public class AddSessionModel : PageModel
    {
        public Serviceinfo serviceinfo = new Serviceinfo();
        public String errorMessage = "";
        public String successMessage = "";
        public List<Serviceinfo> servicelist = new List<Serviceinfo>();
        public string serviceName;
        public void OnGet()
        {
             serviceName = Request.Query["servicename"];
            try
            {
                servicelist.Clear();
                String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlquery = $"SELECT * FROM {serviceName}";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Serviceinfo serviceinfo = new Serviceinfo();
                                serviceinfo.id = "" + reader.GetInt32(0);
                                serviceinfo.doctor = "" + reader.GetInt32(1);
								serviceinfo.date = reader.GetDateTime(2).ToString("yyyy-MM-dd"); 
								serviceinfo.session = reader.GetTimeSpan(3).ToString();
								serviceinfo.status = "" + reader.GetString(4);

                                servicelist.Add(serviceinfo);
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

    }
}

