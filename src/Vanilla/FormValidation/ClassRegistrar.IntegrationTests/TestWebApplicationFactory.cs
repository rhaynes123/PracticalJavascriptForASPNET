using System;
using ClassRegistrar.Models.Options;
using ClassRegistrar.Services;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ClassRegistrar.IntegrationTests
{
    #region Helpful Links
    /*
     * https://www.youtube.com/watch?v=VgStKMB1duY
     * https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api#a-custom-and-reusable-xunit-fixture
     * https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
     * https://andrewlock.net/exploring-dotnet-6-part-6-supporting-integration-tests-with-webapplicationfactory-in-dotnet-6/
     * https://code-maze.com/aspnet-core-integration-testing/
     * https://www.camiloterevinto.com/post/asp-net-core-integration-tests-with-nunit-and-moq
     * https://www.codingame.com/playgrounds/35462/creating-web-api-in-asp-net-core-2-0/part-3---integration-tests
     * https://www.azureblue.io/testing-asp-net-core-middleware-part-5/
     * https://dotnetthoughts.net/dotnet-minimal-api-integration-testing/
     * https://dotnetcorecentral.com/blog/asp-net-core-web-api-integration-testing-with-xunit/
     * https://stackoverflow.com/questions/29216534/how-do-i-set-multiple-headers-using-postasync-in-c
     * https://www.dofactory.com/code-examples/csharp/content-type-header
     * https://stackoverflow.com/questions/55950932/mock-ioptionsmonitor
     */
    #endregion
    public sealed class TestWebApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
	{
        private readonly MongoDbTestcontainer mongoTestContainer;
        private readonly Mock<ILogger<StudentRegistrationService>> mockLogger = new();
        private Mock<IOptionsMonitor<StudentDataStoreOptions>> mockOptions = new();
        public TestWebApplicationFactory()
		{
            var mongoConfig = new MongoDbTestcontainerConfiguration
            {
                Database = "TestStudents",
                Password = "pass",
                Username = "root"
            };
            
            mongoTestContainer = new TestcontainersBuilder<MongoDbTestcontainer>()
                .WithDatabase(mongoConfig)
                .Build();
		}

        public async Task InitializeAsync()
        {
            await mongoTestContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices( servicesCollection =>
            {
                servicesCollection.AddSingleton<IStudentRegistrationService>( _ =>
                {
                    var options = new StudentDataStoreOptions
                    {
                        ConnectionString = mongoTestContainer.ConnectionString,
                        DatabaseName = mongoTestContainer.Database,
                        StudentsCollectionName = "RegisteredStudents"
                    };
                    mockOptions.Setup(option => option.CurrentValue).Returns(options);
                    return new StudentRegistrationService(mockOptions.Object, mockLogger.Object);
                });
            });
        }

        public new async Task DisposeAsync()
        {
            await mongoTestContainer.StopAsync();
        }
    }
}

