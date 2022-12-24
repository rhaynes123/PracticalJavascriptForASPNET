using System;
using System.ComponentModel.DataAnnotations;

namespace RocketBank.DTOs
{
	public sealed record CustomerDto
	{
		public CustomerDto()
		{
		}
        public required Guid Id { get; init; } = Guid.NewGuid();
        [Required(AllowEmptyStrings = false), StringLength(100, ErrorMessage = "First Name is too long")]
        public required string FirstName { get; init; }
        [Required(AllowEmptyStrings = false), StringLength(100, ErrorMessage = "Last Name is too long")]
        public required string LastName { get; init; }
        [Required(AllowEmptyStrings = false)]
        public required string SocialSecurityNumber { get; init; }
        [Required(AllowEmptyStrings = false)]
        public required string RoutingNumber { get; init; }
        [Required(AllowEmptyStrings = false)]
        public required string AccountingNumber { get; init; }
    }
}

