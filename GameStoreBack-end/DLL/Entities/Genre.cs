using GameStore.DataLogic.Entities;
using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Genre :BaseEntity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
        public int? ParentGenreId { get; set; } = null;
        public Genre? ParentGenre { get; set; } = null;
    }
}
