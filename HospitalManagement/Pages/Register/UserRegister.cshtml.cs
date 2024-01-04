using HospitalManagement.Pages.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Register
{
    public class UserRegisterModel : PageModel
    {
        public Userinfo userInfo = new Userinfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.role = Request.Form["role"];
            userInfo.password = Request.Form["password"];
            userInfo.createdat = Request.Form["createdat"];
            if (userInfo.name.Length == 0 || userInfo.email.Length == 0 || userInfo.phone.Length == 0
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
                    String sqlquery = "insert into users(name,email,phone,role,password) values(@name,@email,@phone,@role,@password)";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@name", userInfo.name);
                        cmd.Parameters.AddWithValue("@email", userInfo.email);
                        cmd.Parameters.AddWithValue("@phone", userInfo.phone);
                        cmd.Parameters.AddWithValue("@role", userInfo.role);
                        cmd.Parameters.AddWithValue("@password", userInfo.password);


                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            userInfo.name = "";
            userInfo.email = "";
            userInfo.phone = "";
            userInfo.role = "";
            userInfo.password = "";
            userInfo.createdat = "";

            successMessage = "User Added";
            Response.Redirect("/Register/UserView");
        }
    }
}
