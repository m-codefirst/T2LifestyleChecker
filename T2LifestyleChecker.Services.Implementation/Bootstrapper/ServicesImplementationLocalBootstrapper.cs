using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using T2LifestyleChecker.Services.Implementation.Models;
using T2LifestyleChecker.Services.Implementation.Services;
using T2LifestyleChecker.Services.Models;
using T2LifestyleChecker.Services.Services;

namespace T2LifestyleChecker.Services.Implementation.Bootstrapper
{
    public class ServicesImplementationLocalBootstrapper
    {
        public ServicesImplementationLocalBootstrapper(IServiceCollection services, IConfigurationManager configuration)
        {
            // Services
            services.AddScoped<IPatientValidationService, PatientValidationService>();
            services.AddScoped<IQuestionAndScoringService, QuestionAndScoringService>();

            services.Configure<ScoringConfiguration>(configuration.GetSection("ScoringConfiguration"));

            services.AddSingleton<IScoringConfiguration>(sp => sp.GetRequiredService<IOptions<ScoringConfiguration>>().Value);
            services.AddSingleton<IDictionary<string, string>>(sp => configuration.GetSection("Messages").Get<Dictionary<string, string>>()!);
        }
    }
}
