using BibliotecaApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseInMemoryDatabase("BibliotecaDB"));

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();
