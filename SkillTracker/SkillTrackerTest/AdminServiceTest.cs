using AdminService.Controllers;
using AdminService.Helpers;
using AdminService.Models;
using AdminService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SkillTrackerTest.Builders;

namespace SkillTrackerTest
{
    public class AdminServiceTest
    {
        Mock<ISkillProfileService> service;
        Mock<ILogger<AdminController>> logger;
        ICriteriaBuilder criteriaBuilder;

        [SetUp]
        public void Setup()
        {
            service = new Mock<ISkillProfileService>();
            logger = new Mock<ILogger<AdminController>>();
            criteriaBuilder = new CriteriaBuilder();

            var testDataBuilder = new AdminSkillProfileBuilder();
            testDataBuilder.WithDefaults();
            service.Setup(x => x.SearchProfile(It.IsAny<Criteria>())).ReturnsAsync(testDataBuilder.Build());
        }

        [Test]
        public void SearchByAssociateIdTest()
        {
            var controller = new AdminController(service.Object, logger.Object, criteriaBuilder);

            var result = controller.Get("associateId", "346757") as ObjectResult;


            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void SearchByInvalidCriteriaTest()
        {
            var controller = new AdminController(service.Object, logger.Object, criteriaBuilder);

            var result = controller.Get("test", "346757") as ObjectResult;

            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Criteria Not Found", result.Value);
        }
    }
}