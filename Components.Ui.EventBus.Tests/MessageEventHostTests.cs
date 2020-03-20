using Components.Ui.EventBus.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Components.Ui.EventBus.Tests
{
    [TestClass]
    public class MessageEventHostTests
    {
        /// <summary>
        /// Send a message to registered host with type
        /// </summary>
        [TestMethod]
        public void MessageEventHostSimpleTest()
        {
            // arrange
            IMessageEventHost eventHost = new MessageEventHost<MockView, MockMessage>();
            object result = null;
            Func<IMessageContainer, bool> eventMethod = (message) => {
                result = message.Content;
                return true; };
            eventHost.Subscribe(eventMethod);

            // act
            eventHost.Send(new MockMessage());

            // assert
            var host = (MessageEventHost<MockView, MockMessage>)eventHost;
            Assert.AreEqual("Test Mock", result);
        }

        /// <summary>
        /// Send a message with a specific test
        /// </summary>
        [TestMethod]
        public void MessageEventHostSimpleSetMessageContentTest()
        {
            // arrange
            IMessageEventHost eventHost = new MessageEventHost<MockView, MockMessage>();
            object result = null;
            bool eventMethod(IMessageContainer message)
            {
                result = message.Content;
                return true;
            }
            eventHost.Subscribe(eventMethod);
            
            // act
            eventHost.Send(new MockMessage("Hello"));

            // assert
            var host = (MessageEventHost<MockView, MockMessage>)eventHost;
            Assert.AreEqual("Hello", result);
        }

        [TestMethod]
        public void MessageEventHostRegisterTwoViewsSendMessageToTarget()
        {

        }
    }
}
