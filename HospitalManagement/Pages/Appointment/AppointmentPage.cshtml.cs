using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Appointment
{
    public class AppointmentPageModel : PageModel
    {
            public List<Serviceinfo> servicelist = new List<Serviceinfo>();
        //public string userEmail;
        public string userName;
        public void OnGet()
            {
            //userEmail = HttpContext.Session.GetString("email");
            userName = HttpContext.Session.GetString("fullname");
            servicelist.Clear();
                try
                {
                    String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        String sqlquery = "select * from service where Status='Available'";
                        using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Serviceinfo serviceinfo = new Serviceinfo();
                                    serviceinfo.id = "" + reader.GetInt32(0);
                                    serviceinfo.servicename = reader.GetString(1);
                                    //serviceinfo.deptid = reader.GetString(2);
                                    serviceinfo.status = reader.GetString(3);
                                    //serviceinfo.doct = reader.GetString(4);

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
            public string deptid;
            public string status;
            public string location;
            public string name;
            public string email;
            public string history;

        }
    }

