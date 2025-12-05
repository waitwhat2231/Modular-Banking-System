using Common.SharedClasses.Repositories;
using Modules.Accounts.Infrastructure.Extensions;
using Modules.Transactions.Application.Extensions;
using Modules.Transactions.Infrastructure.Extensions;
using Modules.Users.Application.Extensions;
using Modules.Users.Endpoints.Extensions;
using Modules.Users.Infrastructure.Extensions;
using Modules.Users.Infrastructure.Seeders;
using Template.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().
    AddApplicationPart(typeof(Modules.Transactions.Endpoints.Controllers.TransactionsController).Assembly);

builder.Services.AddControllers().
    AddApplicationPart(typeof(Modules.Users.Endpoints.Controllers.UsersController).Assembly);

builder.Services.AddControllers().
    AddApplicationPart(typeof(Modules.Accounts.Endpoints.Controller.AccountsController).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.AddUserModulePresentation();
builder.Services.AddUsersApplication();
builder.Services.AddUsersInfrastructure(builder.Configuration);

builder.Services.AddAccountsApplication();
builder.Services.AddAccountsInfrastructure(builder.Configuration);

builder.Services.AddTransactionsApplication();
builder.Services.AddTransactionsInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


var app = builder.Build();

var scope = app.Services.CreateScope(); //for seeders
// example: var govSeeder = scope.ServiceProvider.GetRequiredService<IGovernorateSeeder>();
var rolesSeeder = scope.ServiceProvider.GetRequiredService<IRolesSeeder>();

await rolesSeeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
