using System.Collections.Generic;
using System.Web;
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
        public void IndexReturnsClassModel()
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
        public void DetailsReturnsClassModel()
        {
            Assert.IsInstanceOf<Class>(GetResultForDetails().Model);
        }

        [Test]
        public void DetailsThrowsHttpExceptionIfIdIsNotFound()
        {
            HomeController homeController = new HomeController(new ClassesMockRepository());

            Assert.Throws<HttpException>(()=> homeController.Details(int.MinValue));
        }

        [Test]
        public void CreateNotNullTest()
        {
            HomeController homeController = new HomeController(new ClassesMockRepository());

            ViewResult result = homeController.Create() as ViewResult;

            Assert.NotNull(result);
        }

        [Test]
        public void CreateReturnsCreateCshtml()
        {
            HomeController homeController = new HomeController(new ClassesMockRepository());

            ViewResult result = homeController.Create() as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public void SuccessfulCreateRedirectToIndex()
        {
            HomeController homeController = new HomeController(new ClassesMockRepository());
            Class cl = new Class()
            {
                Id = 11,
                Date = System.DateTime.Now,
                Location = "404a",
                Discipline = "ТП",
                Teacher = "Отец"
            };

            RedirectToRouteResult result = homeController.Create(cl) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void UnsuccessfulCreateReturnsCreateCshtml()
        {
            HomeController homeController = new HomeController(new ClassesMockRepository());

            ViewResult result = homeController.Create(null) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }
    }
}
