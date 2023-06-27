using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CopyPaste.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public List<Order> Orders { get; set; }
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Orders = new List<Order>
        {
            new Order(Id:Guid.NewGuid(), "Richard", "Sandwhich", 4.99m, DateOnly.FromDateTime(DateTime.Now)),
            new Order(Id:Guid.NewGuid(), "Tim", "Sandwhich", 4.99m,DateOnly.FromDateTime(DateTime.Now)),
            new Order(Id:Guid.NewGuid(), "Kevin","Wings", 6.99m, DateOnly.FromDateTime(DateTime.Now)),
            new Order(Id:Guid.NewGuid(), "Chris", "Yogurt", 2.99m,DateOnly.FromDateTime(DateTime.Now)),
        };
    }
}

public sealed record Order(Guid Id, string CustomerName, string ItemName, decimal ItemPrice, DateOnly OrderDate);
