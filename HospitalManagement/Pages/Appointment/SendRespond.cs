using Microsoft.Build.Framework;

namespace HospitalManagement.Pages.Appointment
{
    public class SendRespond
    {

        [Required]
        public string To { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string email { get; set; }

        [Required]
        public string Body { get; set; }
        [Required]
        public string Appointment { get; set; }
    }
}
