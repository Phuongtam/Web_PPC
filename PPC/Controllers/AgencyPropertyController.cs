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
                var project = db.PROPERTY.OrderByDescending(x => x.USER.ID==id).ToList();
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
           
            ViewBag.property_type = db.PROPERTY_TYPE.OrderByDescending(x => x.ID).ToList();
            ViewBag.district = db.DISTRICT.OrderByDescending(x => x.ID).Where(y => y.ID >= 31 && y.ID <= 54).ToList();
            ViewBag.ward = db.WARD.OrderByDescending(x => x.ID).Where(y => y.District_ID >= 31 && y.District_ID <= 54).ToList();
            ViewBag.street = db.STREET.OrderByDescending(x => x.ID).Where(y => y.District_ID >= 31 && y.District_ID <= 54).ToList();
            ViewBag.status = db.PROJECT_STATUS.OrderByDescending(x => x.ID).ToList();
            ViewBag.sale = db.USER.OrderByDescending(x => x.ID).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewProperty(PROPERTY p)
        {
            PROPERTY en = db.PROPERTY.FirstOrDefault(x => x.ID==p.ID);
            
            //PROPERTY en;
           


            return RedirectToAction("Index");

        }
        public void AvatarU(ref string s,ref PROPERTY p)
        {
        
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

            }
            else
            {
                //s = en.Avatar;

            }
        }
        private void ImagesU(ref PROPERTY p, ref string s)
        {
           
            string filename;
            string extension;
            string b;
            s = "";

            if (p.ImagesUpload == null)
            {
                s = p.Images;

            }

            else
            {

                foreach (var file in p.ImagesUpload)
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
                    else
                    {
                        s = p.Images;
                    }

                }



            }


        }
    }
}