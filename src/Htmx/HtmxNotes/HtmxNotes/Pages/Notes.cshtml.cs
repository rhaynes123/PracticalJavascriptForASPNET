using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HtmxNotes.Data;
using HtmxNotes.Notes;
/*
 * https://htmx.org
 * https://xakpc.info/htmx-dotnet
 * https://htmx.org/server-examples/
 * https://blog.pagesd.info/2021/12/24/use-htmx-with-asp-net-core-mvc/
 * https://blog.jetbrains.com/dotnet/2022/04/27/htmx-for-asp-net-core-developers-tutorial/
 * https://www.youtube.com/watch?v=uS6m37jhdqM
 * https://surferjeff.medium.com/convert-an-asp-net-website-into-a-spa-using-htmx-1274ae0d8be8
 */

namespace HtmxNotes.Pages
{
    public class NotesModel : PageModel
    {
        private readonly HtmxNotes.Data.ApplicationDbContext _context;

        public NotesModel(HtmxNotes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Note NewNote { get; set; } = default!;

        public IList<Note> Notes { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Note != null)
            {
                Notes = await _context.Note.ToListAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSave()
        {
            await _context.Note.AddAsync(NewNote);
            await _context.SaveChangesAsync();
            // This took hours to figure out but the notes collection is null OnPost
            var notes = await _context.Note.ToListAsync();
            return Partial("_Notes", notes);
        }

        public async Task<IActionResult> OnPostClear()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Note;");
            var notes = await _context.Note.ToListAsync();
            return Partial("_Notes", notes);
        }
    }
}
