using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace JordnærTest2
{
    [TestClass]
    public class UnitTest2
    {
        private string connection = "";
        [TestMethod]
        public async Task AddEventServiceAsync()
        {
            //arrange 
            EventService eventService = new EventService(connection);
            List<Event> events = eventService.GetAllEventsAsync().Result;
            //act
            int noOfEventsBefore = events.Count;
            Event eEvent = new Event(10, "TestEvent", DateTime.Now, DateTime.Today, 699, false, 45);
            eventService.AddEventAsync(eEvent);

            int noOfEventsAfter = events.Count;
            //assert
            Assert.AreEqual(noOfEventsBefore -1, noOfEventsAfter);
        }

    }
}
