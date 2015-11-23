using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;
using CourseServer.Views;
using Newtonsoft.Json.Linq;

namespace CourseServer.Repositories.Tests
{
    [TestClass()]
    public class DispatchRepositoryTests
    {
        [TestMethod()]
        public void GetDispatchListTest()
        {
            /*DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);
            DispatchRepository dispatchRepo = new DispatchRepository();

            var list = dispatchRepo.GetDispatchList(new Entities.UserEntity { Id = 1, Mode = 1 });
            DispatchView view = new DispatchView();
            string s = view.Show(list);

            JObject obj = JObject.Parse(s);*/
        }
    }
}