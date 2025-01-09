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
    public class IndexModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public IndexModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        public IList<Feedback> Feedback { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Feedback = await _context.Feedback
                .Include(f => f.Player)
                .Include(f => f.TrainingSession).ToListAsync();
        }
    }
}
