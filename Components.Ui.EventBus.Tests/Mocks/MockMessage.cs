namespace Components.Ui.EventBus.Tests.Mocks
{
    public class MockMessage : IMessageContainer
    {
        private string _message = "Test Mock";

        public MockMessage()
        {
        }

        public MockMessage(string message) => this._message = message;

        public object Content => this._message;
    }
}
