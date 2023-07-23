using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class SpecificProductController : Controller
    {
        // GET: Admin/SpecificProduct
        SpecificProductDAO SpecificProductDAO = new SpecificProductDAO();
        WebCosmeticEntities db = new WebCosmeticEntities();
        public ActionResult Index()
        {
            return View(SpecificProductDAO.getAllSProduct());
        }
        public ActionResult Create()
        {
            ViewBag.ProId = new SelectList(db.Products, "ProId", "ProName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(SpecificProduct specificProduct, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            string _FileName = " ";
            string _FileName1 = " ";
            string _FileName2 = " ";
            
            try
            {
                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/FileUL"), _FileName);
                    file.SaveAs(_path);
                }
                if (file1.ContentLength > 0)
                {
                    _FileName1 = Path.GetFileName(file1.FileName);
                    string _path1 = Path.Combine(Server.MapPath("~/FileUL"), _FileName1);
                    file1.SaveAs(_path1);
                }
                if (file2.ContentLength > 0)
                {
                    _FileName2 = Path.GetFileName(file2.FileName);
                    string _path2 = Path.Combine(Server.MapPath("~/FileUL"), _FileName2);
                    file2.SaveAs(_path2);
                }
            }
            catch
            {

            }
            if (ModelState.IsValid)
            {
                specificProduct.Image = "/FileUL/" + _FileName;
                specificProduct.Image1 = "/FileUL/" + _FileName1;
                specificProduct.Image2 = "/FileUL/" + _FileName2;
                var tt = SpecificProductDAO.Create(specificProduct);
                if (tt == true)
                {
                    return RedirectToAction("Index");
                }    
                else
                {
                    ModelState.AddModelError("", "Thời gian không hợp lệ!");
                }
            }
            ViewBag.ProId = new SelectList(db.Products, "ProId", "ProName", specificProduct.ProId);
            return View(specificProduct);
        }
        public ActionResult Edit(long id)
        {
            ViewBag.ProId = new SelectList(db.Products, "ProId", "ProName");
            return View(SpecificProductDAO.getById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(SpecificProduct specific)
        {
            if (ModelState.IsValid)
            {
                SpecificProductDAO.Edit(specific);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sửa không thành công!");
            }
            ViewBag.ProId = new SelectList(db.Products, "ProId", "ProName", specific.ProId);
            return View(specific);
        }
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                SpecificProductDAO.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}