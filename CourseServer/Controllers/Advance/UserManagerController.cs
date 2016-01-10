using CourseServer.Framework;
using CourseServer.Repositories;
using CourseServer.Utils;
using CourseServer.Views;
using System;

namespace CourseServer.Controllers.Advance
{
    public class UserManagerController : Controller
    {
        private GenericView view;
        private UserRepository userRepo;

        public UserManagerController()
        {
            view = new GenericView();
            userRepo = new UserRepository();
        }

        public string AllUser(int mode)
        {
            Validator validator = new Validator();
            if (! validator.MatchRule(mode + "", "required", "mode"))
            {
                return view.Error(validator.GetDetail());
            }

            var result = userRepo.GetAllUserByMode(mode);

            return view.Show(result);
        }

        public string Store(string email, string user, string pass, int mode)
        {
            RegisterView view = new RegisterView();
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { email, user, pass, mode + "" },
                new string[] { "email", "required", "required", "required" },
                new string[] { "email", "username", "password", "mode" }))
            {
                return view.Error(validator.GetDetail());
            }
            
            int result = userRepo.Register(email, user, pass, mode);

            return view.Show(result);
        }

        public string Update(int id, int mode, 
            string name, string avatar, string cellphone)
        {
            Validator validator = new Validator();
            // Validate the user input here.
            if (!validator.Make(new string[] { id + "", mode + "", name},
                new string[] { "required", "required", "required" },
                new string[] { "userId", "mode", "username" }))
            {
                return view.Error(validator.GetDetail());
            }
            
            bool bRet = false;
            if (userRepo.Exists(id, mode))
            {
                userRepo.Update(userRepo.CurrentUser, name, avatar, cellphone, null);
                bRet = true;
            }

            return bRet ? view.Success() : view.Error();
        }

        public string Destroy(int id, int mode)
        {
            Validator validator = new Validator();
            // Validate the user input here.
            if (!validator.Make(new string[] { id + "", mode + "" },
                new string[] { "required", "required"},
                new string[] { "userId", "mode" }))
            {
                return view.Error(validator.GetDetail());
            }

            userRepo.Destroy(id, mode);

            return view.Success();
        }

        public string ResetPassword(int id, int mode)
        {
            Validator validator = new Validator();
            if (!validator.Make(new string[] { id + "", mode + "" },
                new string[] { "required", "required" },
                new string[] { "userId", "mode" }))
            {
                return view.Error(validator.GetDetail());
            }

            string pwd = Guard.GenerateRandomPassword();
            bool bRet = userRepo.ResetPassword(id, pwd, mode);

            return bRet ? view.Show(pwd) : view.Error();
        }

        public string Profile(int id, int mode)
        {
            UserView view = new UserView();
            Validator validator = new Validator();
            // Validate the user input here.
            if (!validator.Make(new string[] { id + "", mode + "" },
                new string[] { "required", "required" },
                new string[] { "userId", "mode" }))
            {
                return view.Error(validator.GetDetail());
            }

            return userRepo.Exists(id, mode) ?
                view.Show(userRepo.CurrentUser) :
                view.Error();
        }
    }
}
