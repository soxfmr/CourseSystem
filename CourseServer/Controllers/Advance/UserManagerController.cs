using CourseServer.Framework;
using CourseServer.Repositories;
using CourseServer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string name, string avatar, string cellphone, 
            string newPwd, string pwdConfirm)
        {
            Validator validator = new Validator();
            // Validate the user input here.
            if (!validator.Make(new string[] { id + "", mode + "", name, newPwd, pwdConfirm },
                new string[] { "required", "required", "required", "match:newPassword_confirmation", "" },
                new string[] { "userId", "mode", "username", "newPassword", "newPassword_confirmation" }))
            {
                return view.Error(validator.GetDetail());
            }
            
            bool bRet = false;
            if (userRepo.Exists(id, mode))
            {
                userRepo.Update(userRepo.CurrentUser, name, avatar, cellphone, newPwd);
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
