using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Specification.Infrastructure;
using Specification.Models;

namespace Specification.Pages.SmartPlaylists
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly SmartPlaylistRepository _smartPlaylistRepository;

        public DeleteModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _smartPlaylistRepository = new SmartPlaylistRepository(dbContext);
        }

        public SmartPlaylist SmartPlaylist { get; private set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SmartPlaylist = _smartPlaylistRepository.FindById(id ?? 0);

            if (SmartPlaylist == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SmartPlaylist = _smartPlaylistRepository.FindById(id ?? 0);

            if (SmartPlaylist != null)
            {
                _smartPlaylistRepository.Remove(SmartPlaylist);
            }

            return RedirectToPage("./Index");
        }
    }
}
