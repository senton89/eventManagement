using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.Services;

namespace EventOrganizerSystem.Tests
{
    [TestClass]
    public class OrganizerServiceTests
    {
        [TestMethod]
        public void AddOrganizer_ShouldAddOrganizer()
        {
            var service = new OrganizerService();
            var newOrganizer = new Organizer { Name = "Test Organizer" };
            service.AddOrganizer(newOrganizer);
            Assert.AreEqual(1, service.GetAllOrganizers().Count);
        }
    }
}