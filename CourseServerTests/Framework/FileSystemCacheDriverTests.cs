using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework.Tests
{
    [TestClass()]
    public class FileSystemCacheDriverTests
    {
        [TestMethod()]
        public void FileSystemCacheDriverTest()
        {

        }

        [TestMethod()]
        public void CacheTest()
        {
            FileSystemCacheDriver driver = new FileSystemCacheDriver();
            // driver.Cache(driver.GetCacheKey(this, "CacheTest"), "Some data here to be cache.");

            // Assert.IsTrue(driver.isCached(driver.GetCacheKey(this, "CacheTest")));
        }

        [TestMethod()]
        public void LoadCacheTest()
        {
            FileSystemCacheDriver driver = new FileSystemCacheDriver();
            // driver.Cache(driver.GetCacheKey(this, "CacheTest"), "Some data here to be cache.");

            // Assert.IsTrue(driver.isCached(driver.GetCacheKey(this, "CacheTest")));

            // Assert.AreEqual("Some data here to be cache.", driver.LoadCache(driver.GetCacheKey(this, "CacheTest")));
        }

        [TestMethod()]
        public void isCachedTest()
        {

        }

        [TestMethod()]
        public void GetCacheKeyTest()
        {

        }
    }
}