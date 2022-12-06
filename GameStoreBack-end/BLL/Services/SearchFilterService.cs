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
        private readonly Mapper _mapper;
        public SearchFilterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }
            );
            _mapper = new Mapper(config);
        }
        public SearchFilterService()
        {
            var options = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GameStoreDb;Trusted_Connection=True;")
                .Options;
            _unitOfWork = new UnitOfWork(options);
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }
            );
            _mapper = new Mapper(config);
        }

        public async Task<IEnumerable<GameModel>> SearchGamesByName(string gameName)
        {
            return _mapper.Map<IEnumerable<GameModel>>(
                await _unitOfWork.GameRepository.GetAllWithDetailsAsync()
                ).Where(g=>g.Name.Contains(gameName));
        }

        public async Task<IEnumerable<GameModel>> FilterGameByGenres(IEnumerable<int> genresIds)
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
