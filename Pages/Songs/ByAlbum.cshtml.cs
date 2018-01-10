using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Specification.Infrastructure;
using Specification.Interfaces;
using Specification.Models;
using Specification.Models.Specs;

namespace Specification.Pages.Songs
{
    public class ByAlbumModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly ISongRepository _songRepository;
        private int _albumId;

        public string Album
        {
            get
            {
                if (Songs.Count() == 0)
                {
                    return "";
                }
                else
                {
                    return Songs.First().Album.Title;
                }
            }
        }
        
        public ByAlbumModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _songRepository = new SongRepository(dbContext);
        }

        private Lazy<IEnumerable<Song>> _songs;
        public IEnumerable<Song> Songs
        {
            get
            {
                if (_songs == null)
                {
                    _songs = new Lazy<IEnumerable<Song>>(
                        () => _songRepository.List(new SongAlbumSpecification(_albumId)));
                }

                return _songs.Value;
            }
        }
        
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _albumId = id ?? 0;

            if (string.IsNullOrEmpty(Album))
            {
                return NotFound();
            }

            return Page();
        }
    }
}
