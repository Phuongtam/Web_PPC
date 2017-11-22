using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPC.Models;

namespace PPC.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        DemoPPCRentalEntities db = new DemoPPCRentalEntities();
        public ActionResult Detail(int id)
        {
            var project = db.PROPERTY.FirstOrDefault(x => x.ID == id);
            return View(project);
        }
    }
}