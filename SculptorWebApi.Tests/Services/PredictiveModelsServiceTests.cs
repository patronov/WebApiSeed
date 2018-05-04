using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SculptorWebApi.DataAccess;
using SculptorWebApi.DataAccess.Interfaces;
using SculptorWebApi.Services.DataAccessServices.Classes;
using SculptorWebApi.Services.DataAccessServices.Classes.Interfaces;
using SculptorWebApi.Services.DataAccessServices.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SculptorWebApi.Tests.Services
{
    [TestClass]
    public class PredictiveModelsServiceTests : ServiceTestsBase<IPredictiveModelService, SculptorContext>
    {
        public PredictiveModelsServiceTests()
        {

        }

        protected override void SetupMock()
        {
            Mock.Provide<IPredictiveModelService, PredictiveModelService>();
        }

        [TestMethod]
        public void TestPredictiveModels_FindAll()
        {
            // Act
            var result = TestedService.FindAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 6);//db scpecific
        }

        [TestMethod]
        public void TestPredictiveModels_FindByID()
        {
            // Act
            var result = TestedService.FindByID(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ID, 1);//db scpecific
        }

        [TestMethod]
        public void TestPredictiveModels_Create()
        {
            // Arrange
            var dto = new PredictiveModelDTO()
            {
                Name = "Test DTO",
                ModelData = "Test Data "
            };

            // Act
            var result = TestedService.Create(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "Test DTO");

            Unit.Rollback();
        }

        [TestMethod]
        public void TestPredictiveModels_CreateMany()
        {
            // Arrange
            var dto1 = new PredictiveModelDTO()
            {
                Name = "Test DTO 1",
                ModelData = "Test Data 1"
            };

            var dto2 = new PredictiveModelDTO()
            {
                Name = "Test DTO 2",
                ModelData = "Test Data 2"
            };

            var dtos = new List<PredictiveModelDTO> { dto1, dto2 };

            // Act
            var result = TestedService.Create(dtos);
            var found = TestedService.FindByID(result.FirstOrDefault().ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.FirstOrDefault().ID, 0);
            Assert.AreEqual(result.Count, 2);//db scpecific
            Assert.IsNotNull(found);
            Assert.AreEqual(result.FirstOrDefault().Name, found.Name);

            Unit.Rollback();
        }

        [TestMethod]
        public void TestPredictiveModels_Update()
        {
            // Arrange
            var dto = TestedService.FindByID(1);
            dto.Name = "Test Updating";

            // Act
            TestedService.Update(dto);
            var result = TestedService.FindByID(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, dto.Name);

            Unit.Rollback();
        }

        [TestMethod]
        public void TestPredictiveModels_Delete()
        {
            // Arrange
            var existing = TestedService.FindByID(1);

            // Act
            TestedService.Delete(1);
            var result = TestedService.FindByID(1);

            // Assert
            Assert.IsNull(result);
            Assert.IsNotNull(existing);

            Unit.Rollback();
        }
    }
}
