using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Specification.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1), Required]
        public string Name { get; set; }

        public ICollection<GenreAssignment> GenreAssignments { get; set; }
    }
}