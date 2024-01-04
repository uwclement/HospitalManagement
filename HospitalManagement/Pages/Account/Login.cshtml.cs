using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credetial credetial { get; set; }
        public void OnGet()
        {
            this.credetial = new Credetial { Username = "Admin" };
        }
        public void OnPost ()
        {
            Response.Redirect("/Index");
        }
        public class Credetial
        {
            [Required]
            [Display(Name ="User Name")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
