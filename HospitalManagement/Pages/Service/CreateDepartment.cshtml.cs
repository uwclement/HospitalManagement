using HospitalManagement.Pages.Register;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Service
{
    public class CreateDepartmentModel : PageModel
    {
        public Deptinfo deptinfo = new Deptinfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
           deptinfo.deptname = Request.Form["deptname"];
           deptinfo.hod = Request.Form["hod"];
            if (deptinfo.deptname.Length == 0 ||deptinfo.hod.Length == 0 
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
                    String sqlquery = "insert into Department(DepartmentName,hod) values(@name,@hod)";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@name",deptinfo.deptname);
                        cmd.Parameters.AddWithValue("@hod",deptinfo.hod);

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
           deptinfo.deptname = "";
           deptinfo.hod = "";
     

            successMessage = "Depatment Added";
            Response.Redirect("/Service/Department");
        }
    }
}
