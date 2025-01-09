using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesktopApp.Data;
using DesktopApp.Models;

namespace DesktopApp.Pages.TrainingSessions
{
    public class EditModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public EditModel(DesktopApp.Data.DesktopAppContext context)
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

            var trainingsession =  await _context.TrainingSession.FirstOrDefaultAsync(m => m.TrainingSessionId == id);
            if (trainingsession == null)
            {
                return NotFound();
            }
            TrainingSession = trainingsession;
           ViewData["CoachId"] = new SelectList(_context.Coach, "CoachId", "FullName");
           ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TrainingSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingSessionExists(TrainingSession.TrainingSessionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TrainingSessionExists(int id)
        {
            return _context.TrainingSession.Any(e => e.TrainingSessionId == id);
        }
    }
}
