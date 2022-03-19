#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AptidudeTest.Data;
using AptidudeTest.Models;
using System.Data;
using AptidudeTest.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace AptidudeTest.Controllers
{
    [Authorize(Roles = "MANAGER")]
    public class ExamsController : Controller
    {
        private IExamRepository _repository;

        public ExamsController(IExamRepository repository)
        {
            _repository = repository;
        }

        // GET: Exams

        public IActionResult Index()
        {
            return View(_repository.GetExamsDetails());
        }
    
        public IActionResult Intro(int id)
        {


            return View(_repository.GetExamById(id));
        }

        // GET: Exams/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Exam exam = _repository.GetById(id);
            return View(exam);
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ExamName,ExamScore,Status")] Exam exam)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _repository.Insert(exam);
                    _repository.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(exam);
        }

        // GET: Exams/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Exam exam = _repository.GetById(id);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ExamName,PassScore,Time")] Exam exam)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _repository.Update(exam);
                    _repository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(exam);
        }

        // GET: Exams/Delete/5
        public IActionResult Delete(int id)
        {
            Exam model = _repository.GetById(id);
            return View(model);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return RedirectToAction("Index", "Exams");
        }

    }
}
