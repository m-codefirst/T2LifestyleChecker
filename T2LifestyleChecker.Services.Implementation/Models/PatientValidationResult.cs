using T2LifestyleChecker.Enumerable.Models;
using T2LifestyleChecker.Services.Models;

namespace T2LifestyleChecker.Services.Implementation.ModelS
{
    public class PatientValidationResult : IPatientValidationResult
    {
        public ValidationStatus Status { get; init; }
        public IPatient? Patient { get; init; }
    }
}
