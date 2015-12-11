using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public class MajorRepository : Repository
    {
        public List<Dictionary<string, object>> GetAll()
        {
            List<Dictionary<string, object>> Ret = null;
            using (var context = GetDbContext())
            {
                DbSet<Major> majors = context.Set<Major>();

                if (majors.Count() <= 0)
                {
                    return Ret;
                }

                Dictionary<string, object> majorInfo;
                Ret = new List<Dictionary<string, object>>(majors.Count());
                foreach (var major in majors)
                {
                    majorInfo = new Dictionary<string, object>();
                    majorInfo.Add("Id", major.Id);
                    majorInfo.Add("Description", major.Description);

                    Ret.Add(majorInfo);
                }
            }

            return Ret;
        }

        public bool Create(string name, string description)
        {
            bool bRet = false;
            using (var context = GetDbContext())
            {
                DbSet<Major> majors = context.Set<Major>();
                Major major = new Major() { Name = name, Description = description };

                majors.Add(major);

                context.SaveChanges();

                bRet = true;
            }
            return bRet;
        }

        public bool Destroy(int id)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<Major> majors = context.Set<Major>();
                Major major = majors.Where(m => m.Id == id).FirstOrDefault();

                if (major != null)
                {
                    majors.Remove(major);

                    context.SaveChanges();
                }

                bRet = true;
            }

            return bRet;
        }

        public bool Update(int id, string name, string description)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<Major> majors = context.Set<Major>();
                Major major = majors.Where(m => m.Id == id).FirstOrDefault();

                if (major != null)
                {
                    major.Name = name;
                    major.Description = description;

                    context.SaveChanges();

                    bRet = true;
                }
            }

            return bRet;
        }
    }
}
