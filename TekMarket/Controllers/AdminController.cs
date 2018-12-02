using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TekMarket.Models;

namespace TekMarket.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Home()
        {
            return View();
        }

       


    }
}