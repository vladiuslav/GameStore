using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models.GenreModels
{
    public class GenreUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public ICollection<int> GamesIds { get; set; }
        public int? ParentGenreId { get; set; }
    }
}
