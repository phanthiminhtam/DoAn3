using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class NewsDAO
    {
        WebCosmeticEntities db = null;
        public NewsDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<News> getAllNew()
        {
            List<News> list = db.News.ToList();
            return list;
        }

        public List<News> getlast()
        {
            List<News> list = db.News.OrderByDescending(N=>N.NewId).Take(4).ToList();
            return list;
        }
        public News getById(long id)
        {
            News news = new News();
            try
            {
                news = db.News.FirstOrDefault(x => x.NewId == id);
                return news;
            }
            catch
            {
                return news;
            }
        }
        public bool Create(News n)
        {
            try
            {
                db.News.Add(n);
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
                var news = db.News.Find(id);
                db.News.Remove(news);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(News news)
        {
            News n = db.News.Find(news.NewId);
            if (n != null)
            {
                n.NewId = news.NewId;
                n.UserId = news.UserId;
                n.Content = news.Content;
                n.Title = news.Title;
                n.PublicDate = news.PublicDate.Value;
                n.Image = news.Image.Trim();
            }
            db.SaveChanges();
        }
    }
}
