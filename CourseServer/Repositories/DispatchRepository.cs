using CourseProvider;
using CourseServer.Entities;
using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public class DispatchRepository : Repository
    {

        public List<Dispatch> GetDispatchList(UserEntity entity)
        {
            using (var context = GetDbContext())
            {
                DbSet<Dispatch> dispatches = context.Set<Dispatch>();

                ChainLoad(dispatches, "Course", "Teacher", "Classroom");

                switch (entity.Mode)
                {
                    case CourseProviderContract.MODE_STUDENT:
                        ChainLoad(dispatches, "Students");
                        return dispatches.Where(d => d.Enable && d.Students.Contains((Student) entity)).ToList();
                    case CourseProviderContract.MODE_TEACHER:
                        return dispatches.Where(d => d.Enable && d.TeacherId == entity.Id).ToList();
                }
            }

            return null;
        }
    }
}
