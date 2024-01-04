namespace HospitalManagement.Pages.Account
{
	public class Patient
	{
		public int id { get; set; }
		public string fullName { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public string address { get; set; }
		public Patient()
		{

		}

		public Patient(string fullName, string email, string phone, string address)
		{
			this.fullName = fullName;
			this.email = email;
			this.phone = phone;
			this.address = address;
		}

		public Patient(int id, string fullName, string email, string phone, string address)
		{
			this.id = id;
			this.fullName = fullName;
			this.email = email;
			this.phone = phone;
			this.address = address;
		}
	}
}
