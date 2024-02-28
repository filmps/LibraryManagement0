// Branch.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Branch
    {
        public int ID { get; set; }
        public string? BranchName { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}