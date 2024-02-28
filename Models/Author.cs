// Author.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Author
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Author name is required.")]
        public string? AuthorName { get; set; }
        public List<Book> Books { get; } = new List<Book>();
        // public int? SelectedBookId { get; set; }
    }
}