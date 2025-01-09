using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DesktopApp.Data;
using DesktopApp.Models;

namespace DesktopApp.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly DesktopApp.Data.DesktopAppContext _context;

        public IndexModel(DesktopApp.Data.DesktopAppContext context)
        {
            _context = context;
        }

        public IList<Player> Player { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Player = await _context.Player
                .Include(p => p.Team).ToListAsync();
        }
    }
}
