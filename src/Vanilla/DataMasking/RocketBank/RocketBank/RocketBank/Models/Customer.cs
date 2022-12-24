using System;
using System.ComponentModel.DataAnnotations;

namespace RocketBank.Models
{
	public sealed class Customer
	{
		public required Guid Id { get; set; } = Guid.NewGuid();
		[Required(AllowEmptyStrings = false), StringLength(100, ErrorMessage ="First Name is too long")]
		public required string FirstName { get; set; }
        [Required(AllowEmptyStrings = false), StringLength(100, ErrorMessage = "Last Name is too long")]
        public required string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string SocialSecurityNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string RoutingNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string AccountingNumber { get; set; }
	}
}

