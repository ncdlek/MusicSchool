using MS.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MS.BLL.Repository.Entity
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected MusicSchoolContext db;
        protected DbSet<TEntity> table;

        public BaseRepository()
        {
            db = new MusicSchoolContext();
            table = db.Set<TEntity>();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int ExecuteSqlCommand(string query)
        {
            return db.Database.ExecuteSqlCommand(query);
        }

        // INSERT
        public int Insert(TEntity entity)
        {
            table.Add(entity);
            return Save();
        }

        public TEntity InsertandReturnId(TEntity entity)
        {
            table.Add(entity);
            db.SaveChanges();
            return entity;
        }

        // SELECT
        public TEntity SelectOne(Expression<Func<TEntity, bool>> predicate)
        {
            return table.FirstOrDefault(predicate);
        }

        public List<TEntity> SelectByCondition(Expression<Func<TEntity, bool>> predicate)
        {
            return table.Where(predicate).ToList();
        }

        public List<TEntity> SelectAll()
        {
            return table.ToList();
        }

        // DELETE
        public int Delete(int? Id)
        {
            TEntity toBeDeleted = table.Find(Id);

            if (toBeDeleted == null)
            {
                return 0;
            }

            table.Remove(toBeDeleted);
            return Save();
        }
    }
}
