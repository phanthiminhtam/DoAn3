using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class SpecificProductDAO
    {
        WebCosmeticEntities db = null;
        public SpecificProductDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<SpecificProduct> getAllSProduct()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<SpecificProduct> list = db.SpecificProducts.ToList();
            return list;
        }
        public SpecificProduct getById(long id)
        {
            SpecificProduct specificProduct = new SpecificProduct();
            try
            {
                specificProduct = db.SpecificProducts.FirstOrDefault(x => x.SpId == id);
                return specificProduct;
            }
            catch
            {
                return specificProduct;
            }
        }
        public bool Create(SpecificProduct specificProduct)
        {
                if(specificProduct.StartedDate.Month <= specificProduct.StopDate.Month && specificProduct.StartedDate.Year <= specificProduct.StopDate.Year)
                { 
                    if(specificProduct.StartedDate.Day < specificProduct.StopDate.Day)
                    {
                        db.SpecificProducts.Add(specificProduct);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }     
                else
                {
                    return false;
                }
        }
        public bool Delete(long id)
        {
            try
            {
                var specificProduct = db.SpecificProducts.Find(id);
                db.SpecificProducts.Remove(specificProduct);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(SpecificProduct specificProduct)
        {
            SpecificProduct specific = db.SpecificProducts.Find(specificProduct.SpId);
            if (specific != null)
            {
                specific.SpId = specificProduct.SpId;
                specific.ProId = specificProduct.ProId;
                //specific.Quantity= specificProduct.Quantity;
                specific.Price = specificProduct.Price;
                specific.Measure = specificProduct.Measure;
                specific.Type = specificProduct.Type;
                specific.StartedDate = specificProduct.StartedDate;
                specific.StopDate = specificProduct.StopDate;
                specific.Offer = specificProduct.Offer;
                specific.Image = specificProduct.Image;
            }
            db.SaveChanges();
        }
        public List<SpecificProduct> getRelatedProduct(long id)
        {
            long idCategory = db.SpecificProducts.Find(id).Product.CosmeticLine.CatId;
            List<SpecificProduct> list = db.SpecificProducts.Where(p => p.Product.CosmeticLine.CatId == idCategory).Take(8).OrderByDescending(p => p.ProId).ToList();
            return list;
        }   
    }
}
