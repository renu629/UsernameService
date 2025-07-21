using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UsernameService.Data;
using UsernameService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=usernames.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/usernames/validate", (string username) =>
{
    if (string.IsNullOrWhiteSpace(username)
        || username.Length < 6
        || username.Length > 30
        || !username.All(char.IsLetterOrDigit))
    {
        return Results.BadRequest("Username must be alphanumeric and between 6–30 characters.");
    }

    return Results.Ok("Username is valid.");
});

app.MapPost("/api/usernames", async (UsernameRecord input, AppDbContext db) =>
{
    var validationContext = new ValidationContext(input);
    var results = new List<ValidationResult>();
    if (!Validator.TryValidateObject(input, validationContext, results, true))
    {
        return Results.BadRequest(results);
    }

    if (await db.UsernameRecords.AnyAsync(u => u.Username == input.Username))
    {
        return Results.Conflict("Username already exists.");
    }

    var existing = await db.UsernameRecords.FindAsync(input.AccountId);
    if (existing != null)
    {
        db.UsernameRecords.Remove(existing);
    }

    db.UsernameRecords.Add(input);
    await db.SaveChangesAsync();

    return Results.Ok("Username registered successfully.");
});

app.Run();
