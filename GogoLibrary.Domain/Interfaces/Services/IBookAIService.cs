using GogoLibrary.Domain.Dto.Book;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Services;

public interface IBookAIService
{
    Task<BaseResult<string>> FindByDescription(string description);
    Task<BaseResult<string>> TranslatePrompt(string promptText, string to);
}