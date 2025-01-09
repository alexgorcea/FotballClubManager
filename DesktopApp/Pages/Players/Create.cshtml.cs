using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesktopApp.Data;
using DesktopApp.Models;

namespace DesktopApp.Pages.Players
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
        ViewData["TeamId"] = new SelectList(_context.Set<Team>(), "TeamId", "Name");
            return Page();
        }

        [BindProperty]
        public Player Player { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Player.Add(Player);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
