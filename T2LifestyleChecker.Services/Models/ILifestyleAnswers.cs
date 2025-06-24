namespace T2LifestyleChecker.Services.Models
{
    public interface ILifestyleAnswers
    {
        //bool DrinksMoreThan2Days { get; }
        //bool Smokes { get; }
        //bool ExercisesMoreThan1Hour { get; }

       List<IAnswer> Answers { set;  get; }
    }

    public interface IAnswer
    {
        int Id { get; set; }
        bool Value { get; set; }
    }
}