using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Service
{
    public class ServiceViewModel : PageModel
    {
        public List<Serviceinfo> servicelist = new List<Serviceinfo>();
        public void OnGet()
        {
            servicelist.Clear();
            try
            {
                String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlquery = "select * from service ";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Serviceinfo serviceinfo = new Serviceinfo();
                                serviceinfo.id = "" + reader.GetInt32(0);
                                serviceinfo.servicename = reader.GetString(1);
                                serviceinfo.department = ""+reader.GetString(2);
                                serviceinfo.status = reader.GetString(3);
                                serviceinfo.doctor = " "+reader.GetInt32(4);

                                servicelist.Add(serviceinfo);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception:" + ex.Message);
            }
        }
    }
    public class Serviceinfo
    {
        public string id;
        public string servicename;
        public string department;
        public string status;
        public string doctor;
        public string session;
        public string date;
		public string docid;
		public string fullNames;
	}
}

