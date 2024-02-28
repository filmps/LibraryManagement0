// CustomerController.cs
using System;
using LibraryManagement;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MyDbContext _dbContext;

        public CustomerController(MyDbContext dbContext)
        {
            _dbContext = (MyDbContext?)dbContext;
        }

        //======================VIEW======================
        //GO TO INDEX VIEW---------------------------
        public IActionResult Index()
        {
            return View(_dbContext.Customers.ToList());
        }
        //GO TO CREATE VIEW---------------------------
        public IActionResult Create()
        {
            return View();
        }
        //GO TO EDIT VIEW---------------------------
        public IActionResult Edit(int id)
        {
            // Retrieve the customer from the database
            var customer = _dbContext.Customers.Find(id);    
            if (customer == null)
            {
                return NotFound(); // Handle case where customer with specified id is not found
            }
            // Convert the Customer model to CustomerViewModel
            var customerViewModel = new CustomerViewModel
            {
                CustomerId = customer.ID,
                // Map other properties as needed
            };

            return View(customerViewModel);
            // var customer = _dbContext.Customers.Include(c => c.Books).FirstOrDefault(c => c.ID == id);
            // if (customer == null)
            // {
            //     return NotFound();
            // }

            // var bookIds = customer.Books.Select(b => b.ID).ToList();
            
            // var viewModel = new CustomerViewModel
            // {
            //     CustomerId = customer.ID,
            //     CustomerName = customer.CustomerName,
            //     SelectedBookId = bookIds,
            //     BookList = _dbContext.Books.ToList()
            // };

            // return View(viewModel);
        }
        //GO TO DETAILS VIEW---------------------------
        public IActionResult Details(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerViewModel
            {
                CustomerId = customer.ID,
                CustomerName = customer.CustomerName,
            };

            return View(viewModel);
            // var customer = _dbContext.Customers.Include(c => c.Books).FirstOrDefault(c => c.ID == id);
            // if (customer == null)
            // {
            //     return NotFound();
            // }
            
            // var bookIds = customer.Books.Select(a => a.ID).ToList();
            
            // var viewModel = new CustomerViewModel
            // {
            //     CustomerId = customer.ID,
            //     CustomerName = customer.CustomerName,
            //     SelectedBookId = bookIds,
            //     BookList = customer.Books
            // };

            // return View(viewModel);
        }
        //======================VIEW======================



        //======================ACTION======================
        //SUBMIT CREATE---------------------------
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        //SUBMIT DELETE---------------------------
        public IActionResult Delete(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //SUBMIT EDIT---------------------------
        [HttpPost]
        public IActionResult Edit(int id, CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.BookList = _dbContext.Books.ToList();
                return View(viewModel);
            }
            
            var customer = _dbContext.Customers.Include(c => c.Books).FirstOrDefault(c => c.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.CustomerName = viewModel.CustomerName;
            customer.Books.Clear();
            if (viewModel.SelectedBookId.Any())
            {
                foreach (var bookId in viewModel.SelectedBookId)
                {
                    var book = _dbContext.Books.Include(b => b.Customer).FirstOrDefault(b => b.BookId == bookId);
                    if (book != null)
                    {
                        if (book.Customer != null && book.Customer.ID != id)
                        {
                            ModelState.AddModelError("SelectedBookIds",
                                "One of the selected books has already been checked out by another customer.");
                            viewModel.BookList = _dbContext.Books.ToList();
                            return View(viewModel);
                        }
                        else
                        {
                            customer.Books.Add(book);
                        }
                    }
                }
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //======================ACTION======================


    }
}