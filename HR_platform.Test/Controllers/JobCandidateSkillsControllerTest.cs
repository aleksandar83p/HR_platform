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
    public class JobCandidateSkillsControllerTest
    {
        [TestMethod]
        public async Task GetSkillsAsync_Return200OK()
        {
            List<JobCandidateSkill> jobCandidateSkills = GetDemoJobCandidateSkillsList();

            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            mockRepository.Setup(x => x.GetJobCandidateSkillsAsync()).ReturnsAsync(jobCandidateSkills.AsEnumerable());

            var controller = new JobCandidateSkillsController(mockRepository.Object);

            ActionResult actionResult = await controller.GetJobCandidatesSkillsAsync();

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetSkillByIdAsync_Return200OK()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            var item = GetDemoJobCandidateSkill();

            mockRepository.Setup(x => x.GetJobCandidateSkillByIdAsync(1)).ReturnsAsync(item);

            var controller = new JobCandidateSkillsController(mockRepository.Object);

            var actionResult = await controller.GetJobCandidateSkillByIdAsync(1);

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetSkillByIdAsync_Return404NotFound()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            var controller = new JobCandidateSkillsController(mockRepository.Object);

            var actionResult = await controller.GetJobCandidateSkillByIdAsync(4);

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateSkillAsync_Return201Created()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            var controller = new JobCandidateSkillsController(mockRepository.Object);
            var item = GetDemoJobCandidateSkill();
            mockRepository.Setup(x => x.AddJobCandidateSkillAsync(item)).ReturnsAsync(item);

            var actionResult = await controller.CreateJobCandidateSkill(item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task UpdateSkillAsync_Return200OK()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            var controller = new JobCandidateSkillsController(mockRepository.Object);
            var item = GetDemoJobCandidateSkill();

            var actionResult = await controller.UpdateJobCandidateSkillAsync(1, item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task UpdatSkillAsync_Return400BadRequest()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            var controller = new JobCandidateSkillsController(mockRepository.Object);
            var item = GetDemoJobCandidateSkill();

            var actionResult = await controller.UpdateJobCandidateSkillAsync(10, item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteSkillAsync_ReturnStatus200OK()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            var item = GetDemoJobCandidateSkill();
            mockRepository.Setup(x => x.GetJobCandidateSkillByIdAsync(1)).ReturnsAsync(item);
            var controller = new JobCandidateSkillsController(mockRepository.Object);

            var actionResult = await controller.DeleteJobCandidateSkillAsync(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task DeleteSkillAsync_ReturnStatus404NotFound()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            var item = GetDemoJobCandidateSkill();
            mockRepository.Setup(x => x.GetJobCandidateSkillByIdAsync(1)).ReturnsAsync(item);
            var controller = new JobCandidateSkillsController(mockRepository.Object);

            var actionResult = await controller.DeleteJobCandidateSkillAsync(11);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task SearchAsync_ReturnStatus200OK()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            List<JobCandidateSkill> skills = GetDemoJobCandidateSkillsList();

            string name = "1";
            string skillName = "1";
            mockRepository.Setup(x => x.SearchAsync(name, skillName)).ReturnsAsync(skills.AsEnumerable().
                Where(x => x.JobCandidateId.ToString().Contains(name) || x.SkillId.ToString().Contains(skillName)));

            var controller = new JobCandidateSkillsController(mockRepository.Object);

            var actionResult = await controller.SearchAsync(name, skillName);

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SearchAsync_ReturnStatus404NotFound()
        {
            var mockRepository = new Mock<IJobCandidateSkillsRepository>();
            List<JobCandidateSkill> skills = GetDemoJobCandidateSkillsList();

            string name = "5";
            string skillName = "5";
            mockRepository.Setup(x => x.SearchAsync(name, skillName)).ReturnsAsync(skills.AsEnumerable().
                Where(x => x.JobCandidateId.ToString().Contains(name) || x.SkillId.ToString().Contains(skillName)));
            

            var controller = new JobCandidateSkillsController(mockRepository.Object);

            var actionResult = await controller.SearchAsync(name, skillName);

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        private JobCandidateSkill GetDemoJobCandidateSkill()
        {
            return new JobCandidateSkill
            {
                Id = 1,
                JobCandidateId = 1,
                SkillId = 1
            };
        }

        private List<JobCandidateSkill> GetDemoJobCandidateSkillsList()
        {
            List<JobCandidateSkill> skills = new List<JobCandidateSkill>()
            {
                 new JobCandidateSkill
                 {
                    Id = 1,
                    JobCandidateId = 1,
                    SkillId = 1
                 },
                 new JobCandidateSkill
                 {
                    Id = 2,
                    JobCandidateId = 2,
                    SkillId = 2
                 }
            };
            return skills;
        }
    }
}
