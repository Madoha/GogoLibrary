using GogoLibrary.Domain.Dto.Comment;
using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Interfaces.Repositories;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Services;

public interface ICommentServices
{
    Task<BaseResult> AddCommentToBookAsync(AddCommentDto dto);
    Task<CollectionResult<CommentDto>> GetCommentsByBookIdAsync(long bookId);
}