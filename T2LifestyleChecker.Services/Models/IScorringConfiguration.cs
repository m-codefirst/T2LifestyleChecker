namespace T2LifestyleChecker.Services.Models
{
    public interface IAgeGroup
    {
         string Label { get; set; }
         int MinAge { get; set; }
         int MaxAge { get; set; }
    }

    public interface IQuestionScoringRule
    {
         int Id { get; set; }
         string Text { get; set; }
         bool AwardForAnswer { get; set; }
        Dictionary<string, int> Scores { get; set; } 
    }

    public interface IScoringConfiguration
    {
        IList<IAgeGroup> AgeGroups { get; }
        IDictionary<string, IQuestionScoringRule> Questions { get; }
    }
}
