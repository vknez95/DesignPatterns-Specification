using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Specification.Infrastructure;
using Specification.Interfaces;
using Specification.Models;
using Specification.Models.Shared;
using Specification.Models.Specs;

namespace Specification.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly ISongRepository _songRepository;
        private readonly SmartPlaylistRepository _playlistRepo;

        public IndexModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _songRepository = new SongRepository(dbContext);
            _playlistRepo = new SmartPlaylistRepository(_dbContext);
        }

        public List<Song> Songs => _songRepository.List(specification).ToList();
    
        public IEnumerable<int> Ratings => new[] { 1, 2, 3, 4, 5 };

        private Lazy<List<CheckModel>> _artists;
        [BindProperty]
        public List<CheckModel> Artists
        {
            get
            {
                if (_artists == null || _artists.Value.Count == 0)
                {
                    _artists = new Lazy<List<CheckModel>>
                    (
                        () => _songRepository
                                .AllArtists()
                                .Select(a => new CheckModel() { Id = a.GetHashCode(), Name = a, Checked = false })
                                .ToList()
                    );
                }
                return _artists.Value;
            }
            set
            {
                _artists = new Lazy<List<CheckModel>>(() => value);
            }
        }

        private Lazy<List<CheckModel>> _genres;
        [BindProperty]
        public List<CheckModel> Genres
        {
            get
            {
                if (_genres == null || _genres.Value.Count == 0)
                {
                    _genres = new Lazy<List<CheckModel>>
                    (
                        () => _songRepository
                                .AllGenres()
                                .Select(g => new CheckModel() { Id = g.Id, Name = g.Name, Checked = false })
                                .ToList()
                    );
                }
                return _genres.Value;
            }
            set
            {
                _genres = new Lazy<List<CheckModel>>(() => value);
            }
        }

        [BindProperty]
        public int MinRating { get; set; }

        [BindProperty]
        public string TitleSearch { get; set; }

        [BindProperty]
        public SmartPlaylist SmartPlaylist { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(string filter = null, string save = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (save != null)
            {
                SmartPlaylist.Specification = specification;
                _playlistRepo.Add(SmartPlaylist);
                return RedirectToPage("/SmartPlaylists/Index");
            }

            return Page();
        }

        private GlobalSongSpecification specification
        {
            get
            {
                var spec = new GlobalSongSpecification();

                if (Artists.Any(a => a.Checked))
                {
                    spec.ArtistsToInclude.AddRange(Artists.Where(a => a.Checked).Select(a => a.Name));
                }
                else
                {
                    spec.ArtistsToInclude.AddRange(Artists.Select(a => a.Name));
                }

                if (Genres.Any(g => g.Checked))
                {
                    spec.GenreIdsToInclude.AddRange(Genres.Where(g => g.Checked).Select(g => g.Id));
                }
                else
                {
                    spec.GenreIdsToInclude.AddRange(Genres.Select(g => g.Id));
                }

                spec.MinRating = MinRating;
                spec.TitleFilter = TitleSearch;

                return spec;
            }
        }
    }
}
