
namespace ServiceTests
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Service.Controllers;
    using Service.Models.Request;
    
    /// <summary>
    /// Performance controller tests
    /// </summary>
    [TestClass]
    public class PerformanceControllerTests
    {
        /// <summary>
        /// Test for the GetResponseTime method 
        /// </summary>
        [TestMethod]
        public void GetResponseTimeGetResponsesOk()
        {
            // Arrange
            var request = new UrlRequestModel();
            request.Id = "google";
            request.Name = "Google";
            request.Url = "www.google.com";
            request.Repetition = 5;

            // Act
            PerformanceController controller = new PerformanceController();
            var response = controller.GetResponseTime(request);
            var responseModel = JsonConvert.DeserializeObject(response, typeof(List<int>)) as List<int>;

            // Assert
            Assert.IsNotNull(response);            
            Assert.IsNotNull(responseModel);            
            Assert.AreEqual(5, responseModel.Count);
        }
    }
}
