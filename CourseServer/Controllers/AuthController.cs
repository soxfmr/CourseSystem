using CourseProvider;
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
    public class AuthController : Controller
    {
        public string OnLogin(string email, string pass, int mode)
        {
            LoginView view = new LoginView();
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.MatchRule(email, "email", "email") )
            {
                return view.Error(validator.GetDetail());
            }

            UserRepository userRepo = new UserRepository();
            if (userRepo.Login(email, pass, mode))
            {
                return view.Show(userRepo.SessionId);
            }

            return view.Error();
        }

        public string OnRegister(string email, string user, string pass)
        {
            RegisterView view = new RegisterView();
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { email, user, pass }, 
                new string[] { "email", "minLength:1", "minLength:8" },
                new string[] { "email", "username", "password" }))
            {
                return view.Error(validator.GetDetail());
            }

            UserRepository userRepo = new UserRepository();
            int result = userRepo.Register(email, user, pass, 0);

            return view.Show(result);
        }

        public string OnLogout()
        {
            Session.Remove(Auth.SessionId);
            return new LoginView().Success();
        }
    }
}
