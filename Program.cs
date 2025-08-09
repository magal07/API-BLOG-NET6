using Blog.Data;


//Application
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddDbContext<BlogDataContext>();
//Build
var app = builder.Build();

// Controller Service
app.MapControllers();

// Run App
app.Run();
