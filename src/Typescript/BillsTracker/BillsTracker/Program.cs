using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BillsTracker.Data;
using BillsTracker.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapPost("/bills", async (BillDto bill, ApplicationDbContext db) =>
{
    var newbill = new BillsTracker.Models.Bill
    {
        Id = Guid.NewGuid(),
        Name = bill.Name!,
        Paid = bill.Paid,
        Amount = bill.Amount
    };
    db.Bills.Add(newbill);
    await db.SaveChangesAsync();

    return Results.Created($"/bills/{newbill.Id}", bill);
});
app.MapPatch("/bills/paybill", async (UpdateBillDto request, ApplicationDbContext db) =>
{
    Guid id = new(request.Id!);
    var bill = await db.Bills.FindAsync(id);
    if (bill is null)
    {
        return Results.NotFound();
    }
    bill.Paid = request.Paid;
    db.Bills.Update(bill);
    await db.SaveChangesAsync();
    return Results.Ok();

});

app.MapRazorPages();

app.Run();

