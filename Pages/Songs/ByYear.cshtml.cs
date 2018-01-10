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
    public class ByYearModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly ISongRepository _songRepository;

        public int Year { get; private set; }
        
        public ByYearModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _songRepository = new SongRepository(dbContext);
        }

        public IEnumerable<Song> Songs => _songRepository.List(new SongYearSpecification(Year));
        
        public IActionResult OnGet(int? year)
        {
            if (year == null)
            {
                return NotFound();
            }

            Year = year ?? 0;
            return Page();
        }
    }
}
