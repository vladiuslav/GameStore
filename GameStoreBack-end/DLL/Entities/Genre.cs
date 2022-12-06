using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Ganre :BaseEntity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
