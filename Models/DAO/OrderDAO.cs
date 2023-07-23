using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderDAO
    {
        WebCosmeticEntities db = null;
        public OrderDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<Order> getAllOrder()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<Order> list = db.Orders.ToList();
            return list;
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrdId;
        }
        
    }
}
