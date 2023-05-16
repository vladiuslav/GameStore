using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models.GameModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<int> GenresIds { get; set; }
        public ICollection<int> CommentsIds { get; set; }
    }
}
