using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrackingApp.Domain.Entities;

namespace TrackingApp.Logic
{
    public class TraineeApiService
    {
        private readonly HttpClient _httpClient;

        public TraineeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Trainee>> GetAllTraineesAsync()
        {
            var response = await _httpClient.GetAsync("api/trainees");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Trainee>>();
            }

            return null;
        }

        public async Task<Trainee> GetTraineeByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/trainees/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Trainee>();
            }

            return null;
        }
    }
}
