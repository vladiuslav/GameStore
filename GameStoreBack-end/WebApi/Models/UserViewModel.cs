﻿namespace WebApi.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? AvatarImageUrl { get; set; }
    }
}
