using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Repositories;
using CourseServer.Framework;

namespace CourseServer.Views.Tests
{
    [TestClass()]
    public class DispatchViewTests
    {
        [TestMethod()]
        public void ShowAvaliableCourseTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchRepository dp = new DispatchRepository();

            DispatchView view = new DispatchView();
            view.ShowAvailableCourse(dp.GetAvailableCourse());
        }
    }
}