using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPC.Models;
using System.IO;

namespace PPC.Areas.Admin.Controllers
{
    public class ProjectAdminController : Controller
    {
        DemoPPCRentalEntities db = new DemoPPCRentalEntities();
        // GET: Admin/ProjectAdmin
        public ActionResult Index()
        {
            var project = db.PROPERTY.OrderByDescending(x => x.ID).ToList();
            return View(project);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var project = db.PROPERTY.FirstOrDefault(x => x.ID == id);
            ViewBag.property_type = db.PROPERTY_TYPE.OrderByDescending(x => x.ID).ToList();
            ViewBag.district = db.DISTRICT.OrderByDescending(x => x.ID).Where(y => y.ID >= 31 && y.ID <= 54).ToList();
            ViewBag.ward = db.WARD.OrderByDescending(x => x.ID).Where(y => y.District_ID >= 31 && y.District_ID <= 54).ToList();
            ViewBag.street = db.STREET.OrderByDescending(x => x.ID).Where(y => y.District_ID >= 31 && y.District_ID <= 54).ToList();
            ViewBag.status = db.PROJECT_STATUS.OrderByDescending(x => x.ID).ToList();
            ViewBag.sale = db.USER.OrderByDescending(x => x.ID).ToList();
            return View(project);
        }

        [HttpPost]
        public ActionResult Edit(int id, PROPERTY p)
        {
            //PROPERTY product;
            PROPERTY en;
            string s;
            string b;
            AvatarU(p, out en, out s);
            ImagesU(p, out en, out b);

            en.PROPERTY_TYPE = p.PROPERTY_TYPE;
            en.PropertyName = p.PropertyName;
            en.Avatar = s;
            en.Images = b;
            en.PropertyType_ID = p.PropertyType_ID;
            en.Content = p.Content;
            en.Street_ID = p.Street_ID;
            en.Ward_ID = p.Ward_ID;
            en.District_ID = p.District_ID;
            en.Price = p.Price;
            en.UnitPrice = p.UnitPrice;
            en.Area = p.Area;
            en.BedRoom = p.BedRoom;
            en.BathRoom = p.BathRoom;
            en.PackingPlace = p.PackingPlace;
            en.Updated_at = DateTime.Now;
            db.SaveChanges();

            return RedirectToAction("Index");
            
        }

        private void AvatarU(PROPERTY p, out PROPERTY en, out string s)
        {
            en = db.PROPERTY.Find(p.ID);
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
                s = en.Avatar;

            }
        }
        private void ImagesU(PROPERTY p, out PROPERTY en, out string s)
        {
            en = db.PROPERTY.Find(p.ID);
            string filename;
            string extension;
            string b;
            s = "";

            if (p.ImagesUpload == null)
            {
                s = en.Images;

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
                        s = en.Images;
                    }

                }



            }


        }

    }
}