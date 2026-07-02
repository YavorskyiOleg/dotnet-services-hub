using Microsoft.EntityFrameworkCore;
using MyProjectsList.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Додавання CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReact", policy => {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// Додамо сервіси Swagger до builder.Build().
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. Налаштування контексту БД
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Усі сервіси мають бути додані строго ДО цього рядка!
var app = builder.Build(); // <--- Усі сервіси мають бути додані строго ДО цього рядка!


// Додано app.UseRouting(): Гарантує, що CORS та авторизація точно знатимуть контекст запиту до того, як він потрапить у контролери.
app.UseRouting(); 

app.UseCors("AllowReact"); // <-- AAA!!! Спочатку дозволяємо CORS для React
app.UseAuthorization();

// Увімкнемо Middleware для Swagger після builder.Build().
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Projects List API v1");
});

// Головна сторінка
app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html lang="uk">
<head>
    <meta charset="utf-8" />
    <title>My Projects List API</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 2rem; }
        code { background: #f3f3f3; padding: 0.2rem 0.4rem; }
    </style>
</head>
<body>
    <h1>My Projects List API</h1>
    <p>API працює.</p>
    <p>Swagger документація: <a href="/swagger">/swagger</a></p>
    <p>Основний endpoint: <code>/api/projects</code></p>
</body>
</html>
""", "text/html"));

app.MapControllers(); // <-- AAA!!! І тільки потім пускаємо запит до контролерів

app.Run();