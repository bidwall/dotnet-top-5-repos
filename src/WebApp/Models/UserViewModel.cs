using System.Collections.Generic;
using Models;

namespace WebApp.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string AvatarUrl { get; set; }
        public IEnumerable<Repo> Repos { get; set; }
    }
}