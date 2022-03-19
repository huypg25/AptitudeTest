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
using Microsoft.AspNet.Identity;

namespace AptidudeTest.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Results.Include(r => r.Exam);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Exam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "ExamName");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection iFormCollection)
        {
            int count = 0;
            double total = 0;
            bool Status;
            string[] questionIds = iFormCollection["questionId"];
            var ExamId = iFormCollection["ExamId"];
            var UserId = iFormCollection["UserId"];
            var examDetails = _context.Exams
                                .Include(q => q.Questions)
                                .ThenInclude(c => c.Choices)
                                .FirstOrDefault(n => n.Id == int.Parse(ExamId));
            foreach (var questionId in questionIds)
            {
                var questionDetail = _context.Questions
                                    .Include(c => c.Choices)
                                    .FirstOrDefault(n => n.Id == int.Parse(questionId));
                var choiceIdCorrect = questionDetail.Choices.FirstOrDefault(x => x.IsCorrect == true).Id;
                if (choiceIdCorrect == int.Parse(iFormCollection["question_" + questionId]))
                {
                    total += questionDetail.Point;
                }
            }
            var PassScore = examDetails.Questions.Sum(q => q.Point);
            var ResultPercent = total / PassScore * 100;
            if (ResultPercent >= examDetails.PassScore)
            {
                Status = true;
            }
            else
            {
                Status = false;
            }

            var model = new Result()
            {
                ExamId = int.Parse(ExamId),
                UserId = UserId,
                ResultScore = total,
                PassScore = PassScore,
                CreatedAt = DateTime.Now,
                Status = Status,


            };
            _context.Add(model);
            _context.SaveChanges();
            //ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "ExamName", result.ExamId);
            if (int.Parse(ExamId) < 3)
            {
                var i = int.Parse(ExamId);
                i++;
                return RedirectToAction("testpage","Home",new { id = i });
            }
            var result = _context.Results.ToList().Where(x => x.UserId == UserId);
            foreach (var item in result)
            {
                if (item.Status = true) count++;
            }
            if (count == 3)
            {
                _context.Users.Find(UserId).Status = 1;
                _context.SaveChanges();
            }
            else
            {
                _context.Users.Find(UserId).Status = 0;
                _context.SaveChanges();
            }

            return View("FinalResult", result);
        }
        //[HttpGet]
        //public IActionResult FinalResult()
        //{
        //    string userId = TempData["UserId"].ToString();
        //    var result = _context.Results.ToArray().Where(x=>x.UserId == userId);
        //    return View(result);
        //}
        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "ExamName", result.ExamId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ResultScore,CreatedAt,Status,UserId,ExamId")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "ExamName", result.ExamId);
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Exam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.Results.FindAsync(id);
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.Id == id);
        }
    }
}
