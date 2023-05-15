using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models
{
    public class UpdateCommentModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(600)]
        public string Text{ get; set; }
    }
}
