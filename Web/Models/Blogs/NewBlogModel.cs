using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Web.Models.Blogs
{
    public class NewBlogModel
    {
        [Required, MaxLength(60, ErrorMessage = "Blog title can't be longer than 60 characters.")]
        public string Title { get; set; }

        [Required, MinLength(60)] public string Content { get; set; }
        [Required] public IFormFile Banner { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}