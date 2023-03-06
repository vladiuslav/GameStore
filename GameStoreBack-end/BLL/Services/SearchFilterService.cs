using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DLL.Data;
using DLL.Entities;
using DLL.Interafeces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SearchFilterService : ISearchFilterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchFilterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameModel>> SearchGamesByNameAsync(string gameName)
        {
            return _mapper.Map<IEnumerable<GameModel>>(
                await _unitOfWork.GameRepository.GetAllWithDetailsAsync()
                ).Where(g=>g.Name.Contains(gameName));
        }

        public async Task<IEnumerable<GameModel>> FilterGameByGenresAsync(IEnumerable<int> genresIds)
        {
            var games = await _unitOfWork.GameRepository.GetAllWithDetailsAsync();
            var genres = await _unitOfWork.GenreRepository.GetAllWithDetailsAsync();
            var Rgames = new List<Game>();
            foreach (var game in games)
            {
                bool feet = true;
                foreach(var genreId in genresIds)
                {
                    var genre = genres.FirstOrDefault(g => g.Id == genreId);
                    if (!genre.Games.Select(g=>g.Id).Contains(game.Id))
                    {
                        feet = false;
                        break;
                    }
                }
                if (feet)
                {
                    Rgames.Add(game);
                }
            }

            return _mapper.Map<IEnumerable<GameModel>>(Rgames);
        }
    }
}
