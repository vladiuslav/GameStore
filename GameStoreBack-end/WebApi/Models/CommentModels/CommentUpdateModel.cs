using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models.CommentModels
{
    public class CommentUpdateModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(600)]
        public string Text { get; set; }
    }
}
