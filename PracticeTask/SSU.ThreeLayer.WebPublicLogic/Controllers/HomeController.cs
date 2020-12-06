using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.Common;
using SSU.ThreeLayer.Entities;
using SSU.ThreeLayer.WebPublicLogic.Models;

namespace SSU.ThreeLayer.WebPublicLogic.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserBusinessLogic _userBusinessLogic;
        public HomeController()
        {
            _userBusinessLogic = Common.DependencyResolver.UserBusinessLogic;
        }

        public ActionResult Index()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    return RedirectToAction("Index", "Shop");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch(FormatException e)
            {
                ViewBag.error = e.Message;
                return View();
            }

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserVM user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _userBusinessLogic.AddUser(new User(user.Login, user.Password, user.Name, user.DateOfBirth, user.Info));
                        return RedirectToAction("Login");
                    }
                    catch (ArgumentException e)
                    {
                        ViewBag.error = "This login is already occupied.";
                        return View();
                    }
                    catch (Exception e)
                    {
                        ViewBag.error = "Invalid data.";
                        return View();
                    }
                }
                return View();
            }
            catch (FormatException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_userBusinessLogic.LogIn(login, password))
                    {
                        var temp = _userBusinessLogic.GetCurrentUser();
                        Session["UserId"] = temp.Id;
                        Session["UserLogin"] = temp.Name;
                        Session["UserName"] = temp.Name;
                        Session["UserAdminStatus"] = temp.IsAdmin ? "Administrator" : "Regular user";
                        return RedirectToAction("Index", "Shop");
                    }
                    else
                    {
                        ViewBag.error = "Login failed";
                        return RedirectToAction("Login");
                    }
                }
                return View();
            }
            catch (FormatException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult RateShop()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            var model = new RatingVM();
            return View(model);
        }

        [HttpPost]
        public ActionResult RateShop(RatingVM rating)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    _userBusinessLogic.RateShop(rating.ID, int.Parse(rating.SelectedRating), int.Parse(Session["UserId"].ToString()));
                }
                return RedirectToAction("Index");
            }
            catch (FormatException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }
    }
}