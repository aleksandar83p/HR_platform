using HR_platform.Models;
using HR_platform.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_platform.Test.Repositories
{
    [TestClass]
    public class JobCandidateSkillsRepositoryTest
    {
        [TestMethod]
        public async Task GetJJobCandidateSkillsAsync_ShouldReturnListOfJobCandidateSkillsObejcts()
        {
            // Arange
            List<JobCandidateSkill> jobCandidateSkills = GetDemoJobCandidateSkillsList();

            var mockJobCandidateSkillRepository = new Mock<IJobCandidateSkillsRepository>();
            mockJobCandidateSkillRepository.Setup(x => x.GetJobCandidateSkillsAsync()).ReturnsAsync(jobCandidateSkills.AsEnumerable());

            // Act
            var items = await mockJobCandidateSkillRepository.Object.GetJobCandidateSkillsAsync();

            //Assert
            Assert.IsNotNull(items);
            Assert.AreEqual(jobCandidateSkills.Count, items.ToList().Count);
            Assert.AreEqual(jobCandidateSkills[0], items.ElementAt(0));
            Assert.IsInstanceOfType(items, typeof(IEnumerable<JobCandidateSkill>));
        }

        [TestMethod]
        public async Task GetJobCandidateSkillByIdAsync_ShouldReturnJobCandidateSkillObjectWithSameId()
        {
            // Arrange
            var mockJobCandidateSkillRepository = new Mock<IJobCandidateSkillsRepository>();
            int id = 1;
            var itemDemo = GetDemoJobCandidateSkill();
            mockJobCandidateSkillRepository.Setup(x => x.GetJobCandidateSkillByIdAsync(id)).ReturnsAsync(itemDemo);

            // Act
            var item = await mockJobCandidateSkillRepository.Object.GetJobCandidateSkillByIdAsync(id);

            // Assert
            Assert.IsNotNull(item);
            Assert.AreEqual(id, item.Id);
            Assert.IsInstanceOfType(item, typeof(JobCandidateSkill));
        }

        [TestMethod]
        public async Task AddJobCandidateSkillAsync_ShouldReturnJobCandidateSkillThatIsAdded()
        {
            var mockJobCandidateSkillRepository = new Mock<IJobCandidateSkillsRepository>();
            var item = GetDemoJobCandidateSkill();

            mockJobCandidateSkillRepository.Setup(x => x.AddJobCandidateSkillAsync(item)).ReturnsAsync(item);

            var itemForAdd = await mockJobCandidateSkillRepository.Object.AddJobCandidateSkillAsync(item);

            Assert.IsNotNull(itemForAdd);
            Assert.IsInstanceOfType(itemForAdd, typeof(JobCandidateSkill));
        }

        [TestMethod]
        public async Task UpdateJobCandidateSkillAsync_ShouldReturnNewUpdatedJobCandidateSkillObject()
        {
            var mockJobCandidateSkillRepository = new Mock<IJobCandidateSkillsRepository>();
            var item = GetDemoJobCandidateSkill();

            mockJobCandidateSkillRepository.Setup(x => x.AddJobCandidateSkillAsync(item)).ReturnsAsync(item);

            var itemForUpdate = new JobCandidateSkill { Id = 1, JobCandidateId = 1, SkillId = 2 };
            mockJobCandidateSkillRepository.Setup(x => x.UpdateJobCandidateSkillAsync(itemForUpdate)).ReturnsAsync(itemForUpdate);

            var result = await mockJobCandidateSkillRepository.Object.UpdateJobCandidateSkillAsync(itemForUpdate);

            Assert.AreEqual(result.JobCandidateId, item.Id);
            Assert.AreEqual(result.SkillId, 2);
            Assert.IsInstanceOfType(result, typeof(JobCandidateSkill));
        }

        [TestMethod]
        public async Task UpdateJobCandidateSkillAsync_ShouldFailBecauseItemForUpdateIdIsWrong()
        {
            var mockJobCandidateSkillRepository = new Mock<IJobCandidateSkillsRepository>();
            var item = GetDemoJobCandidateSkill();

            mockJobCandidateSkillRepository.Setup(x => x.AddJobCandidateSkillAsync(item)).ReturnsAsync(item);

            var itemForUpdate = new JobCandidateSkill { Id = 22, JobCandidateId = 1, SkillId = 2 };
            mockJobCandidateSkillRepository.Setup(x => x.UpdateJobCandidateSkillAsync(itemForUpdate)).ReturnsAsync(itemForUpdate);

            var result = await mockJobCandidateSkillRepository.Object.UpdateJobCandidateSkillAsync(itemForUpdate);

            Assert.AreNotEqual(result.Id, item.Id);
        }

        [TestMethod]
        public async Task SearchAsync_ReturnJobCandidateSkillsObjectsWhereCharactersMachSearchParameters()
        {
            List<JobCandidateSkill> jobCandidates = GetDemoJobCandidateSkillsList();
            var mockJobCandidateSkillRepository = new Mock<IJobCandidateSkillsRepository>();

            string name = "1";
            string skillName = "1";
            mockJobCandidateSkillRepository.Setup(x => x.SearchAsync(name, skillName)).ReturnsAsync(jobCandidates.AsEnumerable().
                Where(x => x.JobCandidateId.ToString().Contains(name) || x.SkillId.ToString().Contains(skillName)));

            var result = await mockJobCandidateSkillRepository.Object.SearchAsync(name, skillName);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<JobCandidateSkill>));
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
