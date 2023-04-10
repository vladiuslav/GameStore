using AutoMapper;
using BLL.Models;
using GameStore.WebAPI.Models;
using GameStrore.BusinessLogic.Models;
using WebApi.Models;

namespace WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
		{
			CreateMap<GameViewModel, GameModel>()
				.ForMember(
					gameModel=>gameModel.Price,
					gameViewModel=>gameViewModel.MapFrom(game=>decimal.Parse(game.Price))
				);

			CreateMap<GameModel,GameViewModel>()
				.ForMember(
                    gameViewModel => gameViewModel.Price,
                    gameModel => gameModel.MapFrom(game => game.Price.ToString())
				);

			CreateMap<GenreViewModel, GenreModel>()
				.ReverseMap();

			CreateMap<UserFullViewModel,UserModel>()
				.ReverseMap();

			CreateMap<UserModel, UserViewModel>()
				.ForMember(
				uvm => uvm.FullName,
				um => um.MapFrom(u => u.FirstName + ' ' + u.LastName)
				);
			CreateMap<CommentModel, CommentViewModel>()
				.ReverseMap();
            CreateMap<CommentModel, CommentCreateModel>()
				.ReverseMap();
        }
	}
}
