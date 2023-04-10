using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models
{
    public class CommentCreateModel
    {
        [Required]
        [MaxLength(600)]
        public string Text { get; set; }
        [Required]
        public int GameId { get; set; }
        public int? RepliedCommentId { get; set; }
    }
}
