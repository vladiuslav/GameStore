using AutoMapper;
using BLL.Models;
using DLL.Entities;

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
                .ReverseMap();

            CreateMap<Genre, GenreModel>()
                .ForMember(
                    gm => gm.GamesIds,
                    g => g.MapFrom(g => g.Games.Select(g => g.Id)))
                .ReverseMap();
        }

    }
}
