using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALModule.Model;
using System.Data.Entity;
using System.Data;

namespace DALModule.Repositories
{
    public class CategoriesRepository:IRepository<Categories>
    {
        private BlogEntities _db;
        public CategoriesRepository(BlogEntities db)
        {
            this._db = db;
        }
        public IEnumerable<Categories> GetAll()
        {
            return _db.Categories;
        }

        public Categories Get(int id)
        {
            return _db.Categories.Find(id);
        }

        public void Create(Categories category)
        {
            _db.Categories.Add(category);
        }

        public void Update(Categories category)
        {
            _db.Entry(category).State = EntityState.Modified;
        }

        public IEnumerable<Categories> Find(Func<Categories, Boolean> predicate)
        {
            return _db.Categories.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Categories category = _db.Categories.Find(id);
            if (category != null)
                _db.Categories.Remove(category);
        }
    }
}