using System;
using System.ComponentModel.DataAnnotations;

namespace ClassRegistrar.Models.Options
{
	public sealed class StudentDataStoreOptions
	{
        [Required(AllowEmptyStrings = false)]
        public required string ConnectionString { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string DatabaseName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string StudentsCollectionName { get; set; }
    }
}

