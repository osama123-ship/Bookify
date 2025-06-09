using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Models
{
    public class Company
    {
        public Company()
        {
            
        }
        public int Id { get; set; }
       
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string? CompanyName { get; set; }
        public string? Website { get; set; }
        public ICollection<Event> Events { get; set;}
    }
}
