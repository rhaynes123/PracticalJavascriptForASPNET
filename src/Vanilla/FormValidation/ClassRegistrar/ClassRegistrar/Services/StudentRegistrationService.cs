using System;
using ClassRegistrar.Models;
using ClassRegistrar.Models.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ClassRegistrar.Services
{
    #region Helpful Links
    /*
     * https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-7.0&tabs=visual-studio-mac
     * https://dev.to/sonyarianto/how-to-spin-mongodb-server-with-docker-and-docker-compose-2lef
     * https://www.bmc.com/blogs/mongodb-docker-container/
     * https://www.mongodb.com/try/download/shell
     * https://medium.com/@kristaps.strals/docker-mongodb-net-core-a-good-time-e21f1acb4b7b
     * https://earthly.dev/blog/mongodb-docker/
     * https://code-maze.com/csharp-catch-multiple-exceptions/
     */
    #endregion
    public sealed class StudentRegistrationService: IStudentRegistrationService
	{
        private readonly IMongoCollection<Student> _studentCollection;
        private readonly ILogger<StudentRegistrationService> _logger;
		public StudentRegistrationService(IOptionsMonitor<StudentDataStoreOptions> options
            , ILogger<StudentRegistrationService> logger)
		{
            _logger = logger;
            var mongoClient = new MongoClient(
            options.CurrentValue.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.CurrentValue.DatabaseName);

            _studentCollection = mongoDatabase.GetCollection<Student>(
                options.CurrentValue.StudentsCollectionName);
        }

        public async Task<(bool, IEnumerable<string>)> RegisterAsync(Student student)
        {
            try
            {
                await _studentCollection.InsertOneAsync(student);
                return (true, new List<string>());
            }
            catch (Exception ex) when(ex is MongoCommandException)
            {
                // This exception can provide different information so we log its actions differently
                _logger.LogWarning("Command Exception Occured Check System Authorization: {error}", ex.Message);
                return (false, new List<string>
                {
                    "Please try this operation again"
                });
            }
            catch (Exception ex) when(ex is TimeoutException)
            {
                _logger.LogWarning("Time out occurred Mongo Db may not be alive: {error}", ex.Message);
                return (false, new List<string>
                {
                    "Oops that request took too long. Please try this operation again or contact support if this continues"
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unepexcted Error Occurred: {error}",ex.Message);
                return (false, new List<string>
                {
                    "An unexpected error occurred please try your operation again or contact support if this continues"
                });
            }
        }
    }
}

