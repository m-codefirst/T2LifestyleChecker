using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using T2LifestyleChecker.Enumerable.Models;
using T2LifestyleChecker.Services.Constants;
using T2LifestyleChecker.Services.Implementation.ModelS;
using T2LifestyleChecker.Services.Models;
using T2LifestyleChecker.Services.Services;

namespace T2LifestyleChecker.Services.Implementation.Services
{
    public class PatientValidationService : IPatientValidationService
    {
        private readonly string _subscriptionKey;
        private readonly string _clientApiTechTestPath;

        private readonly HttpClient _httpClient;
        private readonly ILogger<PatientValidationService> _logger;

        public PatientValidationService(IHttpClientFactory httpClientFactory, IConfiguration config, ILogger<PatientValidationService> logger)
        {
            _subscriptionKey = config["ApiSubscriptionKey"] ?? string.Empty;
            _clientApiTechTestPath = config["ClientApiTechTestPath"] ?? string.Empty;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(config["ClientApiTechTestBaseUrl"] ?? string.Empty);

            _logger = logger;
        }

        public async Task<IPatientValidationResult> ValidatePatientAsync(string nhsNumber, string surname, string dob)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,  $"{_clientApiTechTestPath}/{nhsNumber}");
            request.Headers.Add(ApiHeaderConstant.Ocp_Apim_Subscription_Key, _subscriptionKey);

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new PatientValidationResult { Status = ValidationStatus.NotFound };
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(Constant.ApiCallFailedWithStatusCodeMessage, response.StatusCode);
                return new PatientValidationResult { Status = ValidationStatus.NotFound };
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var patient = JsonSerializer.Deserialize<Patient>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (patient == null)
                return new PatientValidationResult { Status = ValidationStatus.NotFound };

            if (!patient.Name.Contains(surname, StringComparison.OrdinalIgnoreCase))
                return new PatientValidationResult { Status = ValidationStatus.DetailsMismatch };

            patient.Age = CalculateAge(patient.Born);
            if (patient.Age < 16)
                return new PatientValidationResult { Status = ValidationStatus.UnderAge, Patient = patient };

            return new PatientValidationResult { Status = ValidationStatus.Valid, Patient = patient };
        }

        private static int CalculateAge(string dob)
        {
            if (!DateTime.TryParse(dob, out var dateOfBirth)) return 0; 

            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth > today.AddYears(-age)) age--;

            return age;
        }

    }

}
