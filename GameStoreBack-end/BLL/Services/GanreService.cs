using BLL.Interfaces;
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
    public class GanreService : IGanreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public GanreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }
            );
            _mapper = new Mapper(config);
        }
        public GanreService()
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

        public async Task AddAsync(GanreModel model)
        {
            var ganre = _mapper.Map<Ganre>(model);
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
            ganre.Games = games;

            await _unitOfWork.GanreRepository.AddAsync(ganre);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.GanreRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GanreModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<GanreModel>>(await _unitOfWork.GanreRepository.GetAllWithDetailsAsync());
        }

        public async Task<GanreModel> GetByIdAsync(int id)
        {
            return _mapper.Map<GanreModel>(await _unitOfWork.GanreRepository.GetByIdWithDetailsAsync(id));
        }

        public async Task UpdateAsync(GanreModel model)
        {
            await _unitOfWork.GanreRepository.UpdateAsync(_mapper.Map<Ganre>(model));
            await _unitOfWork.SaveAsync();
        }
    }
}
