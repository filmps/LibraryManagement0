// Customer.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        public string? CustomerName { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public int? BorrowedBookId { get; set; }
    }
}