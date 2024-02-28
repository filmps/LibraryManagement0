//CustomerViewModel.cs
using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels
{
    public class CustomerViewModel
    {
        //CUSTOMER--------------------------------------------------
        public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Customer name is required.")]
        public string? CustomerName { get; set; }    


        //BOOK--------------------------------------------------
        public List<Book> BookList { get; set; } = new List<Book>();    
        public List<int> SelectedBookId { get; set; } = new List<int>();
    }
}