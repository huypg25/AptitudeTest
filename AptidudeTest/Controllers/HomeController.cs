using AptidudeTest.Data;
using AptidudeTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AptidudeTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("MANAGER"))
                return LocalRedirect(Url.Content("~/Exams"));
            return View();
        }
        [Authorize(Roles = "MANAGER")]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = "MANAGER,CANDIDATE")]
        public IActionResult TestPage(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            ViewData["FullName"] = user.FullName;
            ViewData["Phone"] = user.Phone;
            ViewData["Id"] = user.Id;
            if (_context.Results.Any(x => x.ExamId == id && x.UserId == user.Id)) return View("Views/Home/Error.cshtml");

            var data = _context.Exams
                        .Include(q => q.Questions)
                        .ThenInclude(c => c.Choices)
                        .FirstOrDefault(n => n.Id == id);
            return View(data);
        }
        //public double SectionTotal(IFormCollection iFormCollection)
        //{
        //    double total = 0;
        //    string[] questionIds = iFormCollection["questionId"];

        //    foreach (var questionId in questionIds)
        //    {
        //        var questionDetail = _context.Questions
        //                            .Include(c => c.Choices)
        //                            .FirstOrDefault(n => n.Id == int.Parse(questionId));
        //        var choiceIdCorrect = questionDetail.Choices.FirstOrDefault(x => x.IsCorrect == true).Id;
        //        if (choiceIdCorrect == int.Parse(iFormCollection["question_" + questionId]))
        //        {
        //            total+= questionDetail.Point;
        //        }
        //    }
        //    int a = 5;
        //    return total;
        //}
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}