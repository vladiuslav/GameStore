using AutoMapper;
using BLL.Models;
using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
		public AutoMapperProfile()
		{
			CreateMap<Game, GameModel>()
				.ForMember(gm => gm.GanresIds, g => g.MapFrom(g => g.Ganres.Select(g => g.Id))).ReverseMap();
			CreateMap<Ganre, GanreModel>()
				.ForMember(gm => gm.GamesIds, g => g.MapFrom(g => g.Games.Select(g => g.Id))).ReverseMap();
		}
		
	}
}
