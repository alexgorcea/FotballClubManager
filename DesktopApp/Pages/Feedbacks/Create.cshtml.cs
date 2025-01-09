using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesktopApp.Data;
using DesktopApp.Models;

namespace DesktopApp.Pages.Feedbacks
{
    public class CreateModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public CreateModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var completedSessions = _context.Set<TrainingSession>()
            .Where(ts => ts.ScheduledDate <= DateTime.Now)
            .ToList();

            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "FullName");
            ViewData["TrainingSessionId"] = new SelectList(
                completedSessions.Select(ts => new {
                    TrainingSessionId = ts.TrainingSessionId,
                    DisplayValue = ts.ScheduledDate.ToString("yyyy-MM-dd") // Format: 2025-01-09
                }),
                "TrainingSessionId",
                "DisplayValue"
            );
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Feedback.Add(Feedback);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
