
//https://www.youtube.com/watch?v=6H0gxh0gwyI&ab_channel=c%23%D1%83%D1%87%D0%B8%D0%BC%D1%81%D1%8F%D0%B2%D0%BC%D0%B5%D1%81%D1%82%D0%B5

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString(
    "Server=(localdb)\\mssqllocaldb;Database=equipmentDb;Trusted_Connection=True;MultipleActiveResultSets=true");

builder.Services.AddDbContext<>();



// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
