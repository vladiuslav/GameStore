using DLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStrore.BusinessLogic.Models
{
    public class CommentModel
    { 
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int? RepliedCommentId { get; set; }
    }
}
