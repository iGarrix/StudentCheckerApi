using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Entities.IdentityEntities;
using StudentAPI.Helper;
using StudentAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddCors();

builder.Services.AddIdentity<Person, IdentityRole<Guid>>(opt =>
{
    opt.Password.RequiredLength = 3;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<StudentDataContext>();

builder.Services.AddScoped<JwtHelper>();
builder.Services.AddTransient<AuthRepository>();
builder.Services.AddTransient<UniversityRepository>();
builder.Services.AddTransient<ScheduleRepository>();
builder.Services.AddTransient<LessonRepository>();

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

app.SeedData();

app.Run();
