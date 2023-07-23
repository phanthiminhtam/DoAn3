using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class Home2Controller : BaseController
    {
        SpecificProductDAO db = new SpecificProductDAO();
        CustomerDAO cs = new CustomerDAO();
        ProductDAO ProductDAO = new ProductDAO();
        OrderDetailDAO detailDAO = new OrderDetailDAO();
        StaffDAO staffDAO = new StaffDAO();
        ProviderDAO providerDAO = new ProviderDAO();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Logout()
        {
            return View();
        }
        public PartialViewResult SPTop()
        {
            var ds = db.getAllSProduct().ToList();
            return PartialView(ds);
        }
        public PartialViewResult SLSP()
        {
            var ds = ProductDAO.getAllProduct().ToList();
            return PartialView(ds);
        }
        public PartialViewResult SPKH()
        {
            var ds = cs.getAllCustomer().ToList();
            return PartialView(ds);
        }
        public PartialViewResult SLDH()
        {
            var ds = detailDAO.getAllOrderDetail().ToList();
            return PartialView(ds);
        }
        public PartialViewResult SLNV()
        {
            var ds = staffDAO.getAllStaff().ToList();
            return PartialView(ds);
        }
        public PartialViewResult SLNCC()
        {
            var ds = providerDAO.getAllProvider().ToList();
            return PartialView(ds);
        }
        public PartialViewResult SLSale()
        {
            SpecificProductDAO sp = new SpecificProductDAO();
            var ds = sp.getAllSProduct().ToList();
            int tg = 0;
            foreach(var i in ds)
            {
                if(i.Offer != null)
                {
                    tg++;
                }
            }
            ViewBag.slsale = tg;
            return PartialView();
        }
    }
}