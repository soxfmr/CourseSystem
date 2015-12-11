using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public class ClassroomRepository : Repository
    {
        public List<Dictionary<string, object>> GetAll()
        {
            List<Dictionary<string, object>> Ret = null;
            using (var context = GetDbContext())
            {
                DbSet<Classroom> classrooms = context.Set<Classroom>();

                if (classrooms.Count() <= 0)
                {
                    return Ret;
                }

                Dictionary<string, object> roomInfo;
                Ret = new List<Dictionary<string, object>>(classrooms.Count());
                foreach (var room in classrooms)
                {
                    roomInfo = new Dictionary<string, object>();
                    roomInfo.Add("Id", room.Id);
                    roomInfo.Add("Location", room.Location);
                    roomInfo.Add("Number", room.Number);

                    Ret.Add(roomInfo);
                }
            }

            return Ret;
        }

        public bool Create(string location, int number)
        {
            bool bRet = false;
            using (var context = GetDbContext())
            {
                DbSet<Classroom> classrooms = context.Set<Classroom>();
                Classroom room = new Classroom() { Location = location, Number = number };

                classrooms.Add(room);

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
                DbSet<Classroom> classrooms = context.Set<Classroom>();
                Classroom room = classrooms.Where(c => c.Id == id).FirstOrDefault();

                if (room != null)
                {
                    classrooms.Remove(room);

                    context.SaveChanges();
                }

                bRet = true;
            }

            return bRet;
        }

        public bool Update(int id, string location, int number)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<Classroom> classrooms = context.Set<Classroom>();
                Classroom room = classrooms.Where(c => c.Id == id).FirstOrDefault();

                if (room != null)
                {
                    room.Location = location;
                    room.Number = number;

                    context.SaveChanges();

                    bRet = true;
                }
            }

            return bRet;
        }
    }
}
