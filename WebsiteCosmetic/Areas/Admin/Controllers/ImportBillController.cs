using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class ImportBillController : Controller
    {
        // GET: Admin/ImportBill
        ImportBillDAO importBillDAO  = new ImportBillDAO();
        WebCosmeticEntities db = new WebCosmeticEntities();
        public ActionResult Index()
        {
            return View(importBillDAO.getAllImportBill());
        }
        public ActionResult Create()
        {
            ViewBag.PrvId = new SelectList(db.Providers, "PrvId", "PrvName");
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "StaffName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImportBill bill)
        {
            if (ModelState.IsValid)
            {
                importBillDAO.Create(bill);
                return RedirectToAction("Index");
            }
            ViewBag.PrvId = new SelectList(db.Providers, "PrvId", "PrvName",bill.PrvId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "StaffName", bill.StaffId);
            return View(bill);
        }
        public ActionResult Edit(long id)
        {

            return View(importBillDAO.getById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ImportBill bill)
        {
            if (ModelState.IsValid)
            {
                importBillDAO.Edit(bill);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sửa không thành công!");
            }
            return View(bill);
        }
      
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                importBillDAO.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}