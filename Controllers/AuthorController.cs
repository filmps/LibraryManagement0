// AuthorController.cs
using System;
using LibraryManagement;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly MyDbContext _dbContext;

        public AuthorController(MyDbContext dbContext)
        {
            _dbContext = (MyDbContext?)dbContext;
        }

        //======================VIEW======================
        //GO TO INDEX VIEW---------------------------
        public IActionResult Index()
        {
            return View(_dbContext.Authors.ToList());
        }
        //GO TO CREATE VIEW---------------------------
        public IActionResult Create()
        {
            return View();
        }
        //GO TO EDIT VIEW---------------------------
        public IActionResult Edit(int id)
        {
            // Retrieve the author from the database
            var author = _dbContext.Authors.Find(id);
            if (author == null)
            {
                return NotFound(); // Handle case where author with specified id is not found
            }
            // Convert the author model to AuthorViewModel
            var authorViewModel = new AuthorViewModel
            {
                AuthorId = author.ID,
                // Map other properties as needed
            };

            return View(authorViewModel);

            // var author = _dbContext.Authors.Include(a => a.Books).FirstOrDefault(a => a.ID == id);
            // if (author == null)
            // {
            //     return NotFound();
            // }
            
            // var viewModel = new AuthorViewModel
            // {
            //     AuthorId = author.ID,
            //     AuthorName = author.AuthorName,
            // };

            // return View(viewModel);
        }
        //GO TO DETAILS VIEW---------------------------
        public IActionResult Details(int id)
        {
            var author = _dbContext.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            var viewModel = new AuthorViewModel
            {
                AuthorId = author.ID,
                AuthorName = author.AuthorName,
            };

            return View(viewModel);
            // var author = _dbContext.Authors.Include(b => b.Books).FirstOrDefault(b => b.ID == id);
            // if (author == null)
            // {
            //     return NotFound();
            // }
            
            // var viewModel = new AuthorViewModel
            // {
            //     AuthorId = author.ID,
            //     AuthorName = author.AuthorName,
            //     BookList = author.Books
            // };

            // return View(viewModel);
        }
        //======================VIEW======================



        //======================ACTION======================
        //SUBMIT CREATE---------------------------
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //SUBMIT DELETE---------------------------
        public IActionResult Delete(int id)
        {
            var author = _dbContext.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //SUBMIT EDIT---------------------------
        [HttpPost]
        public IActionResult Edit(int id, AuthorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            var author = _dbContext.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            author.AuthorName = viewModel.AuthorName;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //======================ACTION======================



    }
}