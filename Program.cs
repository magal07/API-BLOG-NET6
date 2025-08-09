using Blog.Data;
using Microsoft.EntityFrameworkCore;

//Application
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
        options.SuppressModelStateInvalidFilter = true);

// Dependency Inject => Db
builder.Services.AddDbContext<BlogDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Build
var app = builder.Build();

// Controller Service
app.MapControllers();

// Run App
app.Run();
