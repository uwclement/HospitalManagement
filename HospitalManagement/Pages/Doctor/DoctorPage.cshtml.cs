using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Doctor
{
    public class DoctorPageModel : PageModel
    {
		public List<Appointmentinfo> appointmentlist = new List<Appointmentinfo>();
		public void OnGet()
		{
			appointmentlist.Clear();
			try
			{
				String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(conString))
				{
					con.Open();
					String sqlquery = "select * from appointment";
					using (SqlCommand cmd = new SqlCommand(sqlquery, con))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Appointmentinfo appointmentinfo = new Appointmentinfo();
								appointmentinfo.id = "" + reader.GetInt32(0);
								appointmentinfo.name = reader.GetString(1);
								appointmentinfo.email = reader.GetString(2);
								appointmentinfo.service = reader.GetString(3);
								appointmentinfo.history = reader.GetString(4);
								appointmentinfo.createdat = " " + reader.GetDateTime(5);
								appointmentinfo.respond = " " + reader.GetString(6);
								//appointmentinfo.reason = " " + reader.GetString(7);

								appointmentlist.Add(appointmentinfo);
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




		public class Appointmentinfo
		{
			public string id;
			public string name;
			public string email;
			public string service;
			public string history;
			public string createdat;
			public string respond;
			//public string reason;
		}
	}
}

