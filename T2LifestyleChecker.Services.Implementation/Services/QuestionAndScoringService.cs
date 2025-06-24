using T2LifestyleChecker.Services.Constants;
using T2LifestyleChecker.Services.Models;
using T2LifestyleChecker.Services.Services;

namespace T2LifestyleChecker.Services.Implementation.Services
{
    public class QuestionAndScoringService : IQuestionAndScoringService
    {
        private readonly IDictionary<string, string> _messages;
        private readonly IScoringConfiguration _config;
        public QuestionAndScoringService(IScoringConfiguration configuration, IDictionary<string, string> messages)
        {
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public int CalculateScore(IList<IAnswer> answers, int age)
        {

            var ageGroup = _config.AgeGroups.FirstOrDefault(g => age >= g.MinAge && age <= g.MaxAge)?.Label;

            if (string.IsNullOrEmpty(ageGroup)) return 0;

            return _config.Questions.Values
                .Where(q => answers.Any(a => a.Id == q.Id && a.Value == q.AwardForAnswer))
                .Sum(q => q.Scores.TryGetValue(ageGroup, out var val) ? val : 0);
        }

        public string GetOutcomeMessage(int score)
        {
            return score <= 3 ? _messages[Constant.ScoreLow] : _messages[Constant.ScoreHigh];
        }

        public IList<IQuestionScoringRule> GetQuestions()
        {
            return _config.Questions.Values.ToList();
        }
    }
}