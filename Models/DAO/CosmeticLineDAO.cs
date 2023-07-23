using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CosmeticLineDAO
    {
       
        WebCosmeticEntities db = null;
        public CosmeticLineDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<CosmeticLine> getAllCosmetic()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            
            List<CosmeticLine> list = db.CosmeticLines.ToList();
            return list;
        }
        public CosmeticLine getById(long id)
        {
            CosmeticLine cosmeticLine = new CosmeticLine();
            try
            {
                cosmeticLine = db.CosmeticLines.FirstOrDefault(x => x.LineId == id);
                return cosmeticLine;
            }
            catch
            {
                return cosmeticLine;
            }
        }
        public bool Create(CosmeticLine cosmeticLine)
        {
            try
            {
                db.CosmeticLines.Add(cosmeticLine);
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
                var line = db.CosmeticLines.Find(id);
                db.CosmeticLines.Remove(line);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(CosmeticLine line)
        {
            CosmeticLine l = db.CosmeticLines.Find(line.LineId);
            if (l != null)
            {
                l.LineId = line.LineId;
                l.LineName = line.LineName;
                l.CatId = line.CatId;
                l.Brand = line.Brand;
                l.Origin = line.Origin;  
            }
            db.SaveChanges();
        }
    }
}
