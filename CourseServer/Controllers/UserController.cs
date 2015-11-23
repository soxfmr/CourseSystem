using CourseServer.Entities;
using CourseServer.Framework;
using CourseServer.Repositories;
using CourseServer.Utils;
using CourseServer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Controllers
{
    public class UserController : Controller
    {
        private UserView userView;

        public UserController()
        {
            userView = new UserView();
        }

        public string Profile()
        {
            return userView.Show(Auth.User());
        }

        public string Update(string name, string avatar, string cellphone, 
            string newPwd, string pwdConfirm, string originPwd)
        {
            Validator validator = new Validator();
            // Validate the user input here.
            if (!validator.Make(new string[] { name, originPwd, newPwd, pwdConfirm },
                new string[] { "required", "required", "match:newPassword_confirmation", "" },
                new string[] { "username", "password", "newPassword", "newPassword_confirmation" }))
            {
                return userView.Error(validator.GetDetail());
            }
            
            UserRepository userRepo = new UserRepository();

            UserEntity user = Auth.User();
            if (! userRepo.Attempt(user.Email, originPwd, user.Mode))
            {
                return userView.Error();
            }

            userRepo.Update(user, name, avatar, cellphone, newPwd);

            return userView.Success();
        }
    }
}
