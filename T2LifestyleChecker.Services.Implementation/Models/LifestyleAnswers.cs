using T2LifestyleChecker.Services.Models;

namespace T2LifestyleChecker.Services.Implementation.Models
{
    public class LifestyleAnswers : ILifestyleAnswers
    {
        //public bool DrinksMoreThan2Days { get; init; }
        //public bool Smokes { get; init; }
        //public bool ExercisesMoreThan1Hour { get; init; }

        public List<IAnswer> Answers { set;  get; } = new();
    }

    public class Answer : IAnswer
    {
        public int Id { get; set; }
        public bool Value { get; set; }
    }
}