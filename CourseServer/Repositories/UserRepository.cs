using CourseProvider;
using CourseServer.Entities;
using CourseServer.Framework;
using CourseServer.Helper;
using CourseServer.Model;
using CourseServer.Utils;
using System.Data.Entity;
using System.Linq;

namespace CourseServer.Repositories
{
    public class UserRepository : Repository
    {

        public string SessionId { get; private set; }

        public UserEntity CurrentUser { get; private set; }

        /// <summary>
        /// Try to match the user with password in database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool Attempt(string email, string pass, int mode)
        {
            bool bRet = false;

            switch (mode)
            {
                case CourseProviderContract.MODE_STUDENT:
                    bRet = hasUser<Student>(email, pass);
                    break;
                case CourseProviderContract.MODE_TEACHER:
                    bRet = hasUser<Teacher>(email, pass);
                    break;
                case CourseProviderContract.MODE_MANAGER:
                    bRet = hasUser<Manager>(email, pass);
                    break;
                default: break;
            }

            return bRet;
        }
        
        /// <summary>
        /// Try to match the user with password in database, and then create a new session
        /// for the user if matched.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool Login(string email, string pass, int mode)
        {
            bool bRet = Attempt(email, pass, mode);

            if (bRet)
            {                
                SessionId = Session.Add(new SessionInfo { UserEntity = CurrentUser });
            }

            return bRet;
        }

        public int Register(string email, string user, string pass, int mode)
        {
            if (Exists(email, mode))
            {
                return CourseProviderContract.REG_ALREADY_EXISTS;
            }

            bool bRet = false;
            switch (mode)
            {
                case CourseProviderContract.MODE_STUDENT:
                    bRet = newUser(new Student { Email = email, Name = user, Password = Guard.Encrypt(pass) });
                    break;                
                case CourseProviderContract.MODE_TEACHER:
                    bRet = newUser(new Teacher { Email = email, Name = user, Password = Guard.Encrypt(pass) });
                    break;
                case CourseProviderContract.MODE_MANAGER:
                    bRet = newUser(new Manager { Email = email, Name = user, Password = Guard.Encrypt(pass) });
                    break;
                default: break;
            }

            return bRet ? CourseProviderContract.RESULT_SUCCESS : CourseProviderContract.RESULT_FAILED;
        }

        public bool Exists(string email, int mode)
        {
            bool bRet = false;

            switch (mode)
            {
                case CourseProviderContract.MODE_STUDENT:
                    bRet = Exists<Student>(email);
                    break;
                case CourseProviderContract.MODE_TEACHER:
                    bRet = Exists<Teacher>(email);
                    break;
                case CourseProviderContract.MODE_MANAGER:
                    bRet = Exists<Manager>(email);
                    break;
                default: break;
            }

            return bRet;
        }

        public bool Exists(int userId, int mode)
        {
            bool bRet = false;

            switch (mode)
            {
                case CourseProviderContract.MODE_STUDENT:
                    bRet = Exists<Student>(userId);
                    break;
                case CourseProviderContract.MODE_TEACHER:
                    bRet = Exists<Teacher>(userId);
                    break;
                case CourseProviderContract.MODE_MANAGER:
                    bRet = Exists<Manager>(userId);
                    break;
                default: break;
            }

            return bRet;
        }

        /// <summary>
        /// Update the user properties with the giving value and save changes
        /// to the database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="name"></param>
        /// <param name="avatar"></param>
        /// <param name="cellphone"></param>
        /// <param name="newPwd"></param>
        public void Update(UserEntity user, string name,
            string avatar, string cellphone, string newPwd)
        {
            user.Name = name;
            user.Avatar = avatar;
            user.Cellphone = cellphone;
            if (!TextUtils.isEmpty(newPwd))
            {
                user.Password = Guard.Encrypt(newPwd);
            }

            Update(user);
        }

        /// <summary>
        /// Update a user with the new properties which is sepcified in the entity
        /// </summary>
        /// <param name="user"></param>
        public void Update(UserEntity user)
        {
            switch (user.Mode)
            {
                case CourseProviderContract.MODE_STUDENT:
                    Update<Student>(user);
                    break;
                case CourseProviderContract.MODE_TEACHER:
                    Update<Teacher>(user);
                    break;
                case CourseProviderContract.MODE_MANAGER:
                    Update<Manager>(user);
                    break;
                default: break;
            }
        }

        public void Destroy(int userId, int mode)
        {
            switch (mode)
            {
                case CourseProviderContract.MODE_STUDENT:
                    Destroy<Student>(userId);
                    break;
                case CourseProviderContract.MODE_TEACHER:
                    Destroy<Teacher>(userId);
                    break;
                case CourseProviderContract.MODE_MANAGER:
                    Destroy<Manager>(userId);
                    break;
                default: break;
            }
        }

        private void Update<TEntity>(UserEntity user) where TEntity : UserEntity
        {
            UserEntity origin = null;
            using (var context = GetDbContext())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                origin = dbSet.Where(e => e.Email == user.Email).FirstOrDefault();

                if (origin != null)
                {
                    UpdateProfile(origin, user);
                    context.SaveChanges();
                }
            }
        }

        private void UpdateProfile(UserEntity origin, UserEntity newProfile)
        {
            origin.Name = newProfile.Name;
            origin.Password = newProfile.Password;
            origin.Cellphone = newProfile.Cellphone;
            origin.Avatar = newProfile.Avatar;
        }

        private bool Exists<TEntity>(int id) where TEntity : UserEntity
        {
            using (var context = GetDbContext())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                CurrentUser = dbSet.Where(e => e.Id == id).FirstOrDefault();
            }

            return CurrentUser != null;
        }

        private bool Exists<TEntity>(string email) where TEntity : UserEntity
        {
            using (var context = GetDbContext())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                CurrentUser = dbSet.Where(e => e.Email == email).FirstOrDefault();
            }

            return CurrentUser != null;
        }

        private void Destroy<TEntity>(int userId) where TEntity : UserEntity
        {
            using (var context = GetDbContext())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                var user = dbSet.Where(e => e.Id == userId).FirstOrDefault();

                if (user != null)
                {
                    dbSet.Remove(user);
                }

                context.SaveChanges();
            }
        }

        private bool hasUser<TEntity>(string email, string pass) where TEntity : UserEntity
        {
            using (var context = GetDbContext())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                CurrentUser = dbSet.Where(e =>  e.Email == email).FirstOrDefault();
            }

            if (CurrentUser == null)
                return false;

            return Guard.IsMatched(pass, CurrentUser.Password);
        }

        private bool newUser<TEntity>(TEntity userEntity) where TEntity : UserEntity
        {
            int match = 0;

            using (var context = GetDbContext())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                CurrentUser = dbSet.Add(userEntity);

                match = context.SaveChanges();
            }

            return match > 0;
        }
    }
}
