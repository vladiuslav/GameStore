using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models.GameModels
{
    public class GameUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        public string Price { get; set; }
        public ICollection<int> GenresIds { get; set; }
    }
}
