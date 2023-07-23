using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderDetailDAO
    {
        WebCosmeticEntities db = null;
        public OrderDetailDAO()
        {
            db = new WebCosmeticEntities();
        }
        public bool Insert(OderDetail orderDetail)
        {
            try
            {
                db.OderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<OderDetail> getAllOrderDetail()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<OderDetail> list = db.OderDetails.ToList();
            return list;
        }
    }
}
