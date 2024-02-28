// BookController.cs
using System;
using LibraryManagement;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly MyDbContext _dbContext;

        public BookController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //======================VIEW======================
        //GO TO INDEX VIEW---------------------------
        public IActionResult Index()
        {
            return View(_dbContext.Books.ToList());
        }
        //GO TO CREATE VIEW---------------------------
        public IActionResult Create()
        {
            var authors = _dbContext.Authors.ToList();
            var branches = _dbContext.Branches.ToList();
            var customers = _dbContext.Customers.ToList();

            var viewModel = new BookViewModel
            {
                AuthorList = authors,
                BranchList = branches,
                CustomerList = customers
            };

            return View(viewModel);
        }
        //GO TO EDIT VIEW---------------------------
        public IActionResult Edit(int id)
        {
            // Retrieve the book from the database
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound(); // Handle case where book with specified id is not found
            }
            // Convert the Book model to BookViewModel
            var authors = _dbContext.Authors.ToList();
            var branches = _dbContext.Branches.ToList();
            var customers = _dbContext.Customers.ToList();

            var bookViewModel = new BookViewModel
            {
                BookId = book.BookId,
                AuthorList = authors,
                BranchList = branches,
                CustomerList = customers
                // Map other properties as needed
            };

            return View(bookViewModel);
        }
        //GO TO DETAILS VIEW---------------------------
        public IActionResult Details(int id)
        {
            // var book = _dbContext.Books.Find(id);
            var book = _dbContext.Books
            .Include(b => b.Author) // Include the Author related data
            .Include(b => b.Branch) // Include the Branch related data
            .FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorId = book.Author?.ID ?? 0,
                AuthorName = book.Author?.AuthorName ?? "Unknown",
                BranchId = book.Branch?.ID ?? 0,
                BranchName = book.Branch?.BranchName ?? "Unknown"
                // Other properties...
            };

            return View(viewModel);
        }
        //======================VIEW======================



        //======================ACTION======================
        //SUBMIT CREATE---------------------------
        // [HttpPost]
        // public IActionResult Create(BookViewModel viewModel)
        // {
            // // if (viewModel.SelectedAuthorIds == null || viewModel.SelectedAuthorIds.Count == 0)
            // if (!viewModel.SelectedAuthorId.Any())
            // {
            //     ModelState.AddModelError("SelectedAuthorId",
            //         "Select at least one author. If the author is not in the list, create first.");
            // }
            
            // if (_dbContext.Branches.Any() && !viewModel.SelectedBranchId.Any())
            // {
            //     ModelState.AddModelError("SelectedBranchId",
            //         "Select a branch.");
            // }
            
            // if (viewModel.SelectedBranchId.Count > 1)
            // {
            //     ModelState.AddModelError("SelectedBranchId",
            //         "Only one branch can be selected.");
            // }
            // if (!ModelState.IsValid)
            // {
            //     viewModel.AuthorList = _dbContext.Authors.OrderBy(a => a.AuthorName).ToList();
            //     viewModel.BranchList = _dbContext.Branches.ToList();
            //     return View(viewModel);
            // }
            
            // var book = new Book
            // {
            //     Title = viewModel.Title,
            // };
            // if (viewModel.SelectedAuthorId.Any())
            // {
            //     foreach (var authorId in viewModel.SelectedAuthorId)
            //     {
            //         var author = _dbContext.Authors.Find(authorId);
            //         if (author != null)
            //         {
            //             // book.Authors.Add(author);
            //         }
            //     }
            // }
            // if (viewModel.SelectedBranchId.Any())
            // {
            //     var branchId = viewModel.SelectedBranchId.First();
            //     var branch = _dbContext.Branches.Find(branchId);
            //     if (branch != null)
            //     {
            //         book.Branch = branch;
            //     }
            // }
            
            // _dbContext.Books.Add(book);
            // _dbContext.SaveChanges();
            // return RedirectToAction("Index");
        // }

        //---------------------------
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        


        // SUBMIT EDIT ---------------------------
        [HttpPost]
        public IActionResult Edit(int id, BookViewModel viewModel)
        {
            //---------------------------------------
            if (ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            var book = _dbContext.Books
                .Include(b => b.Author)  // Include related authors
                .Include(b => b.Branch)   // Include related branch
                .FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            // Update book properties
            book.Title = viewModel.Title;

            // Update related entities
            var author = _dbContext.Authors.Find(viewModel.AuthorId);
            book.Author = author;


            // Update related branch
            var branch = _dbContext.Branches.Find(viewModel.BranchId);
            book.Branch = branch;

            // Save changes
            _dbContext.SaveChanges();

            return RedirectToAction("Index");

            //---------------------------------------
            // if (!ModelState.IsValid)
            // {
            //     return View(viewModel);
            // }
            
            // var book = _dbContext.Books.Find(id);
            // if (book == null)
            // {
            //     return NotFound();
            // }

            // book.Title = viewModel.Title;
            // _dbContext.SaveChanges();
            // return RedirectToAction("Index");
        }
        //SUBMIT DELETE---------------------------
        public IActionResult Delete(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        //======================ACTION======================

    }
}