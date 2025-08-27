using Business;
using Data;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Utilities;
using Data.Implements;
using Business.Implements;
using Data.Interfaces;
using Business.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuración de JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseData<>), typeof(DataBase<>));
builder.Services.AddScoped(typeof(IBaseBusiness<,>), typeof(BaseBusiness<,>));

builder.Services.AddScoped<IFormData, FormData>();
builder.Services.AddScoped<IFormModuleData, FormModuleData>();
builder.Services.AddScoped<IModuleData, ModuleData>();
builder.Services.AddScoped<IPermissionData, PermissionData>();
builder.Services.AddScoped<IPersonData, PersonData>();
builder.Services.AddScoped<IRolData, RolData>();
builder.Services.AddScoped<IRolFormPermissionData, RolFormPermissionData>();
builder.Services.AddScoped<IRolUserData, RolUserData>();
builder.Services.AddScoped<IUserData, UserData>();

// Y también tus servicios de negocio:
builder.Services.AddScoped<IFormBusiness, FormBusiness>();
builder.Services.AddScoped<IFormModuleBusiness, FormModuleBusiness>();
builder.Services.AddScoped<IModuleBusiness, ModuleBusiness>();
builder.Services.AddScoped<IPermissionBusiness, PermissionBusiness>();
builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();
builder.Services.AddScoped<IRolBusiness, RolBusiness>();
builder.Services.AddScoped<IRolFormPermissionBusiness, RolFormPermissionBusiness>();
builder.Services.AddScoped<IRolUserBusiness, RolUserBusiness>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();

// Registrar clases de User
builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();

// Agregar CORS para permitir cualquier origen
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(politica =>
    {
        politica
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Agregar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

// Agregar configuración de JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EsteEsUnSecretoSuperSeguroDe32Caracteres!!")) // Cambia esto por algo más seguro
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors();


app.UseAuthorization();
app.MapControllers();
app.Run();
