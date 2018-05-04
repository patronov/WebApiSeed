using Microsoft.VisualStudio.TestTools.UnitTesting;
using SculptorWebApi.Controllers;
using SculptorWebApi.DataAccess;
using SculptorWebApi.Services.DataAccessServices.Classes;
using SculptorWebApi.Services.DataAccessServices.Classes.Interfaces;
using SculptorWebApi.Services.DataAccessServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace SculptorWebApi.Tests.Controllers
{
    [TestClass]
    public class PredictiveModelsControllerTest : ControllerTestBase<PredictiveModelsController, SculptorContext>
    {
        public PredictiveModelsControllerTest()
        { }

        protected override void SetupMock()
        {
            Mock.Provide<IPredictiveModelService, PredictiveModelService>();
        }

        [TestMethod]
        public async Task GetAll()
        {
            // Act
            IHttpActionResult result = await TestedController.GetAll();
            dynamic din = result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(6, din.Content.Count);
        }

        [TestMethod]
        public async Task GetAll_Alternative()
        {
            // Act
            var result = await TestedController.GetAll() as OkNegotiatedContentResult<object>;
            var content = result.Content as List<PredictiveModelDTO>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<object>));            
            Assert.AreEqual(6, content.Count);
        }

        [TestMethod]
        public async Task GetByID()
        {
            // Act
            var result = await TestedController.GetByID(1) as OkNegotiatedContentResult<object>;
            var content = result.Content as PredictiveModelDTO;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<object>));
            Assert.AreEqual(1, content.ID);
        }

        [TestMethod]
        public async Task Create()
        {
            // Arrange
            var dto = new PredictiveModelDTO()
            {
                Name = "Test DTO 1",
                ModelData = "Test Data"
            };

            // Act
            dynamic result = await TestedController.Create(dto);

            Unit.Rollback();

            // Assert
            Assert.AreEqual(result.Content.Name, "Test DTO 1");
        }

        [TestMethod]
        public async Task Update()
        {
            // Arrange
            var tochange = await TestedController.GetByID(1) as OkNegotiatedContentResult<object>;
            var dto = tochange.Content as PredictiveModelDTO;
           
            dto.Name = "Test Updating";

            // Act
            dynamic result = await TestedController.Update(dto);

            Unit.Rollback();

            // Assert
            Assert.AreEqual(result.Content.Name, "Test Updating");
        }

        [TestMethod]
        public async Task Delete()
        {
            // Arrange
            var existingResult = await TestedController.GetByID(1) as OkNegotiatedContentResult<object>;
            var existingContent = existingResult.Content as PredictiveModelDTO;

            // Act
            var confirmation = await TestedController.Delete(1) as OkNegotiatedContentResult<bool>;
            var result = await TestedController.GetByID(1) as OkNegotiatedContentResult<object>;

            Unit.Rollback();

            // Assert
            Assert.IsTrue(confirmation.Content);
            Assert.IsNotNull(existingContent);
            Assert.IsNull(result);
        }
    }
}
