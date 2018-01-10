using System.ComponentModel.DataAnnotations;

namespace Specification.Models
{
    public class Album
    {
        public int Id { get; set; }
        
        [StringLength(100, MinimumLength = 1), Required]
        public string Title { get; set; }
        
        [StringLength(100, MinimumLength = 1), Required]
        public string Artist { get; set; }
        public int Year { get; set; }
    }
}