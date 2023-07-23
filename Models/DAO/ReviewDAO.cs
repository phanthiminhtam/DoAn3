using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ReviewDAO
    {
        WebCosmeticEntities db = null;
        public ReviewDAO()
        {
            db = new WebCosmeticEntities();
        }
        public bool Insert(Review review)
        {
            var check = db.Orders.FirstOrDefault(s => s.PhoneNumber == review.PhoneNumber);
            if(check != null)
            {
                review.CusId = check.CusId;
                db.Reviews.Add(review);
                db.SaveChanges();
                return true;
            }    
            else
            {
                return false;
            }
        }
        public List<Review> getAllReview()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<Review> reviews = db.Reviews.ToList();
            return reviews;
        }
        public bool ChangeStatus(long id)
        {
                Review r = db.Reviews.Find(id);
                r.Status = !r.Status;
                db.SaveChanges();
                return r.Status.Value;
        }
    }
}
