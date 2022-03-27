using kanban_api.BusinessLayer;
using kanban_api.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    #region Adiciona Custom Exception para capturar erros da Business Layer

    config.Filters.Add(typeof(CustomExceptionFilter));

    #endregion
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Injeção de Dependência da Business Layer

builder.Services.AddTransient<CardsBL>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
