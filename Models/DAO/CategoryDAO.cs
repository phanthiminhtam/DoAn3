using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryDAO
    {
        WebCosmeticEntities db = null;
        public CategoryDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<Category> getAllCategory()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Category> list = db.Categories.ToList();
            return list;
        }
        public Category getById(long id)
        {
            Category category = new Category();
            try
            {
                category = db.Categories.FirstOrDefault(x => x.CatId == id);
                return category;
            }
            catch
            {
                return category;
            }
        }
        public bool Create(Category cat)
        {
            try
            {
                db.Categories.Add(cat);
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
                var loai = db.Categories.Find(id);
                db.Categories.Remove(loai);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(Category cat)
        {
            Category loai = db.Categories.Find(cat.CatId);
            if (loai != null)
            {
                loai.CatId = cat.CatId;
                loai.CatName = cat.CatName;
                loai.Description = cat.Description;
            }
            db.SaveChanges();
        }
    }
}
