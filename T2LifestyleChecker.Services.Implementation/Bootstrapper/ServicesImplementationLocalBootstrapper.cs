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

            //Part Three(Optional / Advanced)
            //How could the code be implemented in such a way that the scoring mechanism could be altered without requiring the code to be recompiled and re-deployed? This could be a change to age groups or scores for individual questions.
            services.AddScoped<IScoringConfiguration>(sp => sp.GetRequiredService<IOptionsSnapshot<ScoringConfiguration>>().Value);

            services.AddScoped<IDictionary<string, string>>(sp => configuration.GetSection("Messages").Get<Dictionary<string, string>>()!);
        }
    }
}
