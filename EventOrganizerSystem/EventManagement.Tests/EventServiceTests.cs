using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.Services;

namespace EventOrganizerSystem.Tests
{
    [TestClass]
    public class EventServiceTests
    {
        [TestMethod]
        public void AddEvent_ShouldAddEvent()
        {
            var service = new EventService();
            var newEvent = new Event { Name = "Test Event" };
            service.AddEvent(newEvent);
            Assert.AreEqual(1, service.GetAllEvents().Count);
        }
    }
}