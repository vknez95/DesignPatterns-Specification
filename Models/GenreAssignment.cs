using System;
using System.Collections.Generic;
using Specification.Infrastructure;
using Specification.Interfaces;

namespace Specification.Models
{
    public class GenreAssignment
    {
        public int SongId { get; set; }
        public int GenreId { get; set; }
        public Song Song { get; set; }
        public Genre Genre { get; set; }
    }
}