using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Email
{
	public class EmaillMessage
	{

		[Required]
		public string To { get; set; }
		[Required]
		public string Subject { get; set; }
		[Required]
		public string email { get; set; }

		[Required]
		public string Body { get; set; }
	}
}
