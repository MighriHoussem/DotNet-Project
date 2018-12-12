using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TekMarket.Models;
using TekMarket.CustomClasses;
using System.Data.SqlClient;
using System.Web.Helpers;

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

      

        public ActionResult ChartsFun()
        {
            DataBaseUtility db = new DataBaseUtility();
            db.init();
            SqlDataReader reader = db.caByUser();
            List<String> x = new List<string>();
            List<String> y = new List<string>();
            while (reader.Read())
            {
                x.Add((String)reader["nom"]);
                y.Add(reader["pr"].ToString());
            }


            var myChart = new Chart(width: 600, height: 400)
                            .AddTitle("net worth by client")
                            .AddSeries(
                                name: "client",
                                xValue: x.ToArray(),
                                yValues: y.ToArray())
                       .Write();
            return null;
        }

        public ActionResult ChartsFun1()
        {
            DataBaseUtility db = new DataBaseUtility();
            db.init();
            SqlDataReader reader = db.nbprodByCat();
            List<String> x = new List<string>();
            List<String> y = new List<string>();
            while (reader.Read())
            {
                x.Add((String)reader["id"]);
                y.Add(reader["nb"].ToString());
            }


            var myChart = new Chart(width: 600, height: 400)
                            .AddTitle("products by category")
                            .AddSeries(
                                name: "client",
                                xValue: x.ToArray(),
                                yValues: y.ToArray())
                       .Write();
            return null;
        }


    }
}