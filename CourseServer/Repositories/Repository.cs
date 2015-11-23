using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public abstract class Repository
    {
        protected DbContext DbContext;

        protected DbContext GetDbContext()
        {
            /*if (DbContext == null)
                DbContext = DbContextHelper.NewInstance();*/

            return DbContextHelper.NewInstance();
        }

        /// <summary>
        /// Loading the external model with is relationship with this model
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="ModelName"></param>
        protected void ChainLoad(DbSet dbSet, params string[] modelNames)
        {
            if (modelNames == null)
                return;

            foreach(string model in modelNames)
            {
                dbSet.Include(model).Load();
            }
        }

        protected List<M> All<M>(DbContext context, params string[] modelNames) where M : Model.Model
        {
            DbSet<M> dbSet = context.Set<M>();
            ChainLoad(dbSet, modelNames);

            return dbSet != null ? dbSet.ToList() : null;
        }
    }
}
