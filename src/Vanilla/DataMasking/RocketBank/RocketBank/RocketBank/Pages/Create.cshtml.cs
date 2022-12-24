using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RocketBank.Data;
using RocketBank.DTOs;
using RocketBank.Models;

namespace RocketBank.Pages
{
    public class CreateModel : PageModel
    {
        private readonly RocketBank.Data.ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateModel(RocketBank.Data.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CustomerDto Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Customers == null || Customer == null)
            {
                return Page();
            }
            Customer customer = _mapper.Map<Customer>(Customer);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
