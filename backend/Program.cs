using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Solar;
using Solar.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		policy.WithOrigins("http://localhost:5173")
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

// Register controllers before build
builder.Services.AddControllers();

// Register AppDbContext with SQLite (file-based) database
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite("Data Source=app.db"));
// Register UserService and UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LoginService>();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	db.Database.EnsureCreated();
}

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
