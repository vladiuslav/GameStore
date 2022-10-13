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
				.ForMember(gm=>gm.Price,gmv=>gmv.MapFrom(g=>decimal.Parse(g.Price)));
			CreateMap<GameModel,GameViewModel>()
				.ForMember(gmv => gmv.Price, gm => gm.MapFrom(g => g.Price.ToString()));
			CreateMap<GanreViewModel, GanreModel>()
				.ReverseMap();
		}
	}
}
