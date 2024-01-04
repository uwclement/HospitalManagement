using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Patient
{
    public class PatientCreateModel : PageModel
    {
        public PatientInfo patientinfo = new PatientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
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
            //save data
            String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlquery = "insert into patient(first_name,last_name,date_of_birth,gender,contact_number,address,medical_history) values(@first_name,@last_name,@date_of_birth,@gender,@contact_number,@address,@medical_history)";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
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
            patientinfo.fname = "";
            patientinfo.lname = "";
            patientinfo.contact = "";
            patientinfo.gender = "";
            patientinfo.DOB = "";
            patientinfo.address = "";
            patientinfo.history = "";
            patientinfo.createdat = "";

            successMessage = "Petient Added";
            Response.Redirect("/Patient/PatientView");
        }
    }
}
