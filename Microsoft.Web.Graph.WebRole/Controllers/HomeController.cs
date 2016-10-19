using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Graph.WebRole.ViewModels;

namespace Microsoft.Web.Graph.WebRole.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewModelBase model = new ViewModelBase(HttpContext.ApplicationInstance.Context);
            model.PageTitle = "Microsoft Graph - Home";
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}