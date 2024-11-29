using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using recetFr.Models;

namespace recetFr.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://comida23.somee.com/api/";

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        // -------------------------------- Registro de Usuario -------------------------------- //
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

        // -------------------------------- Inicio de Sesión -------------------------------- //
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

        // -------------------------------- CRUD de Meals -------------------------------- //
        public async Task<List<Meal>> GetAllMealsAsync()
        {
            try
            {
                var meals = await _httpClient.GetFromJsonAsync<List<Meal>>("MelsControllerApi");
                return meals ?? new List<Meal>();
            }
            catch
            {
                return new List<Meal>(); // Retornar lista vacía si ocurre un error.
            }
        }

        public async Task<Meal?> GetMealByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Meal>($"MelsControllerApi/{id}");
            }
            catch
            {
                return null; // Retornar null si ocurre un error.
            }
        }

        public async Task<(bool IsSuccess, string Message)> CreateMealAsync(Meal meal)
        {
            try
            {
                var json = JsonSerializer.Serialize(meal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("MelsControllerApi", content);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Comida creada exitosamente");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al crear la comida: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Excepción: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message)> UpdateMealAsync(int id, Meal meal)
        {
            try
            {
                var json = JsonSerializer.Serialize(meal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"MelsControllerApi/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Comida actualizada exitosamente");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al actualizar la comida: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Excepción: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message)> DeleteMealAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"MelsControllerApi/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Comida eliminada exitosamente");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"Error al eliminar la comida: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Excepción: {ex.Message}");
            }
        }

        public async Task<List<Comida>> SearchMealByName(string name)
        {
            var response = await _httpClient.GetFromJsonAsync<ComidaResponse>($"Meals/search?name={name}");
            return response?.Meals ?? new List<Comida>();
        }

        
    }
}