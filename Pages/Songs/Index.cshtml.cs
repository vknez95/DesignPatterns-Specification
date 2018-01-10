using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Specification.Infrastructure;
using Specification.Interfaces;
using Specification.Models;
using Specification.Models.Specs;

namespace Specification.Pages.Songs
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly ISongRepository _songRepository;

        public IndexModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _songRepository = new SongRepository(dbContext);
        }

        public IEnumerable<Song> Songs => _songRepository.List(new AllSongsSpecification());
        
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
