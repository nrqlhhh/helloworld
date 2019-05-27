
using System.ComponentModel.DataAnnotations;
namespace fyp.Model
{
    public class UserLogin

    {


        [Required(ErrorMessage = "Please enter NRIC")]
        [RegularExpression("[sStT][0-9]{7}[A-Za-z]", ErrorMessage = "Invalid NRIC")]

        public string NRIC { get; set; }
        public bool RememberMe { get; set; }
    }
}
