using HR_platform.Controllers;
using HR_platform.Models;
using HR_platform.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_platform.Test.Controllers
{
    [TestClass]
    public class SkillsControllerTest
    {
        [TestMethod]
        public async Task GetSkillsAsync_Return200OK()
        {
            List<Skill> skills = GetDemoSkillsList();

            var mockRepository = new Mock<ISkillRepository>();
            mockRepository.Setup(x => x.GetSkillsAsync()).ReturnsAsync(skills.AsEnumerable());

            var controller = new SkillsController(mockRepository.Object);

            ActionResult actionResult = await controller.GetSkillsAsync();

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetSkillByIdAsync_Return200OK()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var item = GetDemoSkill();

            mockRepository.Setup(x => x.GetSkillByIdAsync(1)).ReturnsAsync(item);

            var controller = new SkillsController(mockRepository.Object);

            var actionResult = await controller.GetSkillByIdAsync(1);

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetSkillByIdAsync_Return404NotFound()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var controller = new SkillsController(mockRepository.Object);

            var actionResult = await controller.GetSkillByIdAsync(4);

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateSkillAsync_Return201Created()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var controller = new SkillsController(mockRepository.Object);
            var item = GetDemoSkill();
            mockRepository.Setup(x => x.AddSkillAsync(item)).ReturnsAsync(item);

            var actionResult = await controller.CreateSkill(item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task UpdateSkillAsync_Return200OK()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var controller = new SkillsController(mockRepository.Object);
            var item = GetDemoSkill();

            var actionResult = await controller.UpdateSkillAsync(1, item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task UpdatSkillAsync_Return400BadRequest()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var controller = new SkillsController(mockRepository.Object);
            var item = GetDemoSkill();

            var actionResult = await controller.UpdateSkillAsync(10, item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteSkillAsync_ReturnStatus200OK()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var item = GetDemoSkill();
            mockRepository.Setup(x => x.GetSkillByIdAsync(1)).ReturnsAsync(item);
            var controller = new SkillsController(mockRepository.Object);

            var actionResult = await controller.DeleteSkillAsync(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task DeleteSkillAsync_ReturnStatus404NotFound()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var item = GetDemoSkill();
            mockRepository.Setup(x => x.GetSkillByIdAsync(1)).ReturnsAsync(item);
            var controller = new SkillsController(mockRepository.Object);

            var actionResult = await controller.DeleteSkillAsync(11);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task SearchAsync_ReturnStatus200OK()
        {
            var mockRepository = new Mock<ISkillRepository>();
            List<Skill> skills = GetDemoSkillsList();

            string search = "Java";
            mockRepository.Setup(x => x.SearchAsync(search)).ReturnsAsync(skills.AsEnumerable().Where(x => x.Name.Contains(search)));

            var controller = new SkillsController(mockRepository.Object);

            var actionResult = await controller.SearchAsync(search);

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SearchAsync_ReturnStatus404NotFound()
        {
            var mockRepository = new Mock<ISkillRepository>();
            List<Skill> skills = GetDemoSkillsList();

            string search = "pppp";
            mockRepository.Setup(x => x.SearchAsync(search)).ReturnsAsync(skills.AsEnumerable().Where(x => x.Name.Contains(search)));

            var controller = new SkillsController(mockRepository.Object);

            var actionResult = await controller.SearchAsync(search);

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        private Skill GetDemoSkill()
        {
            return new Skill()
            {
                SkillId = 1,
                Name = "C#"
            };
        }

        private List<Skill> GetDemoSkillsList()
        {
            List<Skill> skills = new List<Skill>()
            {
                 new Skill()
                 {
                     SkillId = 1,
                     Name = "C#"
                 },
                new Skill()
                 {
                     SkillId = 2,
                     Name = "Java"
                 }
            };

            return skills;
        }
    }
}
