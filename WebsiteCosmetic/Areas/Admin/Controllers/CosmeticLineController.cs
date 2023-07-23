using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class CosmeticLineController : Controller
    {
        CosmeticLineDAO cosmeticLineDAO = new CosmeticLineDAO();
        WebCosmeticEntities db = new WebCosmeticEntities();
        // GET: Admin/CosmeticLine
        public ActionResult Index()
        {
            return View(cosmeticLineDAO.getAllCosmetic());
        }
        public ActionResult Create()
        {
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CosmeticLine line)
        {
            if (ModelState.IsValid)
            {
                cosmeticLineDAO.Create(line);
                return RedirectToAction("Index");
            }
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName", line.CatId);
            return View(line);
        }
        public ActionResult Edit(long id)
        {
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return View(cosmeticLineDAO.getById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CosmeticLine line)
        {
            if (ModelState.IsValid)
            {
                cosmeticLineDAO.Edit(line);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sửa không thành công!");
            }
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName", line.CatId);
            return View(line);
        }
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                cosmeticLineDAO.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}