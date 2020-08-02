using OdeToFood.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Web.Controllers
{
    public class GrettingController : Controller
    {
        // GET: Gretting
        public ActionResult Index(string name)
        {
            var model = new GreetingViewModel();
            //You could access to a querystring  in this way but in MVC you will user more parameters in the method.
            //var name = HttpContext.Request.QueryString["name"];
            model.Name = name??"no name";
            model.Message = ConfigurationManager.AppSettings["message"];
            return View(model);
        }
    }
}