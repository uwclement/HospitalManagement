using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace HospitalManagement.Pages.Patient
{
    public class PatientViewModel : PageModel
    {
        public List<PatientInfo> listPatients = new List<PatientInfo>();
        public void OnGet()
        {
            listPatients.Clear();
            try
            {
                String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlquery = "select * from patient";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientInfo patientinfo = new PatientInfo();
								patientinfo.id = ""+reader.GetInt32(0);
                                patientinfo.fname = " " + reader.GetString(1);
                                patientinfo.lname = " " + reader.GetString(2);
                                patientinfo.DOB = " " + reader.GetDateTime(3);
                                patientinfo.gender = " " + reader.GetString(4);
                                patientinfo.contact = " " + reader.GetString(5);
                                patientinfo.address = " " + reader.GetString(6);
                                patientinfo.history = " " + reader.GetString(7);
                                patientinfo.createdat = " " + reader.GetDateTime(8);

                                listPatients.Add(patientinfo);
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
    public class PatientInfo
    {
        public string id;
        public string fname;
        public string lname;
        public string DOB;
        public string gender;
        public string contact;
        public string address;
        public string history;
        public string createdat;
    }
}
