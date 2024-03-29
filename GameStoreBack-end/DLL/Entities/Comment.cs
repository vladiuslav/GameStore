﻿using DLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataLogic.Entities
{
    public class Comment : BaseEntity
    {

        [Required]
        [MaxLength(600)]
        public string Text { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public Game Game { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required] 
        public User User { get; set; }
        public int? RepliedCommentId { get; set; } = null;
        public Comment? RepliedComment { get; set; } = null;
    }
}
