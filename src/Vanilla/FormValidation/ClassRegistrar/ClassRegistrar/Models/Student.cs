using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ClassRegistrar.Requests;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ClassRegistrar.Models
{
	public sealed class Student
	{
		public Student()
		{
		}
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required, RegularExpression("^[a-zA-Z ]*$")]
        public required string FirstName { get; set; }
        [Required, RegularExpression("^[a-zA-Z ]*$")]
        public required string LastName { get; set; }
        [JsonPropertyName("registrationDate")]
        public required DateTime RegistrationDate { get; set; }
        [BsonElement("courses")]
        public required IEnumerable<string> Courses { get; init; }

        public static explicit operator Student(RegistrationDto dto)
        {
            return new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RegistrationDate = dto.RegistrationDate,
                Courses = dto.Courses
            };
        }
    }
}

