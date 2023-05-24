using GameStore.DataLogic.Entities;
using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Game : BaseEntity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public string? ImageUrl { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
