using CourseServer.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CourseServer
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(string connectionString, int timeout) : base(connectionString)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
        }

        public virtual DbSet<PasswordReset> PasswordResets { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Student> Studentes { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Dispatch> Dispatches { get; set; }
        // public virtual DbSet<Join> Joins { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<AbsenceType> AbsenceTypes { get; set; }
        public virtual DbSet<AbsenceReason> AbsenceReasons { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Broadcast> Broadcasts { get; set; }
        public virtual DbSet<CourseAppliy> CourseApplies { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        // public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<SystemLog> SystemLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            /*modelBuilder.Entity<Course>()
                        .HasRequired(c => c.Teacher)
                        .WithMany(t => t.Courses)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                        .HasRequired(c => c.Major)
                        .WithMany(m => m.Courses)
                        .WillCascadeOnDelete(false);*/

            base.OnModelCreating(modelBuilder);
        }
    }
}
