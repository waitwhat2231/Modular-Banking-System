using Common.SharedClasses.Services;
using FirebaseAdmin;
using FluentValidation;
using FluentValidation.AspNetCore;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Application.Services;


namespace Modules.Users.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddUsersApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        services.AddAutoMapper(applicationAssembly);
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<INotificationService, NotificationService>();

        var firebaseKeyPath = Path.Combine(Directory.GetCurrentDirectory(), configuration["Firebase:ServiceAccountFilePath"]);

        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                //Credential = CredentialFactory.FromFile(firebaseKeyPath, JsonCredentialParameters.ServiceAccountCredentialType)
                Credential = GoogleCredential.FromFile(firebaseKeyPath)
            });
        }
    }


}