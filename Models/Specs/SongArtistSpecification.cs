using System;
using System.Linq.Expressions;
using Specification.Interfaces;

namespace Specification.Models.Specs
{
    public class SongArtistSpecification : ISpecification<Song>
    {
        public string Artist { get; set; }

        public SongArtistSpecification(string artist)
        {
            Artist = artist;
        }

        public Expression<Func<Song, bool>> Criteria
        {
            get { return s => s.Artist.ToLower() == Artist.ToLower(); }
        }
    }
}