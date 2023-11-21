using Enc_Chat_Identity_API.Database;
using EncChatCommonLib.Models;
using EncChatCommonLib.Services;
using EncChatCommonLib.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EncChatDB>(opt => opt.UseSqlite("Data Source=accounts.db;Mode=ReadWriteCreate;Cache=Shared"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IHashingService, HashingService>();
var app = builder.Build();

app.MapGet("/users/{id}", async (long id, EncChatDB db) 
    => await db.Users.FindAsync() is User user 
    ? Results.Ok(new { user.Id, user.Name, user.Email })
    : Results.NotFound());

app.MapGet("/users/{pageNo}/{pageSize}", async (int pageNo, int pageSize, EncChatDB db) =>
{
    int pagesToSkip = (pageNo - 1) * pageSize;
    var users = await db.Users.Select(u => new { u.Name, u.Email }).OrderBy(o => o.Name).Skip(pagesToSkip).Take(pageSize).ToListAsync();
    return Results.Ok(users);
});

app.MapPost("/login", async (LoginViewModel loginViewModel, EncChatDB db) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Email == loginViewModel.Email);
    if (user == null) return Results.BadRequest("Invalid email or password");
    if (user.Email != loginViewModel.Email) return Results.BadRequest("Invalid email or password");
    if (user.PasswordHash != loginViewModel.PasswordHash) return Results.BadRequest("Invalid email or password");

    return Results.Ok();
});

app.MapPost("/signup", async (User user, EncChatDB db) =>
{
    var dbUser = await db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

    if(dbUser != null) return Results.BadRequest("Email is Already Registered");

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", user);
});

app.Run();
