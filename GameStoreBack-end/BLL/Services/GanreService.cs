﻿using BLL.Interfaces;
using BLL.Models;
using DLL.Data;
using DLL.Interafeces;
using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }
            );
            _mapper = new Mapper(config);
        }
        public GenreService()
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

        public async Task AddAsync(GenreModel model)
        {
            var genre = _mapper.Map<Genre>(model);
            ICollection<Game> games = new List<Game>();
            IEnumerable<Game> gamesDb = await _unitOfWork.GameRepository.GetAllWithDetailsAsync();
            foreach (var item in model.GamesIds)
            {
                var game = gamesDb.FirstOrDefault(g => g.Id == item);
                if (game != null)
                {
                    games.Add(game);
                }
            }
            genre.Games = games;

            await _unitOfWork.GenreRepository.AddAsync(genre);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.GenreRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GenreModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<GenreModel>>(await _unitOfWork.GenreRepository.GetAllWithDetailsAsync());
        }

        public async Task<GenreModel> GetByIdAsync(int id)
        {
            return _mapper.Map<GenreModel>(await _unitOfWork.GenreRepository.GetByIdWithDetailsAsync(id));
        }

        public async Task UpdateAsync(GenreModel model)
        {
            await _unitOfWork.GenreRepository.UpdateAsync(_mapper.Map<Genre>(model));
            await _unitOfWork.SaveAsync();
        }
    }
}
