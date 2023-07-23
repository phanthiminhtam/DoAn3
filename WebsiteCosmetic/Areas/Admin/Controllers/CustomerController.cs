using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        CustomerDAO customerDAO = new CustomerDAO();
        public ActionResult Index()
        {
            return View(customerDAO.getAllCustomer());
        }
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                customerDAO.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}