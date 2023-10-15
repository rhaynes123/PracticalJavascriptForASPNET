using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillsTracker.Data;
using BillsTracker.Models;

namespace BillsTracker.Pages
{
    public class BillsModel : PageModel
    {
        private readonly BillsTracker.Data.ApplicationDbContext _context;

        public BillsModel(BillsTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Bill> Bill { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Bills != null)
            {
                Bill = await _context.Bills.ToListAsync();
            }
        }
    }
}
