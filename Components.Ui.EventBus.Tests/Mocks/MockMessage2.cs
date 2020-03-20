namespace Components.Ui.EventBus.Tests.Mocks
{
    public class MockMessage2 : IMessageContainer
    {
        private string _message = "Test Mock";

        public MockMessage2()
        {
        }

        public MockMessage2(string message) => this._message = message;

        public object Content => this._message;
    }
}
