using System.ComponentModel.DataAnnotations;

namespace MvcMoviw.Models
{
    public class Todo
    {
        // public string FirstName {get;set;}
        // public string LastName {get; set}

        public int id {get; set;}

        [Required]
        [MinLength(3)]
        public string title { get; set; } = "";

        public string description { get; set; } = "";
        
    }
}