using AutoMapper;
using GogoLibrary.Domain.Dto.Comment;
using GogoLibrary.Domain.Entities;

namespace GogoLibrary.Application.Mapping;

public class CommentMapping : Profile
{
    public CommentMapping()
    {
        CreateMap<BookComment, AddCommentDto>().ReverseMap();
        CreateMap<BookComment, CommentDto>().ReverseMap();
    }
}