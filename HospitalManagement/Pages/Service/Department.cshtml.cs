using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HospitalManagement.Pages.Service
{
    public class DepartmentModel : PageModel
    {
	
			public List<Deptinfo> deptlist = new List<Deptinfo>();
			public void OnGet()
			{
				deptlist.Clear();
				try
				{
					String conString = @"Data Source=CLEMENT\SQLEXPRESS;Initial Catalog=HealtManagementDb;Integrated Security=True";
					using (SqlConnection con = new SqlConnection(conString))
					{
						con.Open();
						String sqlquery = "select * from Department";
						using (SqlCommand cmd = new SqlCommand(sqlquery, con))
						{
							using (SqlDataReader reader = cmd.ExecuteReader())
							{
								while (reader.Read())
								{
									Deptinfo deptinfo = new Deptinfo();
									deptinfo.id = "" + reader.GetInt32(0);
									deptinfo.deptname = reader.GetString(1);
									deptinfo.hod = reader.GetString(2);

									deptlist.Add(deptinfo);
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
		public class Deptinfo
		{
			public string id;
			public string deptname;
			public string hod;

		}
	}

