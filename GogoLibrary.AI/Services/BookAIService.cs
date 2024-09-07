using System.Net;
using System.Text;
using System.Text.Json;
using GogoLibrary.Domain.Dto.Book;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.Extensions.Configuration;

namespace GogoLibrary.AI.Services;

public class BookAIService : IBookAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public BookAIService(HttpClient httpClient, 
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiUrl = configuration["APIs:FindByDescriptionAI"];
    }

    public async Task<BaseResult<string>> FindByDescription(string description)
    {
        var requestMessage = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text =
                                $"{description}, что это за книга, дай название и автора(ничего лишнего, в другом случае, просто варианты или лучшее подходящее): BookTitle: тут название, BookAuthor: тут автор"
                        }
                    }
                }
            }
        };
        
        var result = await GetResult(requestMessage);

        if (result.IsSuccess)
        {
            return result;
        }

        throw new Exception("Internal server error");
    }

    public async Task<BaseResult<string>> TranslatePrompt(string promptText, string to)
    {
        var requestMessage = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text =
                                $"Вот текст: {promptText}, переведи это на {to} язык, без ничего лишнего, просто дай ответ."
                        }
                    }
                }
            }
        };

        var result = await GetResult(requestMessage);

        if (result.IsSuccess)
        {
            return result;
        }

        throw new Exception("Internal server error");
    }

    public async Task<BaseResult<string>> GetResult(Object requestMessage)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(requestMessage), Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(_apiUrl, jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseContent);
            var root = jsonDoc.RootElement;
            
            var candidates = root.GetProperty("candidates");
            var firstCandidate = candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();

            return new BaseResult<string>
            {
                Data = firstCandidate
            };
        }
        
        return new BaseResult<string>
        {
            ErrorMessage = "Something went wrong while finding a book",
            ErrorCode = (int)HttpStatusCode.InternalServerError
        };
        
    }
}