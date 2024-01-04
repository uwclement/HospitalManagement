using HospitalManagement.Pages.Appointment;
using HospitalManagement.Pages.Register.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

    namespace HospitalManagement.Pages.Service
    {
		public class ServiceCreateModel : PageModel
        {
       
            public Serviceinfo serviceinfo = new Serviceinfo();
            public String errorMessage = "";
            public String successMessage = "";
            public List<Serviceinfo> servicelist = new List<Serviceinfo>();

			public List<Serviceinfo> servicelist2 = new List<Serviceinfo>();
		public void OnGet()
            {
                try
                {
                    servicelist.Clear();
                    String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        String sqlquery = "select * from doctors ";
                        using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
								Serviceinfo serviceinfo = new Serviceinfo();
								serviceinfo.docid = "" + reader.GetInt32(0);
								serviceinfo.fullNames = reader.GetString(1);

								servicelist.Add(serviceinfo);
							}
                            }

                        }
				
					String sqlqueryy = "select * from Department ";
					using (SqlCommand cmd = new SqlCommand(sqlqueryy, con))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Serviceinfo serviceinfo = new Serviceinfo();
								serviceinfo.docid = "" + reader.GetInt32(0);
								serviceinfo.fullNames = reader.GetString(1);

								servicelist2.Add(serviceinfo);
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


            public void OnPost()
            {
                serviceinfo.servicename = Request.Form["servicename"];
                serviceinfo.department = Request.Form["department"];
                serviceinfo.status = Request.Form["status"];
                serviceinfo.doctor = Request.Form["doctor"];
                if (serviceinfo.servicename.Length == 0 || serviceinfo.department.Length == 0
                    || serviceinfo.doctor.Length == 0)
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
                        String sqlquery = "create_service";
                        using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@servicename", serviceinfo.servicename);
                            cmd.Parameters.AddWithValue("@Department", serviceinfo.department);
                            cmd.Parameters.AddWithValue("@status", serviceinfo.status);
                            cmd.Parameters.AddWithValue("@doctor", serviceinfo.doctor);
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }
                serviceinfo.servicename = "";
                serviceinfo.department = "";
                serviceinfo.doctor = "";


                successMessage = "Service Added";
                Response.Redirect("/Service/ServiceView");
            }
        }

		
	}
