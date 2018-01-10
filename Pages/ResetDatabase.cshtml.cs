using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Specification.Infrastructure;

namespace Specification.Pages
{
    public class ResetDatabaseModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        public ResetDatabaseModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult OnGet()
        {
            _dbContext.Initialize();
            return Page();
        }
    }
}
