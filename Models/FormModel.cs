using System.ComponentModel.DataAnnotations;

namespace MvcMoviw.Models
{
    public class Form
    {
        // public string FirstName {get;set;}
        // public string LastName {get; set}
        [Required]
        [MinLength(3)]
        public string UserName { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string Password { get; set; } = "";
        
    }
}