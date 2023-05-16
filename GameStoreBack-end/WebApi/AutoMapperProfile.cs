using AutoMapper;
using BLL.Models;
using GameStore.WebAPI.Models;
using GameStore.WebAPI.Models.CommentModels;
using GameStore.WebAPI.Models.GameModels;
using GameStore.WebAPI.Models.GenreModels;
using GameStore.WebAPI.Models.UserModels;
using GameStrore.BusinessLogic.Models;

namespace WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
		{
			CreateMap<GameModel,GameViewModel>()
				.ForMember(
                    gameViewModel => gameViewModel.Price,
                    gameModel => gameModel.MapFrom(game => game.Price.ToString())
				);

			CreateMap<GameCreateModel, GameModel>()
                .ForMember(
                    gameModel => gameModel.Price,
                    gameCreateModel => gameCreateModel.MapFrom(game => decimal.Parse(game.Price))
                );

            CreateMap<GameUpdateModel, GameModel>()
                .ForMember(
                    gameModel => gameModel.Price,
                    gameUpdateModel => gameUpdateModel.MapFrom(game => decimal.Parse(game.Price))
                );

            CreateMap<GenreModel, GenreViewModel>();

            CreateMap<GenreCreateModel, GenreModel>();

            CreateMap<GenreUpdateModel, GenreModel>();

            CreateMap<UserCreateModel, UserModel>();

            CreateMap<UserFullViewModel,UserModel>()
				.ReverseMap();

            CreateMap<UserUpdateModel, UserModel>()
                .ReverseMap();

            CreateMap<UserModel, UserViewModel>()
				.ForMember(
				uvm => uvm.FullName,
				um => um.MapFrom(u => u.FirstName + ' ' + u.LastName)
				);

            CreateMap<CommentModel, CommentCreateModel>()
				.ReverseMap();

			CreateMap<OrderModel,CreateOrderModel>()
				.ReverseMap();

        }
	}
}
