using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class OpenAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _apiUrl = "https://api.openai.com/v1/chat/completions"; // ✅ Correct endpoint

    public OpenAiService(string apiKey)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey;
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<string> GetOpenAiResponse(string prompt)
    {
        var requestBody = new
        {
            model = "gpt-4-turbo", // ✅ Use "gpt-4-turbo" or "gpt-3.5-turbo"
            messages = new[]
            {
                new { role = "system", content = "You are a helpful AI assistant." },
                new { role = "user", content = prompt } // ✅ User's input
            },
            max_tokens = 100,
            temperature = 0.7
        };

        var jsonRequest = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(_apiUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Log the error and throw an exception with details
                Console.WriteLine($"Error: {response.StatusCode}, Response: {responseString}");
                throw new Exception($"OpenAI API Error: {response.StatusCode} - {responseString}");
            }

            dynamic result = JsonConvert.DeserializeObject(responseString);

            // ✅ Fix: Access `choices[0].message.content` instead of `choices[0].text`
            if (result.choices == null || result.choices.Count == 0)
            {
                throw new Exception("OpenAI API response does not contain choices.");
            }

            return result.choices[0].message.content.ToString().Trim();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in OpenAiService: {ex.Message}");
            throw; // Rethrow the exception to propagate it up the stack
        }
    }
}
