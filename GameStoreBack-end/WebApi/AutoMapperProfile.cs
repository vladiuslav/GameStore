using AutoMapper;
using BLL.Models;
using WebApi.Models;

namespace WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
		{
			CreateMap<GameViewModel, GameModel>()
				
				.ReverseMap();
			CreateMap<GanreViewModel, GanreModel>()
				.ReverseMap();
		}
	}
}
