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
    public class ByArtistModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly ISongRepository _songRepository;

        public string Artist { get; private set; }
        
        public ByArtistModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _songRepository = new SongRepository(dbContext);
        }

        public IEnumerable<Song> Songs => _songRepository.List(new SongArtistSpecification(Artist));
        
        public IActionResult OnGet(string artist)
        {
            if (string.IsNullOrEmpty(artist))
            {
                return NotFound();
            }

            Artist = artist;
            return Page();
        }
    }
}
