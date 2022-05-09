using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Entities.IdentityEntities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors();

builder.Services.AddIdentity<User, IdentityRole<Guid>>().AddDefaultTokenProviders()
                .AddEntityFrameworkStores<StudentDataContext>();

builder.Services.AddDbContext<StudentDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseCors(option => option.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
