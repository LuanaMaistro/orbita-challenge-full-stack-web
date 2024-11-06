using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OrbitaChallengeBackEnd.Data;
using OrbitaChallengeBackEnd.Interfaces;
using OrbitaChallengeBackEnd.Repositories;
using OrbitaChallengeBackEnd.Validator;

var builder = WebApplication.CreateBuilder(args);

//MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 0, 1))));

builder.Services.AddScoped<IStudentRepository, StudentRepository>(); 

// Configurando CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<StudentValidator>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAllOrigins");


app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return; 
    }
    await next(); 
});


app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles();

app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
