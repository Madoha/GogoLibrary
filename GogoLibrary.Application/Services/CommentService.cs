using System.Net;
using AutoMapper;
using GogoLibrary.Domain.Dto.Comment;
using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Interfaces.Repositories;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace GogoLibrary.Application.Services;

public class CommentService : ICommentServices
{
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<BookComment> _bookCommentRepository;
    private readonly IMapper _mapper;

    public CommentService(IBaseRepository<Book> bookRepository, 
        IBaseRepository<User> userRepository, 
        IMapper mapper, 
        IBaseRepository<BookComment> bookCommentRepository)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _bookCommentRepository = bookCommentRepository;
    }

    public async Task<BaseResult> AddCommentToBookAsync(AddCommentDto dto)
    {
        var book = await _bookRepository.GetAll().FirstOrDefaultAsync(b => b.Id == dto.BookId);
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Id == dto.UserId);
        if (book == null || user == null)
        {
            return new BaseResult()
            {
                ErrorMessage = "Book or user not found",
                ErrorCode = (int)HttpStatusCode.NotFound,
            };
        }

        var comment = _mapper.Map<BookComment>(dto);

        try
        {
            await _bookCommentRepository.CreateAsync(comment);
            await _bookRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new BaseResult()
            {
                ErrorMessage = ex.Message,
                ErrorCode = (int)HttpStatusCode.InternalServerError,
            };
        }

        return new BaseResult();
    }

    public async Task<CollectionResult<CommentDto>> GetCommentsByBookIdAsync(long bookId)
    {
        var book = await _bookRepository.GetAll().Include(b => b.Comments).FirstOrDefaultAsync(b => b.Id == bookId);
        if (book == null)
        {
            return new CollectionResult<CommentDto>()
            {
                ErrorMessage = "Book not found",
                ErrorCode = (int)HttpStatusCode.NotFound,
            };
        }

        if (book.Comments.Count == 0)
        {
            return new CollectionResult<CommentDto>()
            {
                ErrorMessage = "No comments found",
                ErrorCode = (int)HttpStatusCode.OK,
            };
        }
        
        var comments = _mapper.Map<List<CommentDto>>(book.Comments);
        return new CollectionResult<CommentDto>()
        {
            Data = comments,
            Count = book.Comments.Count,
        };

    }
}