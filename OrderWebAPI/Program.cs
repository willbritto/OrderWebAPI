using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderWebAPI.Data;
using OrderWebAPI.DTOs.Mappings;
using OrderWebAPI.Execptions;
using OrderWebAPI.Models;
using OrderWebAPI.Services;
using OrderWebAPI.Services.Logs;
using Serilog;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//Autorizacao para acesso dos endpoints do controllador
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
      Title = "APIOrder",
      Version = "v1",
      Description = "Venda e Orçamentos para serviços",
      Contact = new OpenApiContact 
      {
          Name = "Ailson",
          Email = "alfatechvaa@gmail.com",
      }
    
    });

    //XML
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"Preencha o cabeçalho de autorização JWT usando o esquema Bearer.
                        Digite 'Bearer' [Espaço]. Exemplo: 'Bearer 12345abcdef'"
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

//Entities e Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPrintService, PrintService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingsProfile));

//SeriLog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.With(new ThreadIdEnricher())
    .WriteTo.File("log_OrderAPI.txt")
    .WriteTo.Console(outputTemplate : "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
    .CreateLogger();

//Debugging and Diagnostics
var file = File.CreateText("Deb_Diagnostic.txt");
Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));

builder.Host.UseSerilog();

//Connection SQLServer
var connectionStringSQL = builder.Configuration.GetConnectionString("DefaultConnectionDB");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStringSQL)
.EnableSensitiveDataLogging()
.LogTo(Console.WriteLine, LogLevel.Information));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//JWT Autentication 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", op =>
{
    op.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
    };
});

//CORS
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllSpecificOrign", 
        policy => 
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); //CORS aberto apenas para teste da aplicacão.
            //policy.WithOrigins("https://localhost:7175", "http://localhost:5192").AllowAnyHeader().AllowAnyMethod(); //Obrigatoriamente utilizar esse CORS em produção.
        });
});

//Rate Limiting
builder.Services.AddRateLimiter(op => 
{
    op.AddFixedWindowLimiter("fixedRL", RateLimiterOptions => 
    {
        RateLimiterOptions.PermitLimit = 5;
        RateLimiterOptions.Window = TimeSpan.FromSeconds(10);
        RateLimiterOptions.QueueLimit = 2;
        RateLimiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIOrder");
    });
}
app.UseRateLimiter();

app.MapControllers().RequireRateLimiting("fixedRL");

app.UseHttpsRedirection();

//MiddleException
app.UseMiddleware<ExceptionMiddleware>();


app.UseCors(); //CORS
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
