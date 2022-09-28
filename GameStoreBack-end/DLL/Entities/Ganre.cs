using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities
{
    public class Ganre :BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
