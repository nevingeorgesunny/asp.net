using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Appartments.Models;

namespace Appartments.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [HttpGet]
        public ActionResult Index()
        {

            System.Diagnostics.Debug.WriteLine("\n\n---------------HOME INDEX ENTRY\n\n");
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(User u)
        {


         
            System.Diagnostics.Debug.WriteLine("\n\n---------------" + u.Email);
            System.Diagnostics.Debug.WriteLine("\n\n---------------" + u.Password);


            string response = u.logUser(u.Email, u.Password);


            if (response.Equals("false"))
            {


                System.Diagnostics.Debug.WriteLine("FAILED");
                return RedirectToAction("Index", "Home");

              

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("\n\n---------------" + u.FirstName + " " + u.LastName + "\n\n---------------");
                FormsAuthentication.SetAuthCookie(u.Email, true);
                return RedirectToAction("Index", "Home");
            }



            

            

        }



       [HttpPost]
        public ActionResult AddUser(User u)
        {

            System.Diagnostics.Debug.WriteLine("\n\n---------------" + u.FirstName);
            System.Diagnostics.Debug.WriteLine("\n\n---------------" + u.LastName);
            System.Diagnostics.Debug.WriteLine("\n\n---------------" + u.Email);
            System.Diagnostics.Debug.WriteLine("\n\n---------------" + u.Password);


            bool response = u.addUser(u.FirstName, u.LastName, u.Email, u.Password);


            if (response)
            {
                FormsAuthentication.SetAuthCookie(u.Email, true);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("FAILED");
                return RedirectToAction("Index", "Home");
            }


            
        }
      


        [HttpGet]
        public ActionResult testPost()
        {

            System.Diagnostics.Debug.WriteLine("\n\n---------------HOME TESTPOST ENTRY\n\n");

            return View();
        }

        [HttpPost]
        public int testPost(User u)
        {


            System.Diagnostics.Debug.WriteLine("\n" + u.FirstName);
            System.Diagnostics.Debug.WriteLine("\n" + u.LastName);
            System.Diagnostics.Debug.WriteLine("\n" + u.Email);
            System.Diagnostics.Debug.WriteLine("\n" + u.Password);


            bool response = u.addUser(u.FirstName, u.LastName, u.Email, u.Password);



            if (response)
            {
                System.Diagnostics.Debug.WriteLine("SUCCESS");
                return 200;

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("FAILED");
                return 207;
            }

         
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        

    }
}
