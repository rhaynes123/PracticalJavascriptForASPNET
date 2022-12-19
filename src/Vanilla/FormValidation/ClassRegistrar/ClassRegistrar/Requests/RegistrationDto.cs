using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassRegistrar.Requests
{
	public sealed record RegistrationDto
	{
		public RegistrationDto()
		{
		}
		[JsonPropertyName("firstName"), Required(AllowEmptyStrings = false), RegularExpression("^[a-zA-Z ]*$")]
		public required string FirstName { get; init; }
		[JsonPropertyName("lastName"), Required(AllowEmptyStrings = false), RegularExpression("^[a-zA-Z ]*$")]
		public required string LastName { get; init; }
		[JsonPropertyName("registrationDate")]
		public required DateTime RegistrationDate { get; init; }
		[JsonPropertyName("courses")]
		public required IEnumerable<string> Courses { get; init; }

    }
}

