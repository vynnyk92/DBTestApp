using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALModule.Model;

namespace DALModule.Repositories
{
    public class UoW:IDisposable
    {
        private BlogEntities _db;
        private CategoriesRepository _catRep;
        private PostRepository _postRep;
        public UoW(BlogEntities db)
        {
            this._db = db;
        }
        public IRepository<Categories> Categories
        {
            get
            {
                if (_catRep == null)
                    _catRep= new CategoriesRepository(_db);
                return _catRep;
            }
        }
        public IRepository<Posts> Posts
        {
            get
            {
                if (_postRep == null)
                    _postRep = new PostRepository(_db);
                return _postRep;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
