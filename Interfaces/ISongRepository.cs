using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Specification.Models;

namespace Specification.Interfaces
{
    public interface ISongRepository
    {
        IEnumerable<Song> List(ISpecification<Song> specification);
        //IQueryable<Song> List();
        Song GetById(int id);
        void Add(Song song);
        IEnumerable<string> AllArtists();
        IEnumerable<Genre> AllGenres();
        Genre GetGenreById(int id);
    }
}