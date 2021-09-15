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
    public class JobCandidatesControllerTest
    {
        [TestMethod] 
        public async Task GetJobCandidatesAsync_Return200OK()
        {
            List<JobCandidate> jobCandidates = GetDemoJobCandidatesList();            

            var mockRepository = new Mock<IJobCandidateRepository>();
            mockRepository.Setup(x => x.GetJobCandidatesAsync()).ReturnsAsync(jobCandidates.AsEnumerable());

            var controller = new JobCandidatesController(mockRepository.Object);

            ActionResult actionResult = await controller.GetJobCandidatesAsync();            

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));            
        }

        [TestMethod]
        public async Task GetJobCandidateByIdAsync_Return200OK()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();
            var item = GetDemoJobCandidate();

            mockRepository.Setup(x => x.GetJobCandidateByIdAsync(1)).ReturnsAsync(item);

            var controller = new JobCandidatesController(mockRepository.Object);

            var actionResult = await controller.GetJobCandidateAsync(1);            

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetJobCandidateByIdAsync_Return404NotFound()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();           
            var controller = new JobCandidatesController(mockRepository.Object);

            var actionResult = await controller.GetJobCandidateAsync(4);

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));
        }
        
        [TestMethod]
        public async Task CreateJobCandidateAsync_Return201Created()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();         
            var controller = new JobCandidatesController(mockRepository.Object);
            var item = GetDemoJobCandidate();
            mockRepository.Setup(x => x.AddJobCandidateAsync(item)).ReturnsAsync(item);

            var actionResult = await controller.CreateJobCandidate(item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(ObjectResult));            
        }

        [TestMethod]
        public async Task UpdateJobCandidateAsync_Return200OK()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();
            var controller = new JobCandidatesController(mockRepository.Object);
            var item = GetDemoJobCandidate();

            var actionResult = await controller.UpdateJobCandidateAsync(1, item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task UpdateJobCandidateAsync_Return400BadRequest()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();
            var controller = new JobCandidatesController(mockRepository.Object);
            var item = GetDemoJobCandidate();

            var actionResult = await controller.UpdateJobCandidateAsync(10, item);

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteJobCandidateAsync_ReturnStatus200OK()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();
            var item = GetDemoJobCandidate();
            mockRepository.Setup(x => x.GetJobCandidateByIdAsync(1)).ReturnsAsync(item); 
            var controller = new JobCandidatesController(mockRepository.Object);

            var actionResult = await controller.DeleteJobCandidateAsync(1);

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task DeleteJobCandidateAsync_ReturnStatus404NotFound()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();
            var item = GetDemoJobCandidate();
            mockRepository.Setup(x => x.GetJobCandidateByIdAsync(1)).ReturnsAsync(item);
            var controller = new JobCandidatesController(mockRepository.Object);

            var actionResult = await controller.DeleteJobCandidateAsync(11);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task SearchAsync_ReturnStatus200OK()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();
            List<JobCandidate> jobCandidates = GetDemoJobCandidatesList();

            string search = "Leo";
            mockRepository.Setup(x => x.SearchAsync(search)).ReturnsAsync(jobCandidates.AsEnumerable().Where(x =>x.FullName.Contains(search)));

            var controller = new JobCandidatesController(mockRepository.Object);

            var actionResult = await controller.SearchAsync(search);

            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));           
        }

        [TestMethod]
        public async Task SearchAsync_ReturnStatus404NotFound()
        {
            var mockRepository = new Mock<IJobCandidateRepository>();
            List<JobCandidate> jobCandidates = GetDemoJobCandidatesList();

            string search = "pppp";
            mockRepository.Setup(x => x.SearchAsync(search)).ReturnsAsync(jobCandidates.AsEnumerable().Where(x => x.FullName.Contains(search)));

            var controller = new JobCandidatesController(mockRepository.Object);

            var actionResult = await controller.SearchAsync(search);

            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
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
