using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TekMarket.Models;

namespace TekMarket.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {


            return Redirect("/Admin/Index");


            //return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(FormCollection formCollection)
        {
            String email = formCollection["email"];
            String pwd = formCollection["pwd"];


            if (ModelState.IsValid)
            {
                DBModel db=new DBModel();
                List<Admin> la=db.Admins.ToList();
                int i = 0;
                while (i < la.Count && !la.ElementAt(i).email.Equals(email))
                {
                    i++;
                }

                if (i < la.Count)
                {
                    if (la.ElementAt(i).pwd.Equals(pwd))
                    {
                        FormsAuthentication.SetAuthCookie(email, false);

                        return Redirect("/Admin/Home");
                    }
                    else
                    {
                       
                        return RedirectToAction("Login","Login");


                    }

                }
                else
                {
                    return Redirect("/Login/Login");

                }




            }





            return Redirect("/Login/Login");


        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Admin/Index");

        }



        [AllowAnonymous]
        [Route("Account/Login")]
        public ActionResult LoginAuth()
        {


            FormsAuthentication.SignOut();
            return Redirect("/Admin/Index");


            //return View();
        }


    }
}