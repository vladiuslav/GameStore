﻿using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DLL.Data;
using DLL.Entities;
using DLL.Interafeces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(GameModel model)
        {
            var game = _mapper.Map<Game>(model);
            ICollection<Genre> genres = new List<Genre>();
            IEnumerable<Genre> genresDb = await _unitOfWork.GenreRepository.GetAllWithDetailsAsync();
            foreach (var item in model.GenresIds)
            {
                var genre = genresDb.FirstOrDefault(g => g.Id == item);
                if (genre != null)
                {
                    genres.Add(genre);
                }
            }
            game.Genres = genres;

            await _unitOfWork.GameRepository.AddAsync(game);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GameModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<GameModel>>(await _unitOfWork.GameRepository.GetAllWithDetailsAsync());
        }

        public async Task<GameModel> GetByIdAsync(int id)
        {
            return _mapper.Map<GameModel>(await _unitOfWork.GameRepository.GetByIdWithDetailsAsync(id));

        }
        public async Task<GameModel> GetByGameNameAsync(string name)
        {
            return _mapper.Map<GameModel>(
                    (await _unitOfWork.GameRepository.GetAllWithDetailsAsync())
                    .FirstOrDefault(g => g.Name == name)
                );
        }
        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.GameRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }
        public async Task UpdateAsync(GameModel model)
        {
            var game = _mapper.Map<Game>(model);
            var allGenres = await _unitOfWork.GenreRepository.GetAllWithDetailsAsync();
            var ganresForGame = new List<Genre>();
            foreach (var genre in allGenres)
            {
                if (model.GenresIds.Contains(genre.Id))
                {
                    ganresForGame.Add(genre);
                }
            }
            game.Genres = ganresForGame;
            await _unitOfWork.GameRepository.UpdateAsync(game);
            await _unitOfWork.SaveAsync();
        }
    }
}
