using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using recetFr.Models;

namespace recetFr.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://comida23.somee.com/api/")
            };
        }

        public async Task<(bool IsSuccess, string Message)> RegisterUser(User user)
        {
            try
            {
                var json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("Users/register", content);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Registro exitoso");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al registrar: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Excepción: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message)> Login(LoginRequest loginRequest)
        {
            try
            {
                var json = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("Users/login", content);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Inicio de sesión exitoso");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al iniciar sesión: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Excepción: {ex.Message}");
            }
        }

        //--------------------------------Crud de Mels ---------------------------------------------------------//
        public async Task<List<Meal>> GetAllMealsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Meal>>($"{BaseUrl}");
        }

        public async Task<Meal> GetMealByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Meal>($"{BaseUrl}/{id}");
        }

        public async Task<bool> CreateMealAsync(Meal meal)
        {
            var content = new StringContent(JsonConvert.SerializeObject(meal), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMealAsync(int id, Meal meal)
        {
            var content = new StringContent(JsonConvert.SerializeObject(meal), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMealAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
