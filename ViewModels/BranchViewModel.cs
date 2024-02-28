//BranchViewModel.cs
using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels
{
    public class BranchViewModel
    {
        public int BranchId { get; set; }
        
        [Required(ErrorMessage = "Branch name is required.")]
        public string? BranchName { get; set; }    

        public List<Book> BookList { get; set; } = new List<Book>();
        public List<int> SelectedBookId { get; set; } = new List<int>();
    }
}