using System;
using ClassRegistrar.Models;

namespace ClassRegistrar.Services
{
	public interface IStudentRegistrationService
	{
		Task<(bool, IEnumerable<string>)> RegisterAsync(Student student);
	}
}

