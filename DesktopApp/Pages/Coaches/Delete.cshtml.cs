using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DesktopApp.Data;
using DesktopApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DesktopApp.Pages.Coaches
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public DeleteModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach.FindAsync(id);
            if (coach != null)
            {
                Coach = coach;
                _context.Coach.Remove(Coach);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
