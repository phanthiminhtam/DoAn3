using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models.DAO;
using Models.Framework;
using Newtonsoft.Json;
using WebsiteCosmetic.Common;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class ProviderController : Controller
    {
        ProviderDAO providerDAO = new ProviderDAO();
        // GET: Admin/Provider
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getAllProvider()
        {
            WebCosmeticEntities db = new WebCosmeticEntities();
            return Json(db.Providers.Select(s => new { s.PrvId, s.PrvName,s.Email,s.Address,s.PhoneNumber }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(Provider prv)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    providerDAO.Create(prv);
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
            if (ModelState.IsValid)
            {
                try
                {
                    providerDAO.Delete(id);
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
        public ActionResult Edit(long id)
        {
            ViewBag.Id = id;
            return View();
        }

        public JsonResult getById(int id)
        {
            var provider = new ProviderDAO().getById(id);
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(provider, Formatting.Indented, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public JsonResult Edit(Provider provider)
        {
            bool tg = true;
            try
            {
                tg = providerDAO.Edit(provider);
                return Json(tg);
            }
            catch
            {
                return Json(tg);
            }

        }
    }
}