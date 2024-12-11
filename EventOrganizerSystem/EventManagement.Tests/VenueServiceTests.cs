using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.Services;

namespace EventOrganizerSystem.Tests
{
    [TestClass]
    public class VenueServiceTests
    {
        [TestMethod]
        public void AddVenue_ShouldAddVenue()
        {
            var service = new VenueService();
            var newVenue = new Venue { Name = "Test Venue" };
            service.AddVenue(newVenue);
            Assert.AreEqual(1, service.GetAllVenues().Count);
        }
    }
}