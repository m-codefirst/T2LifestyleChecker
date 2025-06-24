using T2LifestyleChecker.Services.Models;

namespace T2LifestyleChecker.Services.Services
{
    public interface IPatientValidationService
    {
        Task<IPatientValidationResult> ValidatePatientAsync(string nhsNumber, string surname, string dob);
    }
}
