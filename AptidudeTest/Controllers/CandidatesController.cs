using AptidudeTest.Data.Services;
using AptidudeTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AptidudeTest.Controllers
{
    public class CandidatesController : Controller
    {
        private ICandidateRepository _repository;

        private static int _id;
        public CandidatesController(ICandidateRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }
        [HttpGet]
        [Route("Candidates/Edit/{email}")]
        public IActionResult Edit(string email)
        {
            if (email == null)
            {
                return NotFound();
            }
            var model = _repository.GetByEmail(email);
            return View(model);
        }


        // POST: Choices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Candidates/Edit/{email}")]
        [ValidateAntiForgeryToken]
        //public IActionResult Edit(string email, [Bind("Email,FullName,Phone,Address,DateOfBirth,Education,WorkExperience")] ApplicationUser applicationUser)
        //{
        //    if (email != applicationUser.Email)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            ApplicationUser user = Microsoft.AspNet.Identity.UserManager.FindById(model.Id);

        //            // Update it with the values from the view model
        //            user.Name = model.Name;
        //            user.Surname = model.Surname;
        //            user.UserName = model.UserName;
        //            user.Email = model.Email;
        //            user.PhoneNumber = model.PhoneNumber;
        //            user.Number = model.Number; //custom property
        //            user.PasswordHash = checkUser.PasswordHash;

        //            // Apply the changes if any to the db
        //            UserManager.Update(user);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ApplicationUserExists(applicationUser.Email))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    return RedirectToAction("Index", "Candidates");
        //}

        private bool ApplicationUserExists(string email)
        {
            return _repository.GetAll().Any(e => e.Email == email);
        }
    }
}
