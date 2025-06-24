using Microsoft.Extensions.Configuration;

namespace T2LifestyleChecker.Services.Implementation.Test
{
    [TestFixture]
    public class ResourceConfigurationTests
    {
        private IConfiguration _configuration;
        private string _messagesPath;
        private string _scoringRulesPath;

        [SetUp]
        public void Setup()
        {
            var basePath = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.Parent.FullName;
            _messagesPath = Path.Combine(basePath, "T2LifestyleChecker\\Resources", "Messages.json");
            _scoringRulesPath = Path.Combine(basePath, "T2LifestyleChecker\\Resources", "ScoringRules.json");

            _configuration = new ConfigurationBuilder()
                .AddJsonFile(_messagesPath, optional: false, reloadOnChange: true)
                .AddJsonFile(_scoringRulesPath, optional: false, reloadOnChange: true)
                .Build();
        }


        [Test]
        public void MessagesJson_ShouldExistAndContainKeys()
        {
            var messagesSection = _configuration.GetSection("Messages");
            Assert.That(messagesSection, Is.Not.Null);
            Assert.That(messagesSection.Exists(), Is.True);

            var messageDict = messagesSection.Get<Dictionary<string, string>>();
            Assert.That(messageDict, Is.Not.Null);
            Assert.That(messageDict.ContainsKey("ScoreLow"), Is.True);
            Assert.That(messageDict.ContainsKey("ScoreHigh"), Is.True);
        }

        [Test]
        public void ScoringRulesJson_ShouldExistAndContainQuestions()
        {
            var ScoringConfigurationSection = _configuration.GetSection("ScoringConfiguration:Questions");
            Assert.That(ScoringConfigurationSection.Exists(), Is.True);

            var questionDict = ScoringConfigurationSection.Get<Dictionary<string, object>>();
            Assert.That(questionDict, Is.Not.Null);
            Assert.That(questionDict.Keys.Count > 0, Is.True);
        }

        [Test]
        public void ResourceFiles_ShouldExistOnDisk()
        {
            Assert.That(File.Exists(_messagesPath), Is.True, "Messages.json is missing from Resources folder.");
            Assert.That(File.Exists(_scoringRulesPath), Is.True, "ScoringRules.json is missing from Resources folder.");
        }
    }
}
