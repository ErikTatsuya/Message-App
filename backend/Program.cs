using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Solar;

var builder = WebApplication.CreateBuilder(args);

// Register controllers before build
builder.Services.AddControllers();

// Register AppDbContext with SQLite (file-based) database
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite("Data Source=app.db"));
// Register UserService
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	db.Database.EnsureCreated();
}

// mapear os controllers
app.MapControllers();

app.Run();
