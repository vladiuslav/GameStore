using BLL.Interfaces;
using BLL.Models;
using DLL.Data;
using DLL.Interafeces;
using DLL.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
                }
            );
            _mapper = new Mapper(config);
        }
        public GameService()
        {
            var options = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=GameStoreDB;Trusted_Connection=True;")
                .Options;
            _unitOfWork = new UnitOfWork(options);
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }
            );
            _mapper = new Mapper(config);
        }

        public async Task AddAsync(GameModel model)
        {
            var game = _mapper.Map<Game>(model);
            ICollection<Genre> genres= new List<Genre>();
            IEnumerable<Genre> genresDb = await _unitOfWork.GenreRepository.GetAllWithDetailsAsync();
            foreach (var item in model.GenresIds)
            {
                var genre = genresDb.FirstOrDefault(g => g.Id == item);
                if (genre!=null) {
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
            await _unitOfWork.GameRepository.UpdateAsync(_mapper.Map<Game>(model));
            await _unitOfWork.SaveAsync();
        }
    }
}
