using System.Text;
using BlogApi;
using BlogApi.Data;
using BlogApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder
    .Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddDbContext<DataContext>();
builder.Services.AddTransient<TokenServices>();

var app = builder.Build();
LoadConfiguration(app);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Vai servir como uma conexão para enviar email.
void LoadConfiguration(WebApplication app)
{
    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");
    Configuration.ApiKeyName = app.Configuration.GetValue<string>("ApiKeyName");
    Configuration.ApiKey = app.Configuration.GetValue<string>("ApiKey");

    var smtp = new Configuration.SmtpConfiguration();
    app.Configuration.GetSection("Smtp").Bind(smtp);
    Configuration.Smtp = smtp;
}
