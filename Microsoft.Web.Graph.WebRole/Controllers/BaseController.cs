using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Microsoft.Web.Graph.WebRole.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsLocal == true)
            {
                HttpContext.Response.ContentType = "text/html";
            }
            else
            {
                HttpContext.Response.ContentType = "text/fdxml";
            }
        }
    }
}