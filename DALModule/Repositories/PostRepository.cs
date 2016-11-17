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
    public class PostRepository:IRepository<Posts>
    {
        private BlogEntities _db;
        public PostRepository(BlogEntities db)
        {
            this._db = db;
        }
        public IEnumerable<Posts> GetAll()
        {
            return _db.Posts;
        }

        public Posts Get(int id)
        {
            return _db.Posts.Find(id);
        }

        public void Create(Posts post)
        {
            _db.Posts.Add(post);
        }

        public void Update(Posts post)
        {
            _db.Entry(post).State = EntityState.Modified;
        }

        public IEnumerable<Posts> Find(Func<Posts, Boolean> predicate)
        {
            return _db.Posts.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Posts post = _db.Posts.Find(id);
            if (post != null)
                _db.Posts.Remove(post);
        }
    }
}