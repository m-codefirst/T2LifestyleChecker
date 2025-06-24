using T2LifestyleChecker.Services.Implementation.Models;
using T2LifestyleChecker.Services.Implementation.Services;
using T2LifestyleChecker.Services.Models;

namespace T2LifestyleChecker.Services.Implementation.Test
{
    [TestFixture]
    public class QuestionAndScoringServiceTests
    {
        private Dictionary<string, string> _messages;
        private QuestionAndScoringService _service;

        [SetUp]
        public void Setup()
        {
            _messages = new Dictionary<string, string>
            {
                { "PatientNotFound", "Your details could not be found" },
                { "UnderAge", "You are not eligible for this service" },
                { "ScoreLow", "Thank you for answering our questions, we don't need to see you at this time. Keep up the good work!" },
                { "ScoreHigh", "We think there are some simple things you could do to improve your quality of life, please phone to book an appointment" }
            };

            var config = new ScoringConfiguration
            {
                AgeGroups = new List<AgeGroup>
                {
                    new() { Label = "16-21", MinAge = 16, MaxAge = 21 },
                    new() { Label = "22-40", MinAge = 22, MaxAge = 40 },
                    new() { Label = "41-65", MinAge = 41, MaxAge = 65 },
                    new() { Label = "64+", MinAge = 66, MaxAge = 120 }
                },
                Questions = new Dictionary<string, QuestionScoringRule>
                {
                    ["Q1"] = new QuestionScoringRule
                    {
                        Id = 1,
                        AwardForAnswer = true,
                        Scores = new Dictionary<string, int>
                        {
                            { "16-21", 1 },
                            { "22-40", 2 },
                            { "41-65", 3 },
                            { "64+", 3 }
                        }
                    },
                    ["Q2"] = new QuestionScoringRule
                    {
                        Id = 2,
                        AwardForAnswer = true,
                        Scores = new Dictionary<string, int>
                        {
                            { "16-21", 2 },
                            { "22-40", 2 },
                            { "41-65", 2 },
                            { "64+", 3 }
                        }
                    },
                    ["Q3"] = new QuestionScoringRule
                    {
                        Id = 3,
                        AwardForAnswer = false,
                        Scores = new Dictionary<string, int>
                        {
                            { "16-21", 1 },
                            { "22-40", 3 },
                            { "41-65", 2 },
                            { "64+", 1 }
                        }
                    }
                }
            };

            _service = new QuestionAndScoringService(config, _messages);
        }

        [Test]
        public void CalculateScore_ReturnsCorrectScore_ForAge25()
        {
            var answers = new List<IAnswer>
            {
                new Answer { Id = 1, Value = true },
                new Answer { Id = 2, Value = true },
                new Answer { Id = 3, Value = false } // triggers scoring
            };

            int result = _service.CalculateScore(answers, 25);
            Assert.That(result, Is.EqualTo(2 + 2 + 3));
        }

        [Test]
        public void GetOutcomeMessage_ReturnsLowRiskMessage_ForScore3()
        {
            var message = _service.GetOutcomeMessage(3);
            Assert.That(message, Is.EqualTo(_messages[Constants.Constant.ScoreLow]));
        }

        [Test]
        public void GetOutcomeMessage_ReturnsHighRiskMessage_ForScore4()
        {
            var message = _service.GetOutcomeMessage(4);
            Assert.That(message, Is.EqualTo(_messages[Constants.Constant.ScoreHigh]));
        }

        [Test]
        public void GetQuestions_ReturnsAllConfiguredQuestions()
        {
            var questions = _service.GetQuestions();
            Assert.That(questions.Count, Is.EqualTo(3));
            CollectionAssert.AreEquivalent(new[] { 1,2,3 }, questions.Select(q => q.Id));
        }
    }
}
