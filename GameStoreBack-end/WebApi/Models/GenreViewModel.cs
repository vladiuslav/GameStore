using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public ICollection<int> GamesIds { get; set; }
        public int? ParentGenreId { get; set; }
    }
}
