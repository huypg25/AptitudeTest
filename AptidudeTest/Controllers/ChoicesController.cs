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
    public class ChoicesController : Controller
    {
        private IChoiceRepository _repository;
        private static int _id;
        public ChoicesController(IChoiceRepository repository)
        {
            _repository = repository;
        }


        // GET: Choices
        public IActionResult Index(int id)
        {
            _id = id;
            ViewData["ExamId"] = TempData["data"];

            return View(_repository.GetAll().Where(x => x.QuestionId == id));
        }

        // GET: Choices/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Choice model = _repository.GetById(id);
            return View(model);
        }

        // GET: Choices/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = _id;
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Text,IsCorrect,QuestionId")] Choice choice)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _repository.Insert(choice);
                    _repository.Save();
                    return RedirectToAction("Index", "Choices", new { id = _id });
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(choice);
        }

        // GET: Choices/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Choice model = _repository.GetById(id);
            ViewData["QuestionId"] = _id;
            return View(model);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Text,IsCorrect,QuestionId")] Choice choice)
        {
            if (id != choice.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _repository.Update(choice);
                    _repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoiceExists(choice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Choices", new { id = _id });
            }
            ViewData["QuestionId"] = _id;
            return View(choice);
        }

        // GET: Choices/Delete/5
        public IActionResult Delete(int? id)
        {
            Choice model = _repository.GetById(id);
            return View(model);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return RedirectToAction("Index", "Choices", new { id = _id });
        }

        private bool ChoiceExists(int id)
        {
            return _repository.GetAll().Any(e => e.Id == id);
        }
    }
}
