using BotDetect;
using Models.DAO;
using Models.Framework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        StaffDAO staffDAO = new StaffDAO();
        // GET: Admin/Staff
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getStaff()
        {
            var staffs = new StaffDAO().getAllStaff();
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
            var result = JsonConvert.SerializeObject(staffs, Formatting.Indented, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(Staff staff)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    staffDAO.Create(staff);
                    return Json(new
                    {
                        message = true,
                        JsonRequestBehavior.AllowGet
                    });
                }
                catch
                {
                    return Json(new
                    {
                        message = false,
                        JsonRequestBehavior.AllowGet

                    });
                }
            }
            return Json(new { message = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(long id)
        {
            ViewBag.Id = id;
            return View();
        }

        public JsonResult getById(int id)
        {
            var staff = new StaffDAO().getById(id);
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(staff, Formatting.Indented, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Staff s)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    staffDAO.Edit(s);
                    return Json(new
                    {
                        message = true,
                        JsonRequestBehavior.AllowGet
                    });
                }        
                catch
                {
                    return Json(new
                    {
                        message = false,
                        JsonRequestBehavior.AllowGet
                    });
                }
            }
            return Json(new { message = false }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        { 
            if(ModelState.IsValid)
            {
                try
                {
                    staffDAO.Delete(id);
                    return Json(new
                    {
                        message = true,
                        JsonRequestBehavior.AllowGet
                    });

                }
                catch
                {
                    return Json(new
                    {
                        message = false,
                        JsonRequestBehavior.AllowGet

                    });
                }
            }
            return Json(new { message = false }, JsonRequestBehavior.AllowGet);
        }
    }
}