//BookViewModel.cs
using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels
{
    public class BookViewModel
    {
        //BOOK--------------------------------------------------
        public int BookId { get; set; }
        
        [Required(ErrorMessage = "Book Title is required.")]
        public string? Title { get; set; }
        

        //AUTHOR--------------------------------------------------
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public List<Author> AuthorList { get; set; } = new List<Author>();

        [Display(Name = "Author")]
        public List<int> SelectedAuthorId { get; set; } = new List<int>();

        

        public Author Author { get; set; }



        //BRANCH--------------------------------------------------
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public List<Branch> BranchList { get; set; } = new List<Branch>();
        public List<int> SelectedBranchId { get; set; } = new List<int>();
        
        
        public Branch Branch { get; set; }
        
        
        //CUSTOMER--------------------------------------------------
        public List<Customer> CustomerList { get; set; } = new List<Customer>();
        public Customer? CheckedOutByCustomer { get; set; }
        public string GetStatus()
        {
            return (CheckedOutByCustomer == null && BranchList.Any()) ? "Available" : "Not available";
        }       
        
    }
}