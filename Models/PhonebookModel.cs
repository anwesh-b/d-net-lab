using System.ComponentModel.DataAnnotations;

namespace MvcMoviw.Models
{
    public class Phonebook
    {
        public int id {get; set;}

        [Required]
        [MinLength(3)]
        public string name { get; set; } = "";

        public string number { get; set; } = "";
        
    }
}