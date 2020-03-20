using Components.Ui.EventBus.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Components.Ui.EventBus.Tests
{
    [TestClass]
    public class EventbusManagerTests
    {

        /// <summary>
        /// return static settings to start.
        /// </summary>
        [TestCleanup]
        public void CleanUpEventbus()
        {
            var eventbus = EventbusManager.GetEventbus();
            eventbus.Deregister<MockView>();
            eventbus.Deregister<Mock2View>();

            Assert.AreEqual(0, eventbus.RegisteredCount);
        }

        /// <summary>
        /// register one message
        /// </summary>
        [TestMethod]
        public void EventbusManagerSimpleTest()
        {
            // arrange
            var eventbus = EventbusManager.GetEventbus();

            // act
            eventbus.Register<MockView, MockMessage>((message) => true);

            // asssert

            Assert.AreEqual(1, eventbus.RegisteredCount);
        }

        /// <summary>
        /// register two message to one view
        /// </summary>
        [TestMethod]
        public void EventbusManagerRegisterTwoEventsInOneViewErrorTest()
        {
            // arrange
            var eventbus = EventbusManager.GetEventbus();
            Func<IMessageContainer, bool> message1 = (message) => true;
            Func<IMessageContainer, bool> message2 = (message) => true;
            eventbus.Register<MockView, MockMessage>(message1);

            // act
            var result = Assert.ThrowsException<EventbusException>(() => eventbus.Register<MockView, MockMessage>(message2));

            // asssert
            Assert.AreEqual("can not register one moretime to the viewType MockView about message type: MockMessage", result.Message);
        }

        [TestMethod]
        public void EventbusManagerRegisterTwoEventsInOneViewTest()
        {
            // arrange
            var eventbus = EventbusManager.GetEventbus();
            Func<IMessageContainer, bool> message1 = (message) => true;
            Func<IMessageContainer, bool> message2 = (message) => true;

            // act
            eventbus.Register<MockView, MockMessage>(message1);
            eventbus.Register<MockView, MockMessage2>(message2);

            // asssert
            Assert.AreEqual(1, eventbus.RegisteredCount);
            Assert.AreEqual(2, eventbus.RegisteredCountAll);
        }

        [TestMethod]
        public void EventbusManagerSendOneMessageTest()
        {
            // arrange
            var eventbus = EventbusManager.GetEventbus();
            string result = string.Empty;
            Func<IMessageContainer, bool> message1 = (message) => { result = message.Content.ToString(); return true; };
            Func<IMessageContainer, bool> message2 = (message) => true;
            eventbus.Register<MockView, MockMessage>(message1);
            eventbus.Register<MockView, MockMessage2>(message2);

            // act
            eventbus.Send<MockView, MockMessage>(new MockMessage("my message"));

            // asssert
            Assert.AreEqual("my message", result);
        }

        [TestMethod]
        public void EventbusManagerSendOneMessageErrorNotReceiverTest()
        {
            // arrange
            var eventbus = EventbusManager.GetEventbus();
            Func<IMessageContainer, bool> message1 = (message) => true;
            eventbus.Register<MockView, MockMessage>(message1);

            // act
            var result = Assert.ThrowsException<EventbusException>(() => eventbus.Send<MockView, MockMessage2>(new MockMessage2("my message")));

            // asssert
            Assert.AreEqual("Not found or registered. View: MockView, MockMessage2", result.Message);
        }

        /// <summary>
        /// register two messae to different views
        /// </summary>
        [TestMethod]
        public void EventbusManagerRegisterTwoEventsForTwoViewsGetCountFromTargetViewTest()
        {
            // arrange
            var eventbus = EventbusManager.GetEventbus();
            Func<IMessageContainer, bool> message1 = (message) => true;
            Func<IMessageContainer, bool> message2 = (message) => true;

            // act
            eventbus.Register<MockView, MockMessage>(message1);
            eventbus.Register<Mock2View, MockMessage>(message2);

            // asssert
            Assert.AreEqual(2, eventbus.RegisteredCount);
        }
    }
}
