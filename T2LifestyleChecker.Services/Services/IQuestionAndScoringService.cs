using T2LifestyleChecker.Services.Models;

namespace T2LifestyleChecker.Services.Services
{
    public interface IQuestionAndScoringService
    {
        int CalculateScore(IList<IAnswer> answers, int age);
        string GetOutcomeMessage(int score);
        IList<IQuestionScoringRule> GetQuestions();
    }
}
