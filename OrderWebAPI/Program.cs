using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Data;
using OrderWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();


//Connection SQLServer
var connectionStringSQL = builder.Configuration.GetConnectionString("DefaultConnectionDB");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStringSQL));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
