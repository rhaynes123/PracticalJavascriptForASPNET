using System;
using System.ComponentModel.DataAnnotations;

namespace BillsTracker.Models
{
	public class Bill
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		public required string Name { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public required bool Paid { get; set; }
	}
}

