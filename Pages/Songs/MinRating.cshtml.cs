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
    public class MinRatingModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly ISongRepository _songRepository;

        public int Rating { get; private set; }
        
        public MinRatingModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _songRepository = new SongRepository(dbContext);
        }

        public IEnumerable<Song> Songs => _songRepository.List(new SongRatingSpecification(Rating));
        
        public IActionResult OnGet(int? rating)
        {
            if (rating == null)
            {
                return NotFound();
            }

            Rating = rating ?? 0;
            return Page();
        }
    }
}
