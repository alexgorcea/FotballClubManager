using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesktopApp.Data;
using DesktopApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DesktopApp.Pages.Coaches
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public CreateModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "Name");
            return Page();
        }

        [BindProperty]
        public Coach Coach { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Coach.Add(Coach);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
