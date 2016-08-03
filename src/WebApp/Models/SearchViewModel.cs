using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Please enter a GitHub username")]
        [StringLength(30, ErrorMessage = "Please ensure GitHub username does not exceed {0}")]
        public string Username { get; set; } 
    }
}