using PhotoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            if (ValueStore.token == null)
            {
                ViewBag.Logout = false;
                return View();
            }
            else
            {
                ViewBag.Logout = true;
                return RedirectToAction("Index","DropBox");
            }
            
        }

        public ActionResult About()
        {
            if (ValueStore.token == null)
            {
                ViewBag.Logout = false;

            }
            else
            {
                ViewBag.Logout = true;
            }

               return View();
        }

        public ActionResult Contact()
        {
            if (ValueStore.token == null)
            {
                ViewBag.Logout = false;

            }
            else
            {
                ViewBag.Logout = true;
            }
            return View();
        }
    }
}