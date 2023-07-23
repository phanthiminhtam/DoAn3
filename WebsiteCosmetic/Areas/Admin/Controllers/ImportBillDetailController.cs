using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class ImportBillDetailController : Controller
    {
        // GET: Admin/ImportBillDetail
        WebCosmeticEntities db = new WebCosmeticEntities();
        ImportBillDetailDAO importBillDetailDAO = new ImportBillDetailDAO();
        public ActionResult Index()
        {
            return View(importBillDetailDAO.getAllIBDetail());
        }
        public ActionResult Create()
        {
            //ViewBag.SpId = new SelectList(db.SpecificProducts, "SpId");
            ViewBag.SpId = new SelectList(db.Products, "ProId", "ProName");
            ViewBag.ImpId = new SelectList(db.Staffs,"StaffId", "StaffName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImportBillDetail d)
        {
            if (ModelState.IsValid)
            {
                importBillDetailDAO.Create(d);
                return RedirectToAction("Index");
            }
            ViewBag.SpId = new SelectList(db.Products, "ProId", "ProName",d.SpId);
            ViewBag.ImpId = new SelectList(db.Staffs, "StaffId", "StaffName",d.ImpId);
            return View(d);
        }
        public ActionResult Edit(long id)
        {
            ViewBag.SpId = new SelectList(db.Products, "ProId", "ProName");
            return View(importBillDetailDAO.getById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ImportBillDetail billD)
        {
            if (ModelState.IsValid)
            {
                importBillDetailDAO.Edit(billD);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sửa không thành công!");
            }
            ViewBag.SpId = new SelectList(db.Products, "ProId", "ProName", billD.SpId);
            return View(billD);
        }

        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                importBillDetailDAO.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}