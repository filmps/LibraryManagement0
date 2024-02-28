//AuthorViewModel.cs
using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        
        [Required(ErrorMessage = "Author name is required.")]
        public string? AuthorName { get; set; }
                
        public List<Book> BookList { get; set; } = new List<Book>();
        public List<int> SelectedBookId { get; set; } = new List<int>();
    }
}