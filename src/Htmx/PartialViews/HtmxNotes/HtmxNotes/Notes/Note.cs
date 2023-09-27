using System;
using System.ComponentModel.DataAnnotations;

namespace HtmxNotes.Notes
{
	public class Note
	{
		[Required, Key]
		public Guid Id { get; set; }
		[Required, StringLength(255)]
		public required string Text { get; set; }
		[Required]
		public required DateTime CreatedDate { get; set; } = DateTime.Now;
	}
}

