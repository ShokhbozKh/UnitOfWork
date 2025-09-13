using UnitOfWork.Data;
using UnitOfWork.Repositories.Implementations;
using UnitOfWork.Repositories.Interfaces;
using UnitOfWork.Services.Users;
using UnitOfWork.Unit_Of_Work;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork.Unit_Of_Work.UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
