using backendPFPU.Respositories;
using backendPFPU.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backendPFPU.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Agregar servicios al contenedor
builder.Services.AddControllers();
// Aprende más sobre cómo configurar OpenAPI en https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la cadena de conexión para la base de datos
var CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder.Services.AddSingleton<string>(CadenaDeConexion);

// Inyección de dependencias para el repositorio de usuarios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAnioRepository, AnioRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<ITipoPagoRepository, TipoPagoRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<INotaRepository, NotaRepository>();
builder.Services.AddScoped<JwtService>(); // Registrar JwtService
builder.Services.AddSingleton<PasswordService>(); // Registrar PasswordService

// Configuración de JWT (Agregado)
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Desactivado solo para desarrollo, habilítalo para producción
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true // Verificar que el token no esté expirado
    };
});



builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Puerto HTTP
    options.ListenAnyIP(7213, listenOptions => listenOptions.UseHttps()); // Puerto HTTPS
});


// Crear la aplicación
var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS - Debe ir antes de UseAuthentication y UseAuthorization
app.UseCors("AllowAll");

app.UseHttpsRedirection();



// Habilitar autenticación y autorización (Agregado)
app.UseAuthentication();  // Agregar autenticación
app.UseAuthorization();   // Agregar autorización

app.MapControllers();

app.Run();