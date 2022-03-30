using kanban_api.Authentication;
using kanban_api.BusinessLayer;
using kanban_api.Context;
using kanban_api.Interfaces;
using kanban_api.Repository;
using kanban_api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(config =>
{
    #region Adiciona Custom Exception e Log para capturar erros da Business Layer e gravar quando chama PUT e DELETE

    config.Filters.Add(typeof(CustomExceptionFilter));
    config.Filters.Add(typeof(LogFilter));
    #endregion
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

#region Injeção de dependência do token jwt

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
            .Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);

#endregion

#region Injeção de dependência do login

var loginConfigurations = new LoginConfigurations();
new ConfigureFromConfigurationOptions<LoginConfigurations>(
    builder.Configuration.GetSection("LoginConfigurations"))
            .Configure(loginConfigurations);

builder.Services.AddSingleton(loginConfigurations);

#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidAudience = tokenConfigurations.Audience,
        ValidIssuer = tokenConfigurations.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Informe o token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

#region Injeção de dependência da Business Layer

builder.Services.AddTransient<CardsBL>();
builder.Services.AddTransient<LoginBL>();

#endregion

#region Injeção de dependência do Repository

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

#endregion


#region Adição do contexto do Entity Framework e uso de banco de dados em memória

builder.Services.AddDbContext<KanbanContext>(options =>
options.UseInMemoryDatabase("Kanban"));

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
