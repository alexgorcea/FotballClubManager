using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DesktopApp.Data;
using DesktopApp.Models;

namespace DesktopApp.Pages.Coaches
{
    public class DetailsModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public DetailsModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        public Coach Coach { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.CoachId == id);
            if (coach == null)
            {
                return NotFound();
            }
            else
            {
                Coach = coach;
            }
            return Page();
        }
    }
}
