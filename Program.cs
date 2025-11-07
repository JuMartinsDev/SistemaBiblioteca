using BibliotecaApp.Data;
using BibliotecaApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseInMemoryDatabase("BibliotecaDB"));

builder.Services.AddScoped<LivroService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<EmprestimoService>();
builder.Services.AddScoped<RelatorioService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
