// Book.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        //BOOK---------------------------------
        public int BookId { get; set; }
        public string? Title { get; set; }


        //AUTHOR---------------------------------
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }


        //BRANCH---------------------------------
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
        

        //CUSTOMER---------------------------------
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        
        public string GetStatus()
        {
            return (CustomerId == null) ? "Available" : "Not available";
            // return (CustomerId == null && Branch != null) ? "Available" : "Not available";
        }
    }
}