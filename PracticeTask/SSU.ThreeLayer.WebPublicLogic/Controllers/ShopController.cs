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
    public class ShopController : Controller
    {
        private readonly IShopBusinessLogic _shopBusinessLogic;
        public ShopController()
        {
            _shopBusinessLogic = Common.DependencyResolver.ShopBusinessLogic;
        }

        // GET: Product
        public ActionResult Index()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            try
            {
                var shops = _shopBusinessLogic.GetAllShops().Select(x => new ShopVM(x.Id, x.Name, x.Type, x.AddressCity, x.AddressStreet, x.AddressBuilding, _shopBusinessLogic.GetShopRatingByIndex(x.Id))).ToList();
                return View(shops);
            }
            catch (FormatException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult AddPost(Shop shop)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            try
            {
                _shopBusinessLogic.AddShop(shop);
                return RedirectToAction("Index");
            }
            catch (FormatException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }
        
        [HttpGet]
        public ActionResult Search(string Name)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Home");

            try
            {
                if (String.IsNullOrEmpty(Name))
                {
                    var shops = _shopBusinessLogic.GetAllShops().Select(x => new ShopVM(x.Id, x.Name, x.Type, x.AddressCity, x.AddressStreet, x.AddressBuilding, _shopBusinessLogic.GetShopRatingByIndex(x.Id))).ToList();
                    return View(shops);
                }
                else
                {
                    ViewData["searchpattern"] = Name;
                    var tmp = _shopBusinessLogic.FindShopsByName(Name).Select(x => new ShopVM(x.Id, x.Name, x.Type, x.AddressCity, x.AddressStreet, x.AddressBuilding, _shopBusinessLogic.GetShopRatingByIndex(x.Id))).ToList();
                    return View(tmp);
                }
            }
            catch (FormatException e)
            {
                ViewBag.error = e.Message;
                return View();
            }
        }
    }
}