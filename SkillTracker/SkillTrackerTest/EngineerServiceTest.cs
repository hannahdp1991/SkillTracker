using EngineerService.Controllers;
using EngineerService.Models;
using EngineerService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SkillTrackerTest.Builders;

namespace SkillTrackerTest
{
    class EngineerServiceTest
    {
        Mock<ISkillService> service;
        Mock<ILogger<EngineerController>> logger;

        [SetUp]
        public void Setup()
        {
            service = new Mock<ISkillService>();
            logger = new Mock<ILogger<EngineerController>>();
        }

        [Test]
        public void CreateSkillProfileTest()
        {
            service.Setup(x => x.Add(It.IsAny<SkillProfile>())).ReturnsAsync(true);
            var controller = new EngineerController(logger.Object, service.Object);
            var request = new SkillProfileRequestBuilder();
            request.WithDefaults();

            var result = controller.Post(request.Build()) as ObjectResult;


            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void UpdateSkillProfileTest()
        {
            service.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<UpdateSkillProfile>())).Returns(true);
            var controller = new EngineerController(logger.Object, service.Object);
            var request = new UpdateSkillProfile();

            var result = controller.Put("CTS346747", request) as ObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void CreateInvalidSkillProfileTest()
        {
            service.Setup(x => x.Add(It.IsAny<SkillProfile>())).Throws(new System.Exception());
            var controller = new EngineerController(logger.Object, service.Object);
            var request = new SkillProfileRequestBuilder();
            request.WithDefaults();

            var result = controller.Post(request.Build()) as ObjectResult;

            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
