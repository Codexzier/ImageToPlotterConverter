using System;

namespace Components.Ui.EventBus
{
    public class EventbusException : Exception
    {
        public EventbusException()
        {
        }

        public EventbusException(string message) : base(message)
        {
        }

        public EventbusException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}