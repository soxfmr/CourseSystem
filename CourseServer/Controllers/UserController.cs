using CourseServer.Entities;
using CourseServer.Framework;
using CourseServer.Model;
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

        public string ShowDispatch()
        {
            DispatchView view = new DispatchView();
            DispatchRepository dispatchRepo = new DispatchRepository();

            var user = Auth.User();
            List<Dispatch> dispatchList = dispatchRepo.GetDispatchList(user.Id, user.Mode);
            return view.Show(dispatchList);
        }

        public string CreateDispatch(int id)
        {
            GenericView view = new GenericView();
            DispatchRepository dispatchRepo = new DispatchRepository();

            Validator validator = new Validator();
            if (!validator.MatchRule(id + "", "required", "id"))
            {
                return view.Error(validator.GetDetail());
            }

            bool Ret = dispatchRepo.JoinCourse(Auth.User().Id, id);

            return Ret ? view.Success() : view.Error();
        }

        public string RemoveDispatch(string id)
        {
            GenericView view = new GenericView();
            DispatchRepository dispatchRepo = new DispatchRepository();

            Validator validator = new Validator();
            if (!validator.MatchRule(id, "required", "id"))
            {
                return view.Error(validator.GetDetail());
            }

            string[] idArr = id.Split(',');
            bool Ret = dispatchRepo.RemoveCourseList(Auth.User().Id, Array.ConvertAll(idArr, int.Parse));

            return Ret ? view.Success() : view.Error();
        }
    }
}
