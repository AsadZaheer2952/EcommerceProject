using Ecommerce.Controllers;
using Ecommerce.Data;
using Ecommerce.Middleware;
using Ecommerce.Model;
using Ecommerce.Repository;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EcommStoreContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver")));
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire")));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<HangfireController>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<ISubCategories, SubCategoriesRepository>();
builder.Services.AddScoped<IAccountRepo, AccountRepository>();
builder.Services.AddTransient<FirstMiddleware>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
        };
    });
//builder.Services.AddScoped<DataSeeder>(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseHangfireDashboard("/mydashboard");

app.UseHttpsRedirection();

app.UseMiddleware<FirstMiddleware>();
/*app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("first middle ware");
    await next();
});*/

app.Use(async (context, next) =>
  {
var token = context.Request.Cookies["accessToken"];
if (token != null)
    context.Request.Headers["Authorization"] = "Bearer " + token.ToString();
await next();
});
app.UseAuthentication();
app.UseAuthorization();
var WssOption = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(120) };
app.UseWebSockets(WssOption);

app.MapControllers();

app.Run();



