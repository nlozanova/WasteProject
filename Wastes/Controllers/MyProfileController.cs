using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wastes.ViewModels;
using Wastes.Models;

namespace Wastes.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        //Database connection       
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Edit()
        {
            //Getts the logged user
            string userId = User.Identity.GetUserId();
            var userDb = db.Users.Find(userId);
            
            ProfileVM profileVM = new ProfileVM()
            {
                Id = userDb.Id,
                Email = userDb.Email,
                PhoneNumber = userDb.PhoneNumber,
            };

            return View(profileVM);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileVM passedProfile)
        {
            //Getts the logged user
            string userId = User.Identity.GetUserId();
            var userDB = db.Users.Find(userId);

            //Passes the new data to the db model 
            userDB.Email = passedProfile.Email;
            userDB.PhoneNumber = passedProfile.PhoneNumber;

            if (ModelState.IsValid)
            {
                db.Entry(userDB).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.SuccessMessage = "Успешен запис!";
                return View("~/Views/Shared/SuccessPage.cshtml");
            }
            return View(passedProfile);
        }
    }
}