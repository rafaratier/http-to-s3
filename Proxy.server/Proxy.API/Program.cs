using Microsoft.AspNetCore.Authentication.JwtBearer;
using Proxy.API.Common.PasswordManager;
using Proxy.API.Common.TokenManager;
using Proxy.API.Persistence;
using Proxy.API.Persistence.Connection;
using Proxy.API.Services;
using Proxy.API.Services.Authentication;
using Proxy.API.Services.Registration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => 
    {   
        options.SuppressModelStateInvalidFilter = true;     
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddScoped<ITokenProvider, JwtProvider>();
builder.Services.AddScoped<IMySqlConnectionFactory, MySqlConnectionFactory>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IPasswordManager, PasswordManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
