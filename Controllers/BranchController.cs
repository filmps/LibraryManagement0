// AuthorController.cs
using System;
using LibraryManagement;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class BranchController : Controller
    {
        private readonly MyDbContext _dbContext;

        public BranchController(MyDbContext dbContext)
        {
            _dbContext = (MyDbContext?)dbContext;
        }

        //======================VIEW======================
        //GO TO INDEX VIEW---------------------------
        public IActionResult Index()
        {
            return View(_dbContext.Branches.ToList());
        }
        //GO TO CREATE VIEW---------------------------
        public IActionResult Create()
        {
            return View();
        }
        //GO TO EDIT VIEW---------------------------
        public IActionResult Edit(int id)
        {
            // Retrieve the branch from the database
            var branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return NotFound(); // Handle case where branch with specified id is not found
            }
            // Convert the Branch model to BranchViewModel
            var branchViewModel = new BranchViewModel
            {
                BranchId = branch.ID,
                // Map other properties as needed
            };

            return View(branchViewModel);

            // var branch = _dbContext.Branches.Find(id);
            // if (branch == null)
            // {
            //     return NotFound();
            // }

            // var viewModel = new BranchViewModel
            // {
            //     BranchId = branch.ID,
            //     BranchName = branch.BranchName,
            // };

            // return View(viewModel);
        }
        //GO TO DETAILS VIEW---------------------------
        public IActionResult Details(int id)
        {
            var branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            var viewModel = new BranchViewModel
            {
                BranchId = branch.ID,
                BranchName = branch.BranchName,
            };

            return View(viewModel);
        }
        //======================VIEW======================



        //======================ACTION======================
        //SUBMIT CREATE---------------------------
        [HttpPost]
        public IActionResult Create(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return View(branch);
            }
            
            _dbContext.Branches.Add(branch);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //SUBMIT DELETE---------------------------
        public IActionResult Delete(int id)
        {
            var branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            _dbContext.Branches.Remove(branch);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //SUBMIT EDIT---------------------------
        [HttpPost]
        public IActionResult Edit(int id, BranchViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            var branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            branch.BranchName = viewModel.BranchName;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        //======================ACTION======================

    }
}