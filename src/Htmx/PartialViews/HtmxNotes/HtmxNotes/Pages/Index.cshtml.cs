using HtmxNotes.Notes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HtmxNotes.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Data.ApplicationDbContext _context;

    public IndexModel(ILogger<IndexModel> logger, Data.ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IList<Note> Notes { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Note != null)
        {
            Notes = await _context.Note.ToListAsync();
        }
    }
}

