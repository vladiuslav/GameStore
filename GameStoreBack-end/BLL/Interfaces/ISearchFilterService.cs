using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISearchFilterService
    {
        Task<IEnumerable<GameModel>> SearchGamesByName(string gameName);
        Task<IEnumerable<GameModel>> FilterGameByGanres(IEnumerable<int> ganresIds);
    }
}
