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
    public class DetailsModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public DetailsModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        public TrainingSession TrainingSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingsession = await _context.TrainingSession
                .Include(p => p.Coach)
                .Include(n => n.Team)
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
    }
}
