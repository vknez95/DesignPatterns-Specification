using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Specification.Models
{
    public class Song
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Artist { get; set; }
        public TimeSpan Length { get; set; }
        public int Year { get; set; }
        public int AlbumId { get; set; }
        public int? Rating { get; set; }
        public Album Album { get; set; }
        public List<GenreAssignment> GenreAssignments { get; set; }

        [NotMapped]
        public IEnumerable<String> Genres => GenreAssignments.Select(g => g.Genre.Name);

        public bool IsPreferred()
        {
            return Rating.HasValue && Rating.Value >= 4;
        }

        public bool IsRecentFavorite()
        {
            if (DateTime.Now.Year - this.Year > 5) return false;

            return IsPreferred();
        }
    }
}