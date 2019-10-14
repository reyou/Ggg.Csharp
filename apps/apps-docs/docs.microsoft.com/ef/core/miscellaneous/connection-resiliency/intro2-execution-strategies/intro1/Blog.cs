using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace intro1
{
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Guid BlogId { get; set; }
    }
}