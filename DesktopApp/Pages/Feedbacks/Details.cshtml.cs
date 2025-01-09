using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DesktopApp.Data;
using DesktopApp.Models;

namespace DesktopApp.Pages.Feedbacks
{
    public class DetailsModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public DetailsModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback
                .Include(p => p.TrainingSession)
                .Include(n => n.Player)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedback == null)
            {
                return NotFound();
            }
            else
            {
                Feedback = feedback;
            }
            return Page();
        }
    }
}
