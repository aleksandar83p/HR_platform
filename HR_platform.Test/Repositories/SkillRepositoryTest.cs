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
    public class SkillRepositoryTest
    {
        [TestMethod]
        public async Task GetSkillsAsync_ShouldReturnListOfSkillObejcts()
        {
            // Arange
            List<Skill> skills = GetDemoSkillsList();

            var mockSkillRepository = new Mock<ISkillRepository>();
            mockSkillRepository.Setup(x => x.GetSkillsAsync()).ReturnsAsync(skills.AsEnumerable());

            // Act
            var items = await mockSkillRepository.Object.GetSkillsAsync();

            //Assert
            Assert.IsNotNull(items);
            Assert.AreEqual(skills.Count, items.ToList().Count);
            Assert.AreEqual(skills[0], items.ElementAt(0));
            Assert.IsInstanceOfType(items, typeof(IEnumerable<Skill>));
        }

        [TestMethod]
        public async Task GetSkillByIdAsync_ShouldReturnSkillObjectWithSameId()
        {
            // Arrange
            var mockSkillRepository = new Mock<ISkillRepository>();
            int id = 1;
            var itemDemo = GetDemoSkill();
            mockSkillRepository.Setup(x => x.GetSkillByIdAsync(id)).ReturnsAsync(itemDemo);

            // Act
            var item = await mockSkillRepository.Object.GetSkillByIdAsync(id);

            // Assert
            Assert.IsNotNull(item);
            Assert.AreEqual(id, item.SkillId);
            Assert.IsInstanceOfType(item, typeof(Skill));
        }

        [TestMethod]
        public async Task AddSkillAsync_ShouldReturnSkillObjectThatIsAdded()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var item = GetDemoSkill();
            
            mockRepository.Setup(x => x.AddSkillAsync(item)).ReturnsAsync(item);

            var itemForAdd = await mockRepository.Object.AddSkillAsync(item);

            Assert.IsNotNull(itemForAdd);
            Assert.IsInstanceOfType(itemForAdd, typeof(Skill));
        }

        [TestMethod]
        public async Task UpdateSkillAsync_ShouldReturnNewUpdatedSkillObject()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var item = GetDemoSkill();

            mockRepository.Setup(x => x.AddSkillAsync(item)).ReturnsAsync(item);  

            var itemForUpdate = new Skill { SkillId = 1, Name = "Art" };
            mockRepository.Setup(x => x.UpdateSkillAsync(itemForUpdate)).ReturnsAsync(itemForUpdate);

            var result = await mockRepository.Object.UpdateSkillAsync(itemForUpdate);

            Assert.AreEqual(result.SkillId, item.SkillId);
            Assert.AreEqual(result.Name, "Art");
            Assert.IsInstanceOfType(result, typeof(Skill));
        }

        [TestMethod]
        public async Task UpdateSkillAsync_ShouldFailBecauseItemForUpdateIdIsWrong()
        {
            var mockRepository = new Mock<ISkillRepository>();
            var item = GetDemoSkill();

            mockRepository.Setup(x => x.AddSkillAsync(item)).ReturnsAsync(item);

            var itemForUpdate = new Skill { SkillId = 2, Name = "Art" };
            mockRepository.Setup(x => x.UpdateSkillAsync(itemForUpdate)).ReturnsAsync(itemForUpdate);

            var result = await mockRepository.Object.UpdateSkillAsync(itemForUpdate);

            Assert.AreNotEqual(result.SkillId, item.SkillId);               
        }

        [TestMethod]
        public async Task SearchAsync_ReturnSkillObjectsWhereCharactersMachSearchParameters()
        {
            List<Skill> skills = GetDemoSkillsList();
            var mockSkillRepository = new Mock<ISkillRepository>();

            string search = "jav";
            mockSkillRepository.Setup(x => x.SearchAsync(search)).ReturnsAsync(skills.AsEnumerable().Where(x => x.Name.Contains(search)));
            
            var result = await mockSkillRepository.Object.SearchAsync(search);

            Assert.IsInstanceOfType(result, typeof(IEnumerable<Skill>));
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
                     Name = "C# programming"
                 },
                new Skill()
                 {
                     SkillId = 2,
                     Name = "Java programming"
                 }
            };

            return skills;
        }
    }
}
