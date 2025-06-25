namespace T2LifestyleChecker.Services.Models
{
    public interface IPatient
    {
        string NhsNumber { get; }
        string Name { get; }
        string Born { get; }
        int Age { get; set; }
    }
}
