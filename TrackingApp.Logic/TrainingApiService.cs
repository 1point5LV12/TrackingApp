using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;
using System.Net.Http.Json;

namespace TrackingApp.Logic
{
    public class TrainingApiService
    {
        private readonly HttpClient _httpClient;

        public TrainingApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DbGeography> GetCurrentPositionAsync(Guid traineeId)
        {
            var response = await _httpClient.GetAsync($"api/trainings/GetCurrentPosition/{traineeId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DbGeography>();
            }

            return null;
        }

        public async Task<List<DbGeography>> GetTrainingDataAsync(Guid traineeId)
        {
            var response = await _httpClient.GetAsync($"api/trainings/GetTrainingData/{traineeId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DbGeography>>();
            }

            return null;
        }
    }

}
