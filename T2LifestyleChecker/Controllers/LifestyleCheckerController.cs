using Microsoft.AspNetCore.Mvc;
using T2LifestyleChecker.Constants;
using T2LifestyleChecker.Enumerable.Models;
using T2LifestyleChecker.Model;
using T2LifestyleChecker.Services.Implementation.Models;
using T2LifestyleChecker.Services.Models;
using T2LifestyleChecker.Services.Services;

namespace T2LifestyleChecker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LifestyleCheckerController : ControllerBase
    {
        // Services used by the controller to do the actual work
        private readonly IPatientValidationService _patientValidationService;
        private readonly IQuestionAndScoringService _questionAndScoringService;
        private readonly ILogger<LifestyleCheckerController> _logger;

        // Constructor that receives the services via dependency injection (ASP.NET Core does this automatically)
        public LifestyleCheckerController(
            IPatientValidationService patientValidationService,
            IQuestionAndScoringService questionAndScoringService,
            ILogger<LifestyleCheckerController> logger)
        {
            _patientValidationService = patientValidationService;
            _questionAndScoringService = questionAndScoringService;
            _logger = logger;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidatePatient([FromBody] ValidatePatientRequest request)
        {
            try
            {
                var validationResult = await _patientValidationService.ValidatePatientAsync(request.NhsNumber, request.Surname, request.DateOfBirth);

                // The api will respond with a 404 if the patient cannot be found
                // Patient cannot be found - user is shown "Your details could not be found" message
                // Patient found, but details do not match - user is shown "Your details could not be found" message
                // Patient found, details match but they are under 16 years old - user is shown a message "You are not eligble for this service"
                return validationResult.Status switch
                {
                    ValidationStatus.NotFound or ValidationStatus.DetailsMismatch => BadRequest(new { message = ControllerConstant.NotFoundMessage }),
                    ValidationStatus.UnderAge => Ok(new { message = ControllerConstant.UnderAgeMessage }),
                    ValidationStatus.Valid => Ok(new { message = ControllerConstant.ValidMessage, age = validationResult?.Patient?.Age }),
                    _ => StatusCode(500)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }

        [HttpPost("score")]
        public IActionResult CalculateScore([FromBody] CalculateScoreRequest request)
        {
            if (request?.Answers?.Any() != true) { return BadRequest(ControllerConstant.EmptyRequest); }

            try
            {
                var answers = request.Answers.Select(x => new Answer { Id = x.Id, Value = x.Answer } as IAnswer).ToList();

                var score = _questionAndScoringService.CalculateScore(answers, request.Age);

                var message = _questionAndScoringService.GetOutcomeMessage(score);

                return Ok(new { score, message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }

        [HttpGet("questions")]
        public IActionResult GetQuestions()
        {
            var questions = new List<QuestionsResponse>();

            try
            {
                var questionList = _questionAndScoringService.GetQuestions();

                if (questionList?.Any() == true)
                {
                    questions = questionList.Select(x => new QuestionsResponse { Id = x.Id, Text = x.Text }).ToList();
                }

                return Ok(new { questions });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);

        }
    }
}