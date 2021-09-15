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
    public class JobCandidateRepositoryTest
    {
        [TestMethod]
        public async Task GetJobCandidatesAsync_ShouldReturnListOfJobCandidateObejcts()
        {
            // Arange
            List<JobCandidate> jobCandidates = GetDemoJobCandidatesList();

            var mockJobCandidateRepository = new Mock<IJobCandidateRepository>();
            mockJobCandidateRepository.Setup(x => x.GetJobCandidatesAsync()).ReturnsAsync(jobCandidates.AsEnumerable());

            // Act
            var items = await mockJobCandidateRepository.Object.GetJobCandidatesAsync();

            //Assert
            Assert.IsNotNull(items);
            Assert.AreEqual(jobCandidates.Count, items.ToList().Count);
            Assert.AreEqual(jobCandidates[0], items.ElementAt(0));
            Assert.IsInstanceOfType(items, typeof(IEnumerable<JobCandidate>));
        }

        [TestMethod]
        public async Task GetJobCandidateByIdAsync_ShouldReturnJobCandidateObjectWithSameId()
        {
            // Arrange
            var mockJobCandidateRepository = new Mock<IJobCandidateRepository>();
            int id = 1;
            var itemDemo = GetDemoJobCandidate();
            mockJobCandidateRepository.Setup(x => x.GetJobCandidateByIdAsync(id)).ReturnsAsync(itemDemo);

            // Act
            var item = await mockJobCandidateRepository.Object.GetJobCandidateByIdAsync(id);

            // Assert
            Assert.IsNotNull(item);
            Assert.AreEqual(id, item.JobCandidateId);
            Assert.IsInstanceOfType(item, typeof(JobCandidate));
        }

        [TestMethod]
        public async Task AddJobCandidateAsync_ShouldReturnJobCandidateThatIsAdded()
        {
            var mockJobCandidateRepository = new Mock<IJobCandidateRepository>();
            var item = GetDemoJobCandidate();

            mockJobCandidateRepository.Setup(x => x.AddJobCandidateAsync(item)).ReturnsAsync(item);

            var itemForAdd = await mockJobCandidateRepository.Object.AddJobCandidateAsync(item);

            Assert.IsNotNull(itemForAdd);
            Assert.IsInstanceOfType(itemForAdd, typeof(JobCandidate));
        }

        [TestMethod]
        public async Task UpdateJobCandidateAsync_ShouldReturnNewUpdatedJobCandidateObject()
        {
            var mockJobCandidateRepository = new Mock<IJobCandidateRepository>();
            var item = GetDemoJobCandidate();

            mockJobCandidateRepository.Setup(x => x.AddJobCandidateAsync(item)).ReturnsAsync(item);

            var itemForUpdate = new JobCandidate { JobCandidateId = 1, FullName="Road Runner", ContactNumber="123-567", DateOfBirth = new DateTime(1946, 05, 07), Email="runner@email.com"};
            mockJobCandidateRepository.Setup(x => x.UpdateJobCandidateAsync(itemForUpdate)).ReturnsAsync(itemForUpdate);

            var result = await mockJobCandidateRepository.Object.UpdateJobCandidateAsync(itemForUpdate);

            Assert.AreEqual(result.JobCandidateId, item.JobCandidateId);
            Assert.AreEqual(result.FullName, "Road Runner");
            Assert.IsInstanceOfType(result, typeof(JobCandidate));
        }

        [TestMethod]
        public async Task UpdateJobCandidateAsync_ShouldFailBecauseItemForUpdateIdIsWrong()
        {
            var mockJobCandidateRepository = new Mock<IJobCandidateRepository>();
            var item = GetDemoJobCandidate();

            mockJobCandidateRepository.Setup(x => x.AddJobCandidateAsync(item)).ReturnsAsync(item);

            var itemForUpdate = new JobCandidate { JobCandidateId = 22, FullName = "Road Runner", ContactNumber = "123-567", DateOfBirth = new DateTime(1946, 05, 07), Email = "runner@email.com" };
            mockJobCandidateRepository.Setup(x => x.UpdateJobCandidateAsync(itemForUpdate)).ReturnsAsync(itemForUpdate);

            var result = await mockJobCandidateRepository.Object.UpdateJobCandidateAsync(itemForUpdate);

            Assert.AreNotEqual(result.JobCandidateId, item.JobCandidateId);
        }

        [TestMethod]
        public async Task SearchAsync_ReturnJobCandidateObjectsWhereCharactersMachSearchParameters()
        {
            List<JobCandidate> jobCandidates = GetDemoJobCandidatesList();
            var mockJobCandidateRepository = new Mock<IJobCandidateRepository>();

            string search = "ric";
            mockJobCandidateRepository.Setup(x => x.SearchAsync(search)).ReturnsAsync(jobCandidates.AsEnumerable().Where(x => x.FullName.Contains(search)));

            var result = await mockJobCandidateRepository.Object.SearchAsync(search);

            Assert.IsInstanceOfType(result, typeof(IEnumerable<JobCandidate>));
        }

        private JobCandidate GetDemoJobCandidate()
        {
            return new JobCandidate()
            {
                JobCandidateId = 1,
                FullName = "Rick Hunter",
                DateOfBirth = new DateTime(1990, 11, 22),
                ContactNumber = "123-987-654",
                Email = "skull1@email.com"
            };
        }

        private List<JobCandidate> GetDemoJobCandidatesList()
        {
            List<JobCandidate> jobCandidates = new List<JobCandidate>()
            {
                new JobCandidate
                {
                    JobCandidateId = 1,
                    FullName = "Rick Hunter",
                    DateOfBirth = new DateTime(1990, 11, 22),
                    ContactNumber = "123-987-654",
                    Email = "skull1@email.com"
                },
                new JobCandidate
                {
                    JobCandidateId = 2,
                    FullName = "Leo",
                    DateOfBirth = new DateTime(1990, 11, 22),
                    ContactNumber = "123-987-654",
                    Email = "leo@email.com"
                }
            };

            return jobCandidates;
        }
    }
}
