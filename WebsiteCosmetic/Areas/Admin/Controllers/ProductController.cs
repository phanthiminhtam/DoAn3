using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        WebCosmeticEntities db = new WebCosmeticEntities();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getAllProduct());
        }
        public ActionResult Create()
        {
            ViewBag.LineId = new SelectList(db.CosmeticLines, "LineId", "LineName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productDAO.Create(product);
                return RedirectToAction("Index");
            }
            ViewBag.LineId = new SelectList(db.CosmeticLines, "LineId", "LineName", product.LineId);
            return View(product);
        }
        public ActionResult Edit(long id)
        {
            ViewBag.LineId = new SelectList(db.CosmeticLines, "LineId", "LineName");
            return View(productDAO.getById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productDAO.Edit(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sửa không thành công!");
            }
            ViewBag.LineId = new SelectList(db.CosmeticLines, "LineId", "LineName", product.LineId);
            return View(product);
        }
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                productDAO.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}