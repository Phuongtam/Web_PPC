using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPC.Models;

namespace PPC.Controllers
{
    public class HomeController : Controller
    {
        DemoPPCRentalEntities db = new DemoPPCRentalEntities();
     // List<SelectListItem> type, district;
        public ActionResult Index()
        {
            // commnet
            var model = db.PROPERTY.ToList();
            ViewBag.property_type = db.PROPERTY_TYPE.OrderByDescending(x => x.ID).ToList();
            ViewBag.district = db.DISTRICT.OrderByDescending(x => x.ID ).ToList();
            ViewBag.ward = db.WARD.OrderByDescending(x => x.ID).ToList();
            ViewBag.street = db.STREET.OrderByDescending(x => x.ID).ToList();
            
           // ListAll();
            return View(model);
         
        }
        public JsonResult GetStreet(int District_id)
        {
            return Json(
            db.STREET.Where(s => s.District_ID == District_id)
            .Select(s => new { id = s.ID, text = s.StreetName }).ToList(),
            JsonRequestBehavior.AllowGet);
        }
        //
        //
        public void ListAll()
        {
            
          
            //type = new List<SelectListItem>();
            //district = new List<SelectListItem>();
            //var typee = db.PROPERTY_TYPE.ToList().OrderBy(x => x.CodeType);
            //var distric1 = db.DISTRICT.ToList().OrderBy(x => x.DistrictName);
            ////
            //type.Add(new SelectListItem { Text="All",Value="all" });
            //foreach (var item  in typee)
            //{
            //    type.Add(new SelectListItem { Text = item.CodeType, Value = item.CodeType });

            //}
            //ViewData["loai"] = type;
            ////
            //district.Add(new SelectListItem { Text = "All", Value = "all" });
            //foreach (var item in distric1)
            //{
            //    district.Add(new SelectListItem { Text = item.DistrictName, Value = item.DistrictName });

            //}
            //ViewData["quan"] = district;
        }
        [HttpGet]
        public ActionResult Search(string text)
        {
          
           var product = db.PROPERTY.ToList().Where(x => x.PropertyName.Contains(text)
                || x.Content.Contains(text) || x.Price.ToString().Contains(text));
            //var product = db.PROPERTY.ToList().Where(x => x.PropertyName.Contains(text)
            //    && x.DISTRICT.DistrictName==quan || x.PROPERTY_TYPE.CodeType==loai);
            ListAll();
          
            return View(product);
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