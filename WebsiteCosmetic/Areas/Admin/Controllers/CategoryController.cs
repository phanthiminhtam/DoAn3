using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using Newtonsoft.Json;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        CategoryDAO categoryDAO = new CategoryDAO();
        public ActionResult Index()
        {
            return View(categoryDAO.getAllCategory());
        }
        public ActionResult getCategory()
        {
            //var categories = new CategoryDAO().getAllCategory();
            //JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //var result = JsonConvert.SerializeObject(categories, Formatting.Indented, jss);
            //return Json(result, JsonRequestBehavior.AllowGet);
            WebCosmeticEntities db = new WebCosmeticEntities(); 
            return Json(db.Categories.Select(s => new { s.CatId, s.CatName, s.Description }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create( Category cat)
        {
            if (ModelState.IsValid)
            {
                categoryDAO.Create(cat);
                return RedirectToAction("Index");
            }
            return View(cat);
        }
        public ActionResult Edit(long id)
        {
            return View(categoryDAO.getById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                Category tg = new Category();
                categoryDAO.Edit(cat);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sửa không thành công!");
            }
            return View(cat);
        }
        public ActionResult Delete(long id)
        {
            if(ModelState.IsValid)
            {
                categoryDAO.Delete(id);
                return RedirectToAction("Index");
            }       
            return View();
        }
        
    }
}