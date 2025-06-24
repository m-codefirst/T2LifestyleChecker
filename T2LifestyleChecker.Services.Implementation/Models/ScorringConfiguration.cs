using T2LifestyleChecker.Services.Models;

namespace T2LifestyleChecker.Services.Implementation.Models
{
    public class AgeGroup : IAgeGroup
    {
        public string Label { get; set; } = default!;
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }

    public class QuestionScoringRule : IQuestionScoringRule
    {
        public int Id { get; set; }
        public string Text { get; set; } = default!;
        public bool AwardForAnswer { get; set; }
        public Dictionary<string, int> Scores { get; set; } = new();
    }

    public class ScoringConfiguration : IScoringConfiguration
    {
        public List<AgeGroup> AgeGroups { get; set; } = new();
        public Dictionary<string, QuestionScoringRule> Questions { get; set; } = new();

        // Explicit interface implementation
        IList<IAgeGroup> IScoringConfiguration.AgeGroups => AgeGroups.Cast<IAgeGroup>().ToList();
        IDictionary<string, IQuestionScoringRule> IScoringConfiguration.Questions =>
            Questions.ToDictionary(kvp => kvp.Key, kvp => (IQuestionScoringRule)kvp.Value);
    }
}
