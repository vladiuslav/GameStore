namespace GameStore.WebAPI.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int? RepliedCommentId { get; set; }
    }
}
