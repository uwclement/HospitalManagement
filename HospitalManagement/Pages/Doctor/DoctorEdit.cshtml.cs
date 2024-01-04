using HospitalManagement.Pages.Register.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace HospitalManagement.Pages.Doctor
{
    public class DoctorEditModel : PageModel
    {
        public Doctorinfo doctorinfo = new Doctorinfo();
        public List<Doctorinfo> servicelist = new List<Doctorinfo>();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            servicelist.Clear();
            try
            {
                String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlquery = "select * from doctors where docId =@id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
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
        public string message = "";
        public Doctor doctor = new Doctor();

        public void OnPost()
        {
            doctor.fullNames = Request.Form["fullName"];
            doctor.email = Request.Form["email"];
            doctor.phone = Request.Form["phone"];
            doctor.specialization = Request.Form["specialization"];
            //string password = Request.Form["password"];
            //string emails = Request.Form["email"];
            //string role = "doctor";
            if (doctor.email.Length == 0 || doctor.fullNames.Length == 0 || doctor.phone.Length == 0 ||
                doctor.specialization.Length == 0)
            {
                message = "Provide all Information";
                return;
            }
            try
            {
                String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE doctors set fullName=@fullName, email=@Email, phone=@phone, specialization=@Specialization WHERE docId=@id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", doctorinfo.id);
                        cmd.Parameters.AddWithValue("@FullName", doctor.fullNames);
                        cmd.Parameters.AddWithValue("Email", doctor.email);
                        cmd.Parameters.AddWithValue("@Phone", doctor.phone);
                        cmd.Parameters.AddWithValue("@Specialization", doctor.specialization);
                        //cmd.Parameters.AddWithValue("@password", password);
                        //cmd.Parameters.AddWithValue("@Role", role);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Doctorinfo doctorinfo = new Doctorinfo();
                            message = "Doctor Updated";
                            doctor.fullNames = "";
                            doctor.email = "";
                            doctor.phone = "";
                            doctor.specialization = "";
                            Response.Redirect("/Doctor/DoctorView");


                        }
                        else
                        {
                            message = "Doctor not created";
                        }

                    }
                    con.Close();
                }



            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY "))
                {
                    message = "There's a problem: Doctor already exists";

                }
                else
                {
                    message = "There's a problem: " + ex.Message;

                }
            }
        }
    }
}