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
    public class RecentFavouritesModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly ISongRepository _songRepository;

        public RecentFavouritesModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _songRepository = new SongRepository(dbContext);
        }

        public IEnumerable<Song> Songs => _songRepository.List(new AllSongsSpecification())
                                                         .Where(s => s.IsRecentFavorite());

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
