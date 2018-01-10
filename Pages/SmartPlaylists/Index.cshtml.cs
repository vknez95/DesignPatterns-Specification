using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Specification.Infrastructure;
using Specification.Models;

namespace Specification.Pages.SmartPlaylists
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly SmartPlaylistRepository _smartPlaylistRepository;

        public IndexModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _smartPlaylistRepository = new SmartPlaylistRepository(dbContext);
        }

        public IEnumerable<SmartPlaylist> SmartPlaylists => _smartPlaylistRepository.List();

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
