using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPC.Models;
using System.IO;

namespace PPC.Controllers
{
    public class AgencyPropertyController : Controller
    {
        DemoPPCRentalEntities db = new DemoPPCRentalEntities();
        // GET: AgencyProperty
        public ActionResult ListProperty()
        {
            if(Session["userID"]!=null)
            {
                var id = (int)Session["userID"];
                var project = db.PROPERTY.OrderByDescending(x => x.UserID==id).ToList();
                return View(project);
            }
           else
            {
                return View();
            } 
        }


        [HttpGet]
        public ActionResult CreateNewProperty()
        {
            ViewBag.District_ID = new SelectList(db.DISTRICT.Where(y => y.ID >= 31 && y.ID <= 54), "ID", "DistrictName");
            ViewBag.Status_ID = new SelectList(db.PROJECT_STATUS, "ID", "Status_Name");
            ViewBag.PropertyType_ID = new SelectList(db.PROPERTY_TYPE, "ID", "CodeType");
            ViewBag.Street_ID = new SelectList(db.STREET.Where(y => y.ID >= 31 && y.ID <= 54), "ID", "StreetName");
            ViewBag.UserID = new SelectList(db.USER, "ID", "Email");
            ViewBag.Sale_ID = new SelectList(db.USER, "ID", "Email");
            ViewBag.Ward_ID = new SelectList(db.WARD.Where(y => y.ID >= 31 && y.ID <= 54), "ID", "WardName");
            ViewBag.Feature_ID = new SelectList(db.FEATURE, "ID", "FeatureName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewProperty(PROPERTY property)
        {

            property.Avatar = AvatarU(property);
            property.Images = ImagesU(property);
            property.Created_at = DateTime.Now;
            property.Create_post = DateTime.Now;
            property.UnitPrice = "VND";
            property.Sale_ID = 1;
            property.Status_ID = 1;
            property.UserID = 1;

            if (ModelState.IsValid)
            {

                db.PROPERTY.Add(property);
                db.SaveChanges();
                return Redirect("~/AgencyProperty/ListProperty");
            }

            ViewBag.District_ID = new SelectList(db.DISTRICT, "ID", "DistrictName", property.District_ID);
            ViewBag.Status_ID = new SelectList(db.PROJECT_STATUS, "ID", "Status_Name", property.Status_ID);
            ViewBag.PropertyType_ID = new SelectList(db.PROPERTY_TYPE, "ID", "CodeType", property.PropertyType_ID);
            ViewBag.Street_ID = new SelectList(db.STREET, "ID", "StreetName", property.Street_ID);
            ViewBag.UserID = new SelectList(db.USER, "ID", "Email", property.UserID);
            ViewBag.Sale_ID = new SelectList(db.USER, "ID", "Email", property.Sale_ID);
            ViewBag.Ward_ID = new SelectList(db.WARD, "ID", "WardName", property.Ward_ID);
            return View(property);
        }


        private string ImagesU(PROPERTY p)
        {

            string filename;
            string extension;
            string b;
            string s = "";
            foreach (var file in p.UpImages)
            {
                if (file.ContentLength > 0)
                {
                    filename = Path.GetFileNameWithoutExtension(file.FileName);
                    extension = Path.GetExtension(file.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                    p.Images = filename;
                    b = p.Images;
                    s = string.Concat(s, b, ",");
                    filename = Path.Combine(Server.MapPath("~/Images"), filename);
                    file.SaveAs(filename);

                }

            }
            return s;

        }
        private string AvatarU(PROPERTY p)
        {
            string s = "";
            string filename;
            string extension;


            if (p.AvatarUpload != null)
            {
                filename = Path.GetFileNameWithoutExtension(p.AvatarUpload.FileName);
                extension = Path.GetExtension(p.AvatarUpload.FileName);
                filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                p.Avatar = filename;
                s = p.Avatar;
                filename = Path.Combine(Server.MapPath("~/Images"), filename);
                p.AvatarUpload.SaveAs(filename);
                return s;

            }
            return s;

        }




    }
}