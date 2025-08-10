using System.Text;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

//Application
var builder = WebApplication.CreateBuilder(args);

#region Token Bearer Service

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

// Services
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddTransient<TokenService>();

// Dependency Inject => Db
builder.Services.AddDbContext<BlogDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

// Build
var app = builder.Build();

// Services 
app.UseAuthentication();
app.UseAuthorization();

// Controller Service 

app.MapControllers();


// Run App
app.Run();