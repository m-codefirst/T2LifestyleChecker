using T2LifestyleChecker.Enumerable.Models;

namespace T2LifestyleChecker.Services.Models
{
    public interface IPatientValidationResult
    {
        ValidationStatus Status { get; }
        IPatient? Patient { get; }
    }
}