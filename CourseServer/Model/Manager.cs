using CourseProvider;
using CourseServer.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseServer.Model
{
    [Table("managers")]
    public class Manager : UserEntity
    {
        public Manager()
        {
            Mode = CourseProviderContract.MODE_MANAGER;
        }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
