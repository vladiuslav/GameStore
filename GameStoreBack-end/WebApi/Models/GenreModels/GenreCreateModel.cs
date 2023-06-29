using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models.GenreModels
{
    public class GenreCreateModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public int? ParentGenreId { get; set; }
    }
}
