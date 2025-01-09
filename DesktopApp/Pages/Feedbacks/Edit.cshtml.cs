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

namespace DesktopApp.Pages.Feedbacks
{
    public class EditModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public EditModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback
                .Include(f => f.TrainingSession)
                .ThenInclude(ts => ts.Team)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);

            if (feedback == null)
            {
                return NotFound();
            }

            Feedback = feedback;

            
            var players = await _context.Player
                .Where(p => p.TeamId == feedback.TrainingSession.TeamId)
                .ToListAsync();

            ViewData["PlayerId"] = new SelectList(players, "PlayerId", "FullName", Feedback.PlayerId);

            var completedSessions = await _context.Set<TrainingSession>()
                .Where(ts => ts.ScheduledDate <= DateTime.Now)
                .ToListAsync();
            ViewData["TrainingSessionId"] = new SelectList(
                completedSessions.Select(ts => new
                {
                    TrainingSessionId = ts.TrainingSessionId,
                    DisplayValue = ts.ScheduledDate.ToString("yyyy-MM-dd")
                }),
                "TrainingSessionId",
                "DisplayValue",
                Feedback.TrainingSessionId
            );

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

            _context.Attach(Feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(Feedback.FeedbackId))
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

        public async Task<JsonResult> OnGetPlayersByTrainingSessionAsync(int trainingSessionId)
        {
            var teamId = await _context.TrainingSession
                .Where(ts => ts.TrainingSessionId == trainingSessionId)
                .Select(ts => ts.TeamId)
                .FirstOrDefaultAsync();

            if (teamId == null)
            {
                return new JsonResult(new { success = false, message = "Invalid Training Session." });
            }

            var players = await _context.Player
                .Where(p => p.TeamId == teamId)
                .Select(p => new
                {
                    p.PlayerId,
                    p.FullName
                })
                .ToListAsync();

            return new JsonResult(new { success = true, players });
        }


        private bool FeedbackExists(int id)
        {
            return _context.Feedback.Any(e => e.FeedbackId == id);
        }
    }
}
