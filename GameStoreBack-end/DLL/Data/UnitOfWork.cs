using DLL.Interafeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGameRepository GameRepository => throw new NotImplementedException();

        public IGanreRepository GanreRepository => throw new NotImplementedException();

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
