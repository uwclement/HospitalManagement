using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Register.Doctor
{
    public class DoctorViewModel : PageModel
    {

        
        public List<Doctorinfo> servicelist = new List<Doctorinfo>();
        public void OnGet()
        {
            servicelist.Clear();
            try
            {
                String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlquery = "select * from doctors";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Doctorinfo doctorinfo = new Doctorinfo();
                                doctorinfo.id = "" + reader.GetInt32(0);
                                doctorinfo.names = reader.GetString(1);
                                doctorinfo.email = reader.GetString(2);
                                doctorinfo.phone = reader.GetString(3);
                                doctorinfo.specialization = reader.GetString(4);

                                servicelist.Add(doctorinfo);
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
    public class Doctorinfo
    {
        public string id;
        public string names;
        public string email;
        public string phone;
        public string specialization;


    }
}

