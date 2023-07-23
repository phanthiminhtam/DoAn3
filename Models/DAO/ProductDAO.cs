using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProductDAO
    {
        WebCosmeticEntities db = null;
        public ProductDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<Product> getAllProduct()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<Product> list = db.Products.ToList();
            return list;
        }
        public Product getById(long id)
        {
            Product product = new Product();
            try
            {
                product = db.Products.FirstOrDefault(x => x.ProId == id);
                return product;
            }
            catch
            {
                return product;
            }
        }

       
        public bool Create(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(Product product)
        {
            Product products = db.Products.Find(product.ProId);
            if (products != null)
            {
                products.ProId = product.ProId;
                products.ProName = product.ProName;
                products.LineId = product.LineId;
                products.Description = product.Description;
            }
            db.SaveChanges();
        }
        public List<Product> getByPro(long? proId)
        {
            return db.Products.Where(x => x.ProId == proId).Include(pi => pi.ProName).OrderByDescending(p => p.ProId).Take(8).ToList();
        }
        public List<SpecificProduct> ListSearch(string keyword)
        {
            return db.SpecificProducts.Where(s => s.Product.ProName.Contains(keyword)).ToList().Select(x => new SpecificProduct()
            {
                Product = new Product() { ProName = x.Product.ProName},
                SpId = x.SpId
            }).ToList();
        }
    }
}
