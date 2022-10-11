using Moq;
using DLL.Repositories;
using DLL.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestsForAllLevels.DLL
{
    [TestFixture]
    public class GameRepositoryTests
    {
        [Test]
        public async Task Game_GetAllAsync_ReturnsAllValues()
        {
            var options = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseInMemoryDatabase("AuthorDb").Options;
            var context = new GameStoreDbContext(options);

            var gameRepository = new GameRepository(context);


            var games = await gameRepository.GetAllAsync();

            IEnumerable<Game> expectedGames = new List<Game>();
            Assert.AreEqual(games.Count(),expectedGames.Count());
        }
    }
}
