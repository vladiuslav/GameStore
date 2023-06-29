namespace GameStore.WebAPI.Models.UserModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? AvatarImageUrl { get; set; }
        public ICollection<int> CommentsIds { get; set; }
    }
}
