using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RocketBank.Data;
using RocketBank.DTOs;
using RocketBank.Models;

namespace RocketBank.Pages
{
    public class AllCustomersModel : PageModel
    {
        private readonly RocketBank.Data.ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AllCustomersModel(RocketBank.Data.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<CustomerDto> Customers { get;set; } = new List<CustomerDto>();

        public async Task OnGetAsync()
        {
            if (_context.Customers != null)
            {
                var customers = await _context.Customers.ToListAsync();
                Customers = _mapper.Map<IList<CustomerDto>>(customers);
            }
        }
    }
}
