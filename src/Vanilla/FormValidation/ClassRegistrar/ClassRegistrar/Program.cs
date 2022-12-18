#region
/*
 * https://code-maze.com/aspnet-configuration-options-validation/
 */
#endregion
using ClassRegistrar.Models.Options;
using ClassRegistrar.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<StudentDataStoreOptions>()
    .Bind(builder.Configuration.GetSection("StudentsDatabase"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
builder.Services.AddSingleton<IStudentRegistrationService, StudentRegistrationService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

