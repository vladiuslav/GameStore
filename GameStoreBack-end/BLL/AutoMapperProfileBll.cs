using AutoMapper;
using BLL.Models;
using DLL.Entities;
using GameStore.DataLogic.Entities;
using GameStrore.BusinessLogic.Models;

namespace BLL
{
    public class AutoMapperProfileBll : Profile
    {
        public AutoMapperProfileBll()
        {
            CreateMap<Game, GameModel>()
                .ForMember(
                    gm => gm.GenresIds,
                    g => g.MapFrom(g => g.Genres.Select(g => g.Id)))
                .ForMember(
                    gm => gm.CommentsIds,
                    g => g.MapFrom(g => g.Comments.Select(c => c.Id)))
                .ReverseMap();

            CreateMap<Genre, GenreModel>()
                .ForMember(
                    gm => gm.GamesIds,
                    g => g.MapFrom(g => g.Games.Select(g => g.Id)))
                .ReverseMap();
            CreateMap<User, UserModel>()
                .ForMember(
                    um => um.CommentsIds,
                    u => u.MapFrom(u => u.Comments.Select(c => c.Id)))
                .ReverseMap();
            CreateMap<Comment, CommentModel>()
                .ReverseMap();
        }

    }
}
