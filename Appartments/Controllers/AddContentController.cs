using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appartments.Models;

using System.Web.Security;

namespace Appartments.Controllers
{
    public class AddContentController : Controller
    {
        //
        // GET: /AddContent/


        [HttpGet]
        public ActionResult AdditionForm()
        {

            User u = new User();

            bool response = u.checkLimit(User.Identity.Name);

            System.Diagnostics.Debug.WriteLine("\n\n--------" + Convert.ToString(u.checkLimit(User.Identity.Name)) + "-------\n\n");

            if(!response)
                TempData["LimitRsponse"] = "0";
            else
                TempData["LimitRsponse"] = "1";
            

            return View();
        }


        [HttpPost]
        public ActionResult AdditionForm(Content c)
        {


           int response =    c.addContent(User.Identity.Name,
                             c.LandLordFirstName,
                             c.LandLordSecondName,
                             c.maleRestriction,
                             c.femaleRestriction,
                             c.maratialStatusRestrictionMaried,
                             c.maratialStatusRestrictionunMaried,
                             c.addressline1,
                             c.addressline2,
                             c.addressline3,
                             c.city,
                             c.pinCode,
                             c.washingMcCheck,
                             c.ovenCheck,
                             c.AcCheck,
                             c.numberOfRooms,
                             c.numberOfAttachedBaths,
                             c.MaxproposedRent,
                             c.MaxproposedRent);



           if (response == 0 || response == 2)
           {
               if(response == 0)
                   TempData["ContentUpdateCode"] = "0";

               if (response == 2)
                   TempData["ContentUpdateCode"] = "2";

               return RedirectToAction("Index", "Home");
           }

            return View();
        }

    }
}
