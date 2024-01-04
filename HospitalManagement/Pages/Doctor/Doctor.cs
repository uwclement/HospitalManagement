namespace HospitalManagement.Pages.Doctor
{
	public class Doctor
	{


		public int id { get; set; }
		public string? fullNames { get; set; }
		public string? email { get; set; }
		public string? phone { get; set; }
		public string? specialization { get; set; }

		public Doctor()
		{

		}

		public Doctor(string? fullNames, string? email, string? phone, string? specialization)
		{
			this.fullNames = fullNames;
			this.email = email;
			this.phone = phone;
			this.specialization = specialization;
		}

		public Doctor(int id, string? fullNames, string? email, string? phone, string? specialization)
		{
			this.id = id;
			this.fullNames = fullNames;
			this.email = email;
			this.phone = phone;
			this.specialization = specialization;
		}
	}
	
}
