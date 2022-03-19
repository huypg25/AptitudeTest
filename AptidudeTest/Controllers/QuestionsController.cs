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

namespace AptidudeTest.Controllers
{
    public class QuestionsController : Controller
    {
        private IQuestionRepository _repository;
        private static int _id;

        public QuestionsController(IQuestionRepository repository)
        {
            _repository = repository;
        }

        // GET: Questions
        public IActionResult Index(int id)
        {
            _id = id;
            TempData["data"] = id;
            return View(_repository.GetAll().Where(x => x.ExamId == id));
        }

        // GET: Questions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Question model = _repository.GetById(id);
            return View(model);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {

            ViewData["ExamId"] = _id;
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Text,Point,ExamId")] Question question)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _repository.Insert(question);
                    _repository.Save();
                    return RedirectToAction("Index", "Questions", new { id = _id });
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(question);
        }

        // GET: Questions/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Choice"] = _repository.GetAllChoicesByQuestionId(id);
            Question model = _repository.GetById(id);
            ViewData["ExamId"] = _id;
            return View(model);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Text,Point,ExamId")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _repository.Update(question);
                    _repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Questions", new { id = _id });
            }
            ViewData["ExamId"] = _id;
            return View(question);
        }

        //GET: Questions/Delete/5
        public IActionResult Delete(int? id)
        {
            Question model = _repository.GetById(id);
            return View(model);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return RedirectToAction("Index", "Questions", new { id = _id });
        }

        private bool QuestionExists(int id)
        {
            return _repository.GetAll().Any(e => e.Id == id);
        }
    }
}
