using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories.Advance
{
    public class AbsenceManageRepository : Repository
    {
        public List<Dictionary<string, object>> GetAll()
        {
            return GetAll(false);
        }

        public List<Dictionary<string, object>> GetAllAuditable()
        {
            return GetAll(true);
        }

        public List<Dictionary<string, object>> GetAll(bool changeable)
        {
            List<Dictionary<string, object>> Ret = null;
            using (var context = GetDbContext())
            {
                DbSet<AbsenceReason> absenceReasons = context.Set<AbsenceReason>();

                if (absenceReasons.Count() <= 0)
                {
                    return Ret;
                }

                Dictionary<string, object> aReasonInfo;
                Ret = new List<Dictionary<string, object>>(absenceReasons.Count());

                var filterResult = absenceReasons.Where(a => a.Changeable == changeable).ToList();
                foreach (var absenceReason in filterResult)
                {
                    aReasonInfo = new Dictionary<string, object>();
                    aReasonInfo.Add("Id", absenceReason.Id);
                    aReasonInfo.Add("Reason", absenceReason.Reason);
                    aReasonInfo.Add("StudentName", absenceReason.Student.Name);

                    Ret.Add(aReasonInfo);
                }
            }

            return Ret;
        }

        public bool AuditAbsence(int id)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<AbsenceReason> absenceReasons = context.Set<AbsenceReason>();
                var absenceReason = absenceReasons.Where(a => a.Id == id).FirstOrDefault();

                if (absenceReason != null)
                {
                    absenceReason.Changeable = false;

                    context.SaveChanges();

                    bRet = true;
                }
            }

            return bRet;
        }

        public bool Destroy(int id)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<AbsenceReason> absenceReasons = context.Set<AbsenceReason>();
                var absenceReason = absenceReasons.Where(a => a.Id == id).FirstOrDefault();

                if (absenceReason != null)
                {
                    absenceReasons.Remove(absenceReason);

                    context.SaveChanges();
                }

                bRet = true;
            }

            return bRet;
        }
    }
}
