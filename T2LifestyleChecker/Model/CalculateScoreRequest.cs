namespace T2LifestyleChecker.Model
{
    public class CalculateScoreRequest
    {
        public List<AnswerResponse> Answers { get; set; } = new();
        public int Age { get; set; }
    }
    public class AnswerResponse
    {
        public int Id { get; set; }
        public bool Answer { get; set; }
    }
}
