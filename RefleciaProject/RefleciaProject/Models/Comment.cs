﻿using System.ComponentModel.DataAnnotations;

namespace RefleciaProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
