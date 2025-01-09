using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesktopApp.Data;
using DesktopApp.Models;

namespace DesktopApp.Pages.TrainingSessions
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
        ViewData["CoachId"] = new SelectList(_context.Coach, "CoachId", "FullName");
        ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "Name");
            return Page();
        }

        [BindProperty]
        public TrainingSession TrainingSession { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TrainingSession.Add(TrainingSession);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
