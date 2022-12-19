using System.Net;
using System.Text;
using ClassRegistrar.Requests;
using FluentAssertions;
using Newtonsoft.Json;

namespace ClassRegistrar.IntegrationTests;
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
 * https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/
 * https://stackoverflow.com/questions/22093843/pass-complex-parameters-to-theory
 * https://hamidmosalla.com/2017/02/25/xunit-theory-working-with-inlinedata-memberdata-classdata/
 */
#endregion
public class RegistrationControllerTests: IClassFixture<TestWebApplicationFactory>
{
    private readonly TestWebApplicationFactory _factory;
    private readonly HttpClient client;
    public RegistrationControllerTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
        client = _factory.CreateDefaultClient();
    }
    [Theory]
    [Trait("Category", "Integration")]
    [MemberData(nameof(StudentRegistrationData))]
    public async Task RegisterShouldReturnStatusCode(HttpStatusCode statusCode
        , string firstName
        , string lastName
        , IEnumerable<string> courses)
    {
        //Arrange
        var dto = new RegistrationDto
        {
            FirstName = firstName,
            LastName = lastName,
            RegistrationDate = DateTime.Now,
            Courses = courses,
        };
        var content = new StringContent(JsonConvert.SerializeObject(dto),Encoding.UTF8, "application/json");
        //Act
        HttpResponseMessage response = await client.PostAsync("/Registration/Register", content);
        //Assert
        response.StatusCode.Should().Be(statusCode);

    }

    public static IEnumerable<object[]> StudentRegistrationData()
    {
        yield return new object[] { HttpStatusCode.BadRequest, "112343", string.Empty, new List<string> { "Math", "Science", "English", "More Science" } };
        yield return new object[] { HttpStatusCode.BadRequest, "Test", "Fake", new List<string> { "Math", "Science"} };
        yield return new object[] { HttpStatusCode.OK, "Test","Fake", new List<string> {"Math", "Science", "English", "More Science" }};
    }
}
