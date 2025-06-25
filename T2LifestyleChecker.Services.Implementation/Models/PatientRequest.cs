using T2LifestyleChecker.Services.Models;

namespace T2LifestyleChecker.Services.Implementation.ModelS
{
    public class Patient : IPatient
    {
        public string NhsNumber { get; init; }
        public string Name { get; init; }
        public string Born { get; init; }
        public int Age { get; set; }
    }
}
