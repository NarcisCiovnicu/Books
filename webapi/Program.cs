using BusinessLogic.Services;
using DataAccess;
using DataAccess.Repositories;
using webapi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


AddServices(builder.Services);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();





static void AddServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddSingleton<BooksDbContext>();
    services.AddScoped<IBooksRepository, BooksRepository>();
    services.AddScoped<IAuthorsRepository, AuthorsRepository>();

    services.AddScoped<IBooksService, BooksService>();
    services.AddScoped<IAuthorsService, AuthorsService>();
}
