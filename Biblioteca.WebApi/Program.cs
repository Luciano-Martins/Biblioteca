using Microsoft.EntityFrameworkCore;
using Repositorio.Data;
using Repositorio.IRepositorios;
using Repositorio.Repositorios;
using Service.IService;
using Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
            // Esta linha configura o System.Text.Json para ignorar ciclos.
            // Quando ele encontra um objeto que já está sendo serializado (o Livro original),
            // ele simplesmente para de segui-lo.
            options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
// Adiciona o AutoMapper e escaneia a assembly atual para encontrar o MappingProfile
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// ou, que pode ser mais profundo e consequentemente mais lenta.
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContexto>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#region Serviços
builder.Services.AddScoped<IAssuntoRepositorio, AssuntoRepositorio>();
builder.Services.AddScoped<IEditoraRepositorio, EditoraRepositorio>();
builder.Services.AddScoped<IAutorRepositorio, AutorRepositorio>();
builder.Services.AddScoped<ILivroRepositorio, LivroRepositorio>();

builder.Services.AddScoped<IAssuntoService, AssuntoService>();
builder.Services.AddScoped<IEditoraService, EditoraService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILivroService, LivroService>();
#endregion

// resolvendo Cors
builder.Services.AddCors();
var app = builder.Build();

// 2. Aplica a política CORS ANTES do UseAuthorization

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
// *************************************************
app.UseCors(cors => cors
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            );
app.MapControllers();
app.Run();
