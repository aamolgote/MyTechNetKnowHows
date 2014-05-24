using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateBundlingTestApp.Controllers
{
    public class TemplateController : Controller
    {
        //
        // GET: /Template/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Code()
        {
            return View();
        }

        public ActionResult ConfigFile()
        {
            return View();
        }

    }
}
