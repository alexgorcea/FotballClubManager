using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DesktopApp.Data;
using DesktopApp.Models;

namespace DesktopApp.Pages.TrainingSessions
{
    public class DeleteModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public DeleteModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TrainingSession TrainingSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingsession = await _context.TrainingSession
                .Include(n => n.Team)
                .Include(p => p.Coach)
                .FirstOrDefaultAsync(m => m.TrainingSessionId == id);

            if (trainingsession == null)
            {
                return NotFound();
            }
            else
            {
                TrainingSession = trainingsession;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingsession = await _context.TrainingSession.FindAsync(id);
            if (trainingsession != null)
            {
                TrainingSession = trainingsession;
                _context.TrainingSession.Remove(TrainingSession);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
