﻿using System.Collections.Generic;
using System.Web.Mvc;

using NUnit.Framework;

using WebAppTDD.Controllers;
using WebAppTDD.Models;
using WebAppTDD.Tests.Mocks;

namespace WebAppTDD.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void IndexNotNullTest()
        {
            HomeController homeController = new HomeController(new ClassesMockRepository());

            ViewResult result = homeController.Index() as ViewResult;

            Assert.NotNull(result);
        }

        [Test]
        public void IndexReturnsIndexCshtml()
        {
            HomeController homeController = new HomeController(new ClassesMockRepository());

            ViewResult result = homeController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void IndexReturnsScheduleModel()
        {
            HomeController homeController = new HomeController(new ClassesMockRepository());

            ViewResult result = homeController.Index() as ViewResult;

            Assert.IsInstanceOf<IEnumerable<Class>>(result.Model);
        }

        private ViewResult GetResultForDetails()
        {
            ClassesMockRepository classes = new ClassesMockRepository();
            classes.Add(new Class());
            int insertedId = classes.LastInsertedIndex;
            HomeController homeController = new HomeController(classes);

            ViewResult result = homeController.Details(insertedId) as ViewResult;
            return result;
        }

        [Test]
        public void DetailsNotNullTest()
        {
            Assert.NotNull(GetResultForDetails());
        }

        [Test]
        public void DetailsReturnsDetailsCshtml()
        {
            Assert.AreEqual("Details", GetResultForDetails().ViewName);
        }

        [Test]
        public void DetailsReturnsScheduleModel()
        {
            Assert.IsInstanceOf<Class>(GetResultForDetails().Model);
        }
    }
}
