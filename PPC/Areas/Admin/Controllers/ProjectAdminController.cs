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
            ViewBag.property_type = db.PROPERTY_TYPE.OrderByDescending(x=>x.ID==id).ToList();
            ViewBag.district = db.DISTRICT.OrderByDescending(x => x.ID == id).ToList();
            ViewBag.ward = db.WARD.OrderByDescending(x => x.ID == id).ToList();
            ViewBag.street = db.STREET.OrderByDescending(x => x.ID == id).ToList();
            ViewBag.status = db.PROJECT_STATUS.OrderByDescending(x => x.ID == id).ToList();
            ViewBag.sale = db.USER.OrderByDescending(x => x.ID == id).ToList();
            return View(project);
        }
        [HttpPost]
        public ActionResult Edit(int id, PROPERTY p)
        {
            ViewBag.property_type = db.PROPERTY_TYPE.OrderByDescending(x => x.ID).ToList();
            ViewBag.district = db.DISTRICT.OrderByDescending(x => x.ID).Where(y => y.ID >= 31 && y.ID <= 54).ToList();
            ViewBag.ward = db.WARD.OrderByDescending(x => x.ID).Where(y => y.District_ID >= 31 && y.District_ID <= 54).ToList();
            ViewBag.street = db.STREET.OrderByDescending(x => x.ID).Where(y => y.District_ID >= 31 && y.District_ID <= 54).ToList();
            ViewBag.status = db.PROJECT_STATUS.OrderByDescending(x => x.ID).ToList();

            var product = db.PROPERTY.FirstOrDefault(x => x.ID == id);

            string imgs = Images(p);
            string filename;
            string extension;
            string s;

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
                s = product.Avatar;

            }


            
            product.PropertyName = p.PropertyName;
            product.Price = p.Price;
            product.Avatar = p.Avatar;
            product.UnitPrice = p.UnitPrice;
            product.BathRoom = p.BathRoom;
            product.BedRoom = p.BedRoom;
            product.PackingPlace = p.PackingPlace;
            //product.USER = p.USER;
            product.Sale_ID = p.Sale_ID;
            product.DISTRICT = p.DISTRICT;
            product.PROJECT_STATUS = p.PROJECT_STATUS;
            product.PROPERTY_TYPE = p.PROPERTY_TYPE;
            product.Create_post = p.Create_post;
            product.Created_at = p.Created_at;
            product.Updated_at = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }


        public string Images(PROPERTY p)
        {

            var product = db.PROPERTY.Find(p.ID);
            string filename;
            string extension;
            string str = "";
            if (p.ImagesUpload != null)
            {
                foreach (var file in p.ImagesUpload)
                {
                    filename = Path.GetFileNameWithoutExtension(file.FileName);
                    extension = Path.GetExtension(file.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                    p.Images = filename;
                    str = p.Images + ",";
                    filename = Path.Combine(Server.MapPath("~/Images"), filename);
                    file.SaveAs(filename);
                }

            }
            else
            {
                str = product.Images;
            }

            return str;

        }

    }
}