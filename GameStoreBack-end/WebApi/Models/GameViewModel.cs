using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class GameViewModel
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
        public string? ImageUrl { get; set; }
        public ICollection<int> GenresIds { get; set; }
        public ICollection<int> CommentsIds { get; set; }
    }
}
